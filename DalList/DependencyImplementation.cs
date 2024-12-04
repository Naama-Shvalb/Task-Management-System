namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// The <c>DependencyImplementation</c> class is an implementation of the <c>IDependency</c> interface
/// responsible for managing and interacting with dependency entities.
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates a new dependency entity in the data source.
    /// </summary>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextTaskId;
        Dependency newItem = item with { Id = newId };
        DataSource.Dependencies.Add(newItem);
        return newId;
    }

    /// <summary>
    /// Deletes a dependency entity from the data source by its ID.
    /// </summary>
    public void Delete(int id)
    {
        if(Read(id) != null)
            DataSource.Dependencies.Remove(Read(id));
        else
            throw new Exception($"Dependency with ID={id} is not exists");
    }

    /// <summary>
    /// Reads a dependency entity from the data source by its ID.
    /// </summary>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(dp => dp!.Id == id);
    }

    /// <summary>
    /// Retrieves a list of all dependency entities from the data source.
    /// </summary>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies!);
    }

    /// <summary>
    /// Updates an existing dependency entity in the data source.
    /// </summary>
    public void Update(Dependency item)
    {
        Dependency? tstId = Read(item.Id);  
        if (tstId != null)
        {
            Delete(item.Id);
            Dependency dependency = new(item.Id, item.DependentTaskId, item.PreviousTaskId);
            DataSource.Dependencies.Add(dependency);
        }
        else
            throw new Exception($"Dependency with ID={item.Id} is not exists");
    }

    
    /// <summary>
    /// Reads a dependency entity from the data source by the DependentTaskId and PreviousTaskId.
    /// </summary>
    /// <param name="_DependentTaskId">The ID of the dependent task.</param>
    /// <param name="_PreviousTaskId">The ID of the previous task.</param>
    /// <returns>The dependency entity if found; otherwise, null.</returns>
    public Dependency? Read(int _DependentTaskId, int _PreviousTaskId)
    {
        return DataSource.Dependencies.Find(dp => dp!.DependentTaskId == _DependentTaskId && dp.PreviousTaskId == _PreviousTaskId);
    }

}
