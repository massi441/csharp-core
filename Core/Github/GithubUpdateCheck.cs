namespace Core.Github;

/// <summary>
/// A result indicating whether the current version of an application needs an update,
/// based on the latest version found on github.
/// Note: The current version of the application is always present in the result
/// </summary>
public class GithubUpdateCheck
{
    public Version CurrentVersion { get; init; } = null!;
    public Version? LatestVersion { get; init; }

    internal GithubUpdateCheck(Version currentVersion)
    {
        CurrentVersion = new Version(currentVersion.Major, currentVersion.Minor, currentVersion.Build);
    }

    internal GithubUpdateCheck(Version currentVersion, Version latestVersion) : this(currentVersion)
    {
        LatestVersion = latestVersion;
    }

    public bool IsNeedUpdate()
    {
        return LatestVersion != null && LatestVersion > CurrentVersion;
    }
}
