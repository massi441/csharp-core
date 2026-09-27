namespace Core.Util;

/// <summary>
/// Represents the state of the current operation
/// </summary>
internal enum ProgressStatusState
{
    /// <summary>
    /// The operation is still in progress
    /// </summary>
    InProgress,

    /// <summary>
    /// The operation has successfully completed
    /// </summary>
    Completed,

    /// <summary>
    /// The operation has failed to complete
    /// </summary>
    Failed
}

/// <summary>
/// A structure representing the progress status of an operation,
/// with a failed status and status message
/// </summary>
public class ProgressStatus
{
    private ProgressStatusState State { get; init; }
    public string Message { get; internal init; } = string.Empty;

    public bool IsInProgress()
    {
        return State == ProgressStatusState.InProgress;
    }

    public bool IsCompleted()
    {
        return State == ProgressStatusState.Completed;
    }

    public bool IsFailed()
    {
        return State == ProgressStatusState.Failed;
    }

    public static ProgressStatus InProgress()
    {
        return new ProgressStatus()
        {
            State = ProgressStatusState.InProgress,
        };
    }

    public static ProgressStatus InProgress(string message)
    {
        return new ProgressStatus()
        {
            State = ProgressStatusState.InProgress,
            Message = message
        };
    }

    public static ProgressStatus Completed()
    {
        return new ProgressStatus()
        {
            State = ProgressStatusState.Completed,
        };
    }

    public static ProgressStatus Completed(string message)
    {
        return new ProgressStatus()
        {
            State = ProgressStatusState.Completed,
            Message = message
        };
    }

    public static ProgressStatus Failure(string errorMessage)
    {
        return new ProgressStatus()
        {
            State = ProgressStatusState.Failed,
            Message = errorMessage
        };
    }
}
