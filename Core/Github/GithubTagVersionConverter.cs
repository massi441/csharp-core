namespace Core.Github;

/// <summary>
/// Converts a github tag name into a Version object
/// </summary>
public interface IGithubTagVersionConverter
{
    Version Convert(string gitTag);
}
