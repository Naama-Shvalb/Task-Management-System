namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// The <c>TaskImplementation</c> class is an implementation of the <c>ITask</c> interface
/// responsible for managing and interacting with task entities.
/// </summary>
internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates a new task entity in the data source.
    /// </summary>
    /// <param name="item">The task to be created.</param>
    /// <returns>The ID of the newly created task.</returns>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newItem = item with { Id = newId };
        DataSource.Tasks.Add(newItem);
        return newId;
    }
    
    /// <summary>
    /// Deletes a task entity from the data source by its ID.
    /// </summary>
    public void Delete(int id)
    {
        if (Read(id) != null)
            DataSource.Tasks.Remove(Read(id));
        else
            throw new Exception($"Task with ID={id} is not exists");
    }

    /// <summary>
    /// Reads a task entity from the data source by its ID.
    /// </summary>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(task => task!.Id == id);
    }

    /// <summary>
    /// Retrieves a list of all task entities from the data source.
    /// </summary>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks!);
    }

    /// <summary>
    /// Updates an existing task entity in the data source.
    /// </summary>
    public void Update(Task item)
    {
        Task? tstId = Read(item.Id);
        if (tstId != null)
        {
            Delete(item.Id);
            Task task = new(item.Id, item.Description, item.Alias, item.Deliverables, item.Remarks,
                item.EngineerId, item.CopmlexityLevel, item.Start, item.ScheduledDate, item.Deadline,  item.Milestone,item.CompleteAt);
            DataSource.Tasks.Add(task);
        }
        else
            throw new Exception($"Task with ID={item.Id} is not exists");
    }


}
