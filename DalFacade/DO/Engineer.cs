namespace DO;
/// <summary>
/// Engineer Entity represents a engineer with all its props
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Private name of the engineer</param>
/// <param name="Email">Email of the engineer</param>
/// <param name="EngineerLevel">Engineer experience</param>
/// <param name="Cost">The engineer's salary per hour</param>
public record Engineer
(
    int Id,
    string Name,
    string Email,
    EngineerExperience? EngineerLevel,
    double Cost
);

