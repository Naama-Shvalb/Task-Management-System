namespace DalApi;
using DO;
//public interface IStudent : ICrud<Student> { }

public interface IDependency : ICrud<Dependency>
{
    Dependency? Read(int _DependentTaskId, int _PreviousTaskId); //Reads entity object by 2 IDs
}
