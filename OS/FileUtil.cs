namespace Core.OS;

/// <summary>
/// A static class containing utilities with the file system
/// </summary>
public static class FileUtil
{
    /// <summary>
    /// Returns the directory of where the currently executing file is located
    /// </summary>
    public static string GetExecutingDirectory()
    {
        return AppContext.BaseDirectory;
    }

    /// <summary>
    /// Constructs a file path from the AppContext's BaseDirectory and a given path
    /// </summary>
    /// <param name="path">The path relative to the AppContext's BaseDirectory</param>
    public static string PathFromRunningDir(string path)
    {
        return Path.Combine(AppContext.BaseDirectory, path);
    }

    /// <summary>
    /// Deletes a directory if it exists, and returns true if it did.
    /// </summary>
    public static bool DeleteDirIfExists(string path, bool recursive = true)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, recursive);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes a file if it exists, and returns true if it did.
    /// </summary>
    public static bool DeleteFileIfExists(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Retrieves the file name of the currently executing file, and throws if it cannot be retrieved.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static string GetExecutingFileName()
    {
        return Path.GetFileName(Environment.ProcessPath) ?? throw new Exception("Count not get name of executing file");
    }

    /// <summary>
    /// Renames a file and defaults to overwrite mode
    /// </summary>
    public static void RenameFile(string oldPath, string newPath, bool overwrite = true)
    {
        File.Move(oldPath, newPath, overwrite);
    }

    /// <summary>
    /// Copies the contents of one directory into another
    /// </summary>
    /// <param name="source">The directory with the contents to copy</param>
    /// <param name="dest">The directory where the contents are copied</param>
    /// <param name="overwrite">If the copied contents should overwrite</param>
    public static void CopyDirectory(string source, string dest, bool overwrite = true)
    {
        foreach (string dir in Directory.EnumerateDirectories(source, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(Path.Combine(dest, Path.GetRelativePath(source, dir)));
        }

        Directory.CreateDirectory(dest);
        foreach (string file in Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories))
        {
            string target = Path.Combine(dest, Path.GetRelativePath(source, file));
            File.Copy(file, target, overwrite);
        }
    }
}
