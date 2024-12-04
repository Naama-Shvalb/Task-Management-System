namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// The <c>EngineerImplementation</c> class is an implementation of the <c>IEngineer</c> interface
/// responsible for managing and interacting with engineer entities.
/// </summary>
internal class EngineerImplementation : IEngineer
{

    /// <summary>
    /// Creates a new engineer entity in the data source.
    /// </summary>
    public int Create(Engineer item)
    {
        //for entities with normal id (not auto id)
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item with { });
        return item.Id;
    }


    /// <summary>
    /// Deletes an engineer entity from the data source.
    /// </summary>
    public void Delete(int id)
    { 
        if (Read(id) != null)
            DataSource.Engineers.RemoveAll(s => s!.Id == id);
        else
            throw new Exception($"Engineer with ID={id} is not exists");
    }

    /// <summary>
    /// Reads an engineer entity from the data source by its ID.
    /// </summary>
    public Engineer? Read(int id)
    {
        Engineer? engineerFind= DataSource.Engineers.Where(s => s!.Id == id).First();
        return engineerFind != null ? engineerFind : null;
    }

    /// <summary>
    /// Retrieves a list of all engineer entities from the data source.
    /// </summary>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers!);
    }

    /// <summary>
    /// Updates an existing engineer entity in the data source.
    /// </summary>
    public void Update(Engineer item)
    {
        Engineer? tstId = Read(item.Id);
        if (tstId != null)
        {
            Delete(item.Id);
            Engineer engineer = new(item.Id, item.Name, item.Email,item!.EngineerLevel,item.Cost);
            DataSource.Engineers.Add(engineer);
        }
        else
            throw new Exception($"Engineer with ID={item.Id} is not exists");
    }
}
