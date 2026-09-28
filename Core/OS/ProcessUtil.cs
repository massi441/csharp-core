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

    /// <summary>
    /// Starts a process in a new console
    /// </summary>
    public static Process? StartNewProcess(string processPath)
    {
        ProcessStartInfo startupInfo = new ProcessStartInfo(processPath)
        {
            UseShellExecute = true
        };

        return Process.Start(startupInfo);
    }

    /// <summary>
    /// Starts a process in a new console
    /// </summary>
    /// <returns>true if the process was started, false otherwise</returns>
    public static bool TryStartNewProcess(string processPath)
    {
        return StartNewProcess(processPath) != null;
    }
}
