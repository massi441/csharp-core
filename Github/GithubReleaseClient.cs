using System.IO.Compression;
using System.Text.Json;
using Core.Util;

namespace Core.Github;

/// <summary>
/// A Github client for checking and downloading latest releases of an application
/// </summary>
public class GithubReleaseClient
{
    private readonly HttpClient _httpClient;
    private readonly IGithubTagVersionConverter _tagConverter;

    /// <summary>
    /// The url of the Github repository's latest release
    /// </summary>
    public string ReleaseUrl { get; }

    /// <summary>
    /// The name of the release, may vary depending on which platform release you are targetting
    /// </summary>
    public string ReleaseName { get; }

    public GithubReleaseClient(string releaseUrl, string appName, string releaseName, IGithubTagVersionConverter tagConverter)
    {
        ReleaseUrl = releaseUrl;
        ReleaseName = releaseName;

        _tagConverter = tagConverter;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(appName);
    }

    /// <summary>
    /// Checks if the assembly of a given type needs an update, based on the current version
    /// of the assembly and the latest version published on github
    /// </summary>
    /// <typeparam name="T">Any type in the assmembly that needs the version check for</typeparam>
    /// <returns>A <see cref="GithubUpdateCheck"/> result</returns>
    public async Task<GithubUpdateCheck> CheckUpdateFor<T>()
    {
        Version currentVersion = ReflectionUtil.GetAssemblyVersionOf<T>();

        GithubRelease? githubRelease = await FetchRelease();
        if (githubRelease == null)
        {
            return new GithubUpdateCheck(currentVersion);
        }

        Version latestVersion = _tagConverter.Convert(githubRelease.ReleaseTag);
        
        foreach (GithubReleaseAsset releaseAsset in githubRelease.ReleaseAssets)
        {
            if (releaseAsset.Name == ReleaseName)
            {
                return new GithubUpdateCheck(currentVersion, latestVersion);
            }
        }

        return new GithubUpdateCheck(currentVersion);
    }

    /// <summary>
    /// Downloads the latest release of the assembly of a given type, if an update is needed,
    /// and extracts it into the given output folder
    /// </summary>
    /// <typeparam name="T">Any type in the assembly to download the latest release for</typeparam>
    /// <param name="outputPath">The folder where the releae should be extracted</param>
    /// <param name="overwriteFiles">If existing files should be overwritten in the output</param>
    public async IAsyncEnumerable<ProgressStatus> DownloadLatestFor<T>(string outputPath, bool overwriteFiles = true)
    {
        yield return ProgressStatus.InProgress($"Downloading release information from {ReleaseUrl}...");

        GithubRelease? githubRelease = await FetchRelease();
        if (githubRelease == null)
        {
            yield return ProgressStatus.Failure($"Github release not found from {ReleaseUrl}");
            yield break;
        }

        GithubUpdateCheck updateCheck = new GithubUpdateCheck(ReflectionUtil.GetAssemblyVersionOf<T>(), _tagConverter.Convert(githubRelease.ReleaseTag));
        if (!updateCheck.IsNeedUpdate())
        {
            yield return ProgressStatus.Completed($"Current version ({updateCheck.CurrentVersion}) is already up to date with latest version on github ({updateCheck.LatestVersion})");
            yield break;
        }

        yield return ProgressStatus.InProgress($"Downloaded release information from github, looking for {ReleaseName} release");

        foreach (GithubReleaseAsset releaseAsset in  githubRelease.ReleaseAssets)
        {
            if (releaseAsset.Name != ReleaseName)
            {
                continue;
            }

            yield return ProgressStatus.InProgress($"Found release, downloading it from {releaseAsset.DownloadUrl}...");

            using Stream zipStream = await _httpClient.GetStreamAsync(releaseAsset.DownloadUrl);

            yield return ProgressStatus.InProgress($"Successfully downloaded release, unzipping it...");

            await ZipFile.ExtractToDirectoryAsync(zipStream, outputPath, overwriteFiles: overwriteFiles);

            yield return ProgressStatus.Completed();
            yield break;
        }
    }

    private async Task<GithubRelease?> FetchRelease()
    {
        string releaseBody = await _httpClient.GetStringAsync(ReleaseUrl);

        try
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
            {
                RespectNullableAnnotations = true,
            };

            GithubRelease? release = JsonSerializer.Deserialize<GithubRelease>(releaseBody, jsonOptions);

            return release;
        }
        catch (JsonException ex)
        {
            throw new Exception($"A JSON error occured while deserializaing release response from github: {ex.Message}");
        }
    }
}
