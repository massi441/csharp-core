using System.Diagnostics.CodeAnalysis;

namespace Core.Util;

/// <summary>
/// A static class of reflection utilities
/// </summary>
public static class ReflectionUtil
{
    /// <summary>
    /// Tries to get the version of the assembly a given type is in
    /// </summary>
    /// <typeparam name="T">A type from the assembly to check the version of</typeparam>
    /// <param name="version">The version if found, null otherwise</param>
    /// <returns>True if the version was found</returns>
    public static bool TryGetAssemblyVersionOf<T>([NotNullWhen(true)] out Version? version)
    {
        version = typeof(T).Assembly.GetName().Version;
        return version != null;
    }

    /// <summary>
    /// Returns the version of the assembly a given type is in, and throws
    /// if it is not found
    /// </summary>
    /// <typeparam name="T">A type from the assembly to check the version of</typeparam>
    /// <returns>The version of the assembly</returns>
    /// <exception cref="Exception">If the version was not found</exception>
    public static Version GetAssemblyVersionOf<T>()
    {
        return typeof(T).Assembly.GetName().Version
            ?? throw new Exception("The version of the application was not found on the current installation, try again later or contact the developer if this issue still persists");
    }

    /// <summary>
    /// Returns the name of an assembly a given type is in, and throws 
    /// if it is not found
    /// </summary>
    /// <typeparam name="T">The type to check the assembly name of</typeparam>
    /// <returns>The name of the assembly</returns>
    /// <exception cref="Exception">If the name was not found</exception>
    public static string GetAssemblyNameOf<T>()
    {
        return typeof(T).Assembly.GetName().Name
            ?? throw new Exception($"Could not get the name of the assembly for {typeof(T).Name}");
    }
}