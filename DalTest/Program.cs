using Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace DalTest
{
    internal class Program
    {
        static readonly IDal s_dal = new Dal.DalList(); //stage 2

        /// <summary>
        /// The main entry point of the program. Initializes data access objects and allows users to interact with the program.
        /// </summary>
        static void Main()
        {
            try
            {
                // Initialize the DAL components and data.
                Initialization.Do(s_dal); //stage 2        
                navigate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Provides a menu for navigating between different data entities (Engineer, Task, Dependency) or exiting the program.
        /// </summary>
        private static void navigate()
        {
            try
            {
                string? _choice;              
                do
                {
                    Console.WriteLine("Enter your choice: Engineer, Task, Dependency, Exit");
                    _choice = Console.ReadLine();
                    Dictionary<string, Action> actions = new Dictionary<string, Action>
                    {
                        { "Engineer", navigateEngineer },
                        { "Task", navigateTask},
                        { "Dependency", navigateDependency},
                        {"Exit", Main}
                    };
                    if (actions.ContainsKey(_choice!))
                    {
                        actions[_choice!].Invoke();
                    }
                    else
                    {
                        throw new Exception("choice does not exist.");
                    }
                } while (_choice != "Exit");

            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        /// <summary>
        /// Engineer navigation menu.
        /// </summary>
        private static void navigateEngineer()
        {
            try
            {
                string? _choice;
                do
                {
                    Console.WriteLine("Enter your choice: Create, Delete, Read, ReadAll, Update, Exit");
                    _choice = Console.ReadLine();
                    Dictionary<string, Action> actions = new Dictionary<string, Action>
                    {
                        { "Create", engineerCreate },
                        { "Delete", engineerDelete },
                        { "Read", engineerRead },
                        { "ReadAll", engineerReadAll },
                        { "Update", engineerUpdate },
                        {"Exit",navigate }
                    };
                    // Get the user's choice or input for _choice
                    if (actions.ContainsKey(_choice!))
                    {
                        actions[_choice!].Invoke();
                    }
                    else
                    {
                        throw new Exception("choice does not exist.");
                    }
                } while (_choice != "Exit");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        /// <summary>
        /// Create a new engineer.
        /// </summary>
        private static void engineerCreate()
        {
             try
            {
                // Gather user input for engineer properties.
                Console.WriteLine("Enter ID, Name, Email, EngineerLevel and Cost  of engineer");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                string? _name = Console.ReadLine()!;
                string? _email = Console.ReadLine()!;
                EngineerExperience _engineerExperience;
                EngineerExperience.TryParse(Console.ReadLine(), out _engineerExperience);
                double _Cost;
                double.TryParse(Console.ReadLine()!, out _Cost);
                // Create a new engineer object and call the DAL to save it.
                Engineer newEngineer = new(_id, _name, _email, _engineerExperience, _Cost);
                Console.WriteLine(s_dal!.Engineer.Create(newEngineer));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Delete an engineer.
        /// </summary>
        private static void engineerDelete()
        {
            try
            {
                // Gather user input for the engineer ID to be deleted.
                Console.WriteLine("Enter ID of an engineer");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                s_dal!.Engineer.Delete(_id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Read an engineer by ID.
        /// </summary>
        private static void engineerRead()
        {
            Console.WriteLine("Enter ID of an engineer");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            Console.WriteLine(s_dal!.Engineer.Read(_id));
        }

        /// <summary>
        /// Read all engineers.
        /// </summary>
        private static void engineerReadAll()
        {
            s_dal!.Engineer.ReadAll().ForEach(engineer => Console.WriteLine(engineer));
        }

        /// <summary>
        /// Update an existing engineer.
        /// </summary>
        private static void engineerUpdate()
        {
            try
            {
                Console.WriteLine("Enter ID of an engineer");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                Engineer? _engineer = s_dal!.Engineer.Read(_id);
                Console.WriteLine("Enter Name, Email, EngineerLevel and Cost  of engineer");
                string? _name = Console.ReadLine()!;
                string? _email = Console.ReadLine()!;
                EngineerExperience? _engineerExperience = tryParseNullableEngineerExperience(_engineer!.EngineerLevel);
                double _cost;
                if (!double.TryParse(Console.ReadLine(), out _cost))
                    _cost = _engineer.Cost;
                if (string.IsNullOrEmpty(_name))
                    _name = _engineer!.Name;
                if (string.IsNullOrEmpty(_email))
                    _email = _engineer!.Email;
                s_dal!.Engineer.Update(new Engineer(_id, _name, _email, _engineerExperience, _cost));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Tries to parse an EngineerExperience value and returns it as a nullable value, allowing for updating the experience level.
        /// </summary>
        /// <param name="previous">The previous value of EngineerExperience.</param>
        /// <returns>The parsed or previous EngineerExperience value.</returns>
        private static EngineerExperience? tryParseNullableEngineerExperience(EngineerExperience? previous)
        {
            EngineerExperience value;
            return EngineerExperience.TryParse(Console.ReadLine(), out value) ? value : previous;
        }


        /// <summary>
        /// navigate for a task.
        /// </summary>
 
        private static void navigateTask()
        {
            try
            {
                string? _choice;
                do
                {
                    Console.WriteLine("Enter your choice: Create, Delete, Read, ReadAll, Update, Exit");
                    _choice = Console.ReadLine();
                    Dictionary<string, Action> actions = new Dictionary<string, Action>
                    {
                        { "Create", taskCreate },
                        { "Delete", taskDelete },
                        { "Read", taskRead },
                        { "ReadAll",taskReadAll },
                        { "Update", taskUpdate },
                        {"Exit", navigate }
                    };
                    // Get the user's choice or input for _choice
                    if (actions.ContainsKey(_choice!))
                    {
                        actions[_choice!].Invoke();
                    }
                    else
                    {
                        throw new Exception("choice does not exist.");
                    }
                } while (_choice != "Exit");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        private static void taskCreate()
        {
            Console.WriteLine("Enter Description, Alias, Deliverables, Remarks, EngineerId, CopmlexityLevel, Start, ScheduledDate and Deadline of task");
            string _description = Console.ReadLine()!;
            string? _alias = Console.ReadLine()!;
            string? _deliverables = Console.ReadLine()!;
            string? _remarks = Console.ReadLine()!;
            int _engineerId;
            int.TryParse(Console.ReadLine()!, out _engineerId);
            EngineerExperience _copmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine(), out _copmlexityLevel);
            DateTime _start;
            DateTime.TryParse(Console.ReadLine(), out _start);
            DateTime _scheduledDate;
            DateTime.TryParse(Console.ReadLine(), out _scheduledDate);
            DateTime _deadline;
            DateTime.TryParse(Console.ReadLine(), out _deadline);
            DO.Task newTask = new(0, _description, _alias, _deliverables, _remarks, _engineerId, _copmlexityLevel, _start, _scheduledDate, _deadline, false, null);
            Console.WriteLine(s_dal!.Task.Create(newTask));
        }

        /// <summary>
        /// Update an existing task.
        /// </summary>
        private static void taskUpdate()
        {
            try
            {
                Console.WriteLine("Enter ID of a task");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                DO.Task? _task = s_dal!.Task.Read(_id);
                Console.WriteLine("Enter Description, Alias, Deliverables, Remarks, EngineerId, CopmlexityLevel, Start, ScheduledDate and Deadline of task");
                string _description = Console.ReadLine()!;
                string? _alias = Console.ReadLine()!;
                string? _deliverables = Console.ReadLine()!;
                string? _remarks = Console.ReadLine()!;
                int _engineerId;
                int.TryParse(Console.ReadLine()!, out _engineerId);
                EngineerExperience? _copmlexityLevel = tryParseNullableEngineerExperience(_task!.CopmlexityLevel);
                DateTime? _start = tryParseNullableDateTime(_task!.Start);
                DateTime? _scheduledDate = tryParseNullableDateTime(_task!.ScheduledDate);
                DateTime? _deadline = tryParseNullableDateTime(_task!.Deadline);
                if (string.IsNullOrEmpty(_description))
                    _description = _task!.Description;
                if (string.IsNullOrEmpty(_alias))
                    _alias = _task!.Alias;
                if (string.IsNullOrEmpty(_deliverables))
                    _deliverables = _task!.Deliverables;
                if (string.IsNullOrEmpty(_remarks))
                    _remarks = _task!.Remarks;
                if (_engineerId == 0)
                    _engineerId = _task.EngineerId;
                s_dal!.Task.Update(new DO.Task(_id, _description, _alias, _deliverables, _remarks, _engineerId, _copmlexityLevel, _start, _scheduledDate, _deadline, false, null));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);


            }
        }

        /// <summary>
        /// Tries to parse a DateTime value and returns it as a nullable value, allowing for updating date-related fields.
        /// </summary>
        /// <param name="previous">The previous value of DateTime.</param>
        /// <returns>The parsed or previous DateTime value.</returns>
        private static DateTime? tryParseNullableDateTime(DateTime? previous)
        {
            DateTime value;
            return DateTime.TryParse(Console.ReadLine(), out value) ? value : previous;
        }

        /// <summary>
        /// Read a task by ID.
        /// </summary>
        private static void taskRead()
        {
            Console.WriteLine("Enter ID of a task");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            Console.WriteLine(s_dal!.Task.Read(_id));
        }

        /// <summary>
        /// Read all tasks.
        /// </summary>
        private static void taskReadAll()
        {
            s_dal!.Task.ReadAll().ForEach(task => Console.WriteLine(task));
        }

        /// <summary>
        /// Delete a task by ID.
        /// </summary>
        private static void taskDelete()
        {
            try
            {
                Console.WriteLine("Enter ID of a task");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                s_dal!.Task.Delete(_id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        /// <summary>
        /// Dependency navigation menu.
        /// </summary>
        private static void navigateDependency()
        {
            try
            {
                string? _choice;
                do
                {
                    Console.WriteLine("Enter your choice: Create, Delete, Read, ReadAll, Update, Exit");
                    _choice = Console.ReadLine();
                    Dictionary<string, Action> actions = new Dictionary<string, Action>
                {
                    { "Create", dependencyCreate },
                    { "Delete", dependencyDelete },
                    { "Read", dependencyRead },
                    { "ReadAll", dependencyReadAll },
                    { "Update", dependencyUpdate },
                    { "Exit", navigate }
                };
                    // Get the user's choice or input for _choice
                    if (actions.ContainsKey(_choice!))
                    {
                        actions[_choice!].Invoke();
                    }
                    else
                    {
                        throw new Exception("choice does not exist.");
                    }
                } while (_choice != "Exit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }

        /// <summary>
        /// Create a new dependency.
        /// </summary>
        private static void dependencyCreate()
        {
            Console.WriteLine("Enter DependentTaskId and PreviousTaskId of dependency: ");
            int _dependentTaskId;
            int.TryParse(Console.ReadLine()!, out _dependentTaskId);
            int _previousTaskId;
            int.TryParse(Console.ReadLine()!, out _previousTaskId);
            Dependency newDependency = new(0, _dependentTaskId, _previousTaskId);
            Console.WriteLine(s_dal!.Dependency.Create(newDependency));
        }

        /// <summary>
        /// Delete a dependency by ID.
        /// </summary>
        private static void dependencyDelete()
        {
            try
            {
                Console.WriteLine("Enter ID of dependency: ");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                s_dal!.Dependency.Delete(_id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        /// <summary>
        /// Read a dependency by ID.
        /// </summary>
        private static void dependencyRead()
        {
            Console.WriteLine("Enter ID of dependency: ");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            Console.WriteLine(s_dal!.Dependency.Read(_id));
        }

        /// <summary>
        /// Read all dependencies.
        /// </summary>
        private static void dependencyReadAll()
        {
            s_dal!.Dependency.ReadAll().ForEach(dependency => Console.WriteLine(dependency));
        }

        /// <summary>
        /// Update an existing dependency.
        /// </summary>
        private static void dependencyUpdate()
        {
            try
            {
                Console.WriteLine("Enter ID of dependency: ");
                int _id;
                int.TryParse(Console.ReadLine()!, out _id);
                Dependency? _dependency = s_dal!.Dependency.Read(_id);
                Console.WriteLine("Enter DependentTaskId and PreviousTaskId of dependency: ");
                int _dependentTaskId;
                int.TryParse(Console.ReadLine()!, out _dependentTaskId);
                int _previousTaskId;
                int.TryParse(Console.ReadLine()!, out _previousTaskId);
                if (_dependentTaskId == 0)
                    _dependentTaskId = _dependency!.DependentTaskId;
                if (_previousTaskId == 0)
                    _previousTaskId = _dependency!.PreviousTaskId;
                s_dal!.Dependency.Update(new Dependency(_id, _dependentTaskId, _previousTaskId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}