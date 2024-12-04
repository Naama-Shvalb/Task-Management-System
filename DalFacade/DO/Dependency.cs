namespace DO;
/// <summary>
/// represents a dependency between the tasks
/// </summary>
/// <param name="Id">Unique ID number (automatic runner number)</param>
/// <param name="DependentTaskId">ID of a pending task</param>
/// <param name="PreviousTaskId">ID for a previous task</param>
public record Dependency
(
    int Id,
    int DependentTaskId,
    int PreviousTaskId
);
