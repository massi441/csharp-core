namespace Core.Util;

/// <summary>
/// A static utility class for enums
/// </summary>
public static class EnumUtil
{
    /// <summary>
    /// Returns the number of named values in the specified enum type
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <returns>The number of named values in the enum</returns>
    public static int GetEnumCount<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Length;
    }
}
