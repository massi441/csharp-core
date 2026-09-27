using System.Text.Json.Serialization;

namespace Core.Github;

/// <summary>
/// Represents an API response from a Github Release
/// </summary>
public class GithubRelease
{
    [JsonInclude, JsonPropertyName("tag_name")]
    public required string ReleaseTag { get; set; } = string.Empty;


    [JsonPropertyName("assets")]
    public required List<GithubReleaseAsset> ReleaseAssets { get; set; }
}

/// <summary>
/// Represents a specific asset as part of a github release
/// </summary>
public class GithubReleaseAsset
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("browser_download_url")]
    public required string DownloadUrl { get; set; }
}
