namespace Core.Github;

/// <summary>
/// Converts tag version in the format "{Prefix}x.x.x"
/// </summary>
public class GithhubTagVersionPrefixConvertor : IGithubTagVersionConverter
{
    public string Prefix { get; }

    public GithhubTagVersionPrefixConvertor(string prefix)
    {
        Prefix = prefix;
    }

    public Version Convert(string gitTag)
    {
        return Version.Parse(gitTag[Prefix.Length..]);
    }
}
