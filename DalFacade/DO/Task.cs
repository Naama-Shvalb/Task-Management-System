namespace DO;
/// <summary>
/// Task entity represents a task with all its accessories
/// </summary>
/// <param name="Id">Unique ID number (automatic runner number)</param>
/// <param name="Description">The task description</param>
/// <param name="Alias">Short name of the task</param>
/// <param name="Deliverables">Product (a string describing the product)</param>
/// <param name="Remarks">Notes on the task</param>
/// <param name="EngineerId">An engineering ID that is an edge to the task</param>
/// <param name="CopmlexityLevel">Difficulty level of the task</param>
/// <param name="Start">Task start date</param>
/// <param name="ScheduledDate">Estimated completion date</param>
/// <param name="Deadline">Final date for completion</param>
/// <param name="CreatedAt">Task creation date</param>
/// <param name="Milestone">milestone (boolean)</param>
/// <param name="CompleteAt">Actual end date</param>

public record Task
(
int Id,
string Description,
string? Alias,
string? Deliverables,
string? Remarks,
int EngineerId,
EngineerExperience? CopmlexityLevel,
DateTime? Start,
DateTime? ScheduledDate,
DateTime? Deadline,
bool Milestone,
DateTime? CompleteAt
)

    
{
    public DateTime CreatedAt => DateTime.Now; //get only
}
