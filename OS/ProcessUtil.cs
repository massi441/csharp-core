using System.Diagnostics;

namespace Core.OS;

/// <summary>
/// Utility functions on processes
/// </summary>
public static class ProcessUtil
{
    /// <summary>
    /// Counts and closes all instances of a given process 
    /// </summary>
    /// <param name="processName">The name of the process, without the extension</param>
    /// <returns>The count of instances of the process that were closes</returns>
    public static int CloseProcessInstances(string processName)
    {
        int closedCount = 0;
        Process[] instances = Process.GetProcessesByName(processName);
        foreach (Process process in instances)
        {
            process.Kill();
            closedCount++;
        }

        return closedCount;
    }
}
