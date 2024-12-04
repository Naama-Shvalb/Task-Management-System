namespace DalTest;
using DO;
using DalApi;
using System.Security.Cryptography;
using System.Collections.Generic;

/// <summary>
/// The <c>Initialization</c> class provides methods for initializing test data in the DAL.
/// </summary>
public static class Initialization
{ 
    private static IDal? s_dal; //stage 2
    private const int LEVELS = 5;
    private static readonly Random s_rand = new();

    /// <summary>
    /// Initializes the DAL with test data, including engineers, tasks, and their dependencies.
    /// </summary>
    /// <param name="dalDependency">An instance of the dependency data access layer.</param>
    /// <param name="dalEngineer">An instance of the engineer data access layer.</param>
    /// <param name="dalTask">An instance of the task data access layer.</param>
    /// <exception cref="NullReferenceException">Thrown if any of the provided DAL instances is null.</exception>
    public static void Do(IDal dal) //stage 2
    {
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        createEngineer();
        createTask();
        createDependency();
    }

    /// <summary>
    /// Creates test engineer entities and adds them to the data source through the DAL.
    /// </summary>
    private static void createEngineer()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;

        string[] engineerNames =
        {
        "Dani Levi", "Eli Amar", "Yair Cohen",
        "Ariela Levin", "Dina Klein", "Shira Israelof"
        };

        foreach (var _name in engineerNames)
        {
            int _id;
            string _email;
            double _cost;

            _email = _name.Replace(" ", ".");//Replaces the space between the names with a point to match an email address

            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer.Read(_id) != null);

            do
            {
                _id = s_rand.Next(MIN_ID, MAX_ID);
            }
            while (s_dal!.Engineer.Read(_id) != null);

            _email = $"{_email}@gmail.com";

            //Converting an int to an enum type in order to get an enum value in a specific place.
            int _levelNumber = _id % 5;
            EngineerExperience _level = (EngineerExperience)_levelNumber;

            _cost = (double)s_rand.Next(100, 500);

            Engineer newEngineer = new(_id, _name, _email, _level, _cost);

            s_dal!.Engineer.Create(newEngineer);
        }
    }

    /// <summary>
    /// Creates test task entities and adds them to the data source through the DAL.
    /// </summary>
    private static void createTask()
    {

        string[] descriptions = new string[]
        {
            "Complete project proposal",
            "Review and provide feedback on team members' work",
            "Prepare presentation for the client meeting",
            "Submit expense reports",
            "Research and gather data for market analysis",
            "Schedule team training sessions",
            "Conduct user interviews for UX research",
            "Code review for software development",
            "Create content for social media marketing",
            "Test and document software features",
            "Setup and configure server infrastructure",
            "Organize a team-building event",
            "Analyze financial data for quarterly reports",
            "Design a new logo for the company",
            "Prepare and deliver a client presentation",
            "Write technical documentation",
            "Run a software security audit",
            "Manage customer support tickets",
            "Develop a marketing campaign strategy",
            "Create a project timeline and Gantt chart"
        };


        foreach (var _description in descriptions)
        {
            int _count = 1;
            string _alias;
            EngineerExperience _difficulty;
            bool _mileStone = false;
            _alias = $"T{_count++}";
            //Converting an int to an enum type in order to get an enum value in a specific place.
            int _difficultyNumber = s_rand.Next(1, 5);
            _difficulty = (EngineerExperience)_difficultyNumber;
            int range = s_rand.Next(-365, 0); //1 year
            DateTime _createdAtDate = DateTime.Today.AddDays(range);
            Task newTask = new(0, _description, _alias, null, null, 0, _difficulty, null, null, null, _mileStone, null);
            s_dal!.Task.Create(newTask);
        }
    }


    /// <summary>
    /// Creates dependencies between test tasks and adds them to the data source through the DAL.
    /// </summary>
    private static void createDependency()
    {
        List<Task> AllTasks = s_dal!.Task.ReadAll();
        int _sumTask = AllTasks.Count;
        int _dependencyCounter = 0;
        if (_sumTask != 0)
        {
            do
            {
                List<Task> _currentTasks = s_dal!.Task.ReadAll();
                foreach (Task _task in _currentTasks)
                {
                    int _dependentTaskId = _task.Id;
                    int _numDep = s_rand.Next(0, 4);
                    for (int i = 0; i < _numDep; i++)
                    {
                        int previousTaskPlace = s_rand.Next(0, _sumTask);
                        int _previousTaskId = _currentTasks[previousTaskPlace].Id;
                        if (s_dal!.Dependency.Read(_previousTaskId, _dependentTaskId) == null)
                        {
                            _dependencyCounter++;
                            Dependency newDependency = new(0, _dependentTaskId, _previousTaskId);
                            s_dal!.Dependency.Create(newDependency);
                        }
                    }
                }
            } while (_dependencyCounter < 41);
        }
    }
}
