using TaskTracker.Application.Implementations;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Model.Entities;

namespace TaskTracker.Menu
{
    public class TaskMenu
    {
        private readonly ICategoryServiceInterface _categoryService;
        private readonly ITodoServiceInterface _todoService;
        private readonly ITodoItemsRepositoryInterface _todoItemsRepository;
        private readonly ICategoryRepositoryInterface _categoryRepository;

        public TaskMenu()
        {
            _categoryService = new CategoryService();
            _todoService = new TodoService();
            _todoItemsRepository = new TodoItemsRepository();
            _categoryRepository = new CategoryRepository();
        }
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("WELCOME TO TASK MENU");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1 ➤ Create A Task");
                Console.WriteLine("2 ➤ View All Tasks");
                Console.WriteLine("3 ➤ View Overdue Tasks");
                Console.WriteLine("4 ➤ View Todo Tasks");
                Console.WriteLine("5 ➤ View Done Tasks");
                Console.WriteLine("6 ➤ View Tasks In-Progress");
                Console.WriteLine("7 ➤ Search A Task");
                Console.WriteLine("0 ➤ return to main Menu");
                Console.WriteLine("-----------------------------------------");

                int choice = ConsoleHelper.ValidateChoice(0, 7);

                switch (choice)
                {
                    case 1:
                        CreateNewTask();
                        break;

                    case 2:
                        ViewAllTasks();
                        break;

                    case 3:
                        ViewOverdueTasks();
                        break;

                    case 4:
                        ViewTodoTasks();
                        break;

                    case 5:
                        ViewDoneTasks();
                        break;

                    case 6:
                        ViewTasksInProgress();
                        break;

                    case 7:
                        SearchATask();
                        break;

                    case 0:
                        return;

                    default:
                        break;
                }
            }

        }


        private void CreateNewTask()
        {

            Guid id = Guid.NewGuid();

            Console.WriteLine("Input your Task Name");
            string taskName = Console.ReadLine()?.Trim() ?? "";

            while (string.IsNullOrWhiteSpace(taskName))
            {
                Console.WriteLine("input a valid name. task name cant be empty");
                taskName = Console.ReadLine()?.Trim() ?? "";
            }

            Console.WriteLine("input a description");
            string taskDescription = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(taskDescription))
            {
                taskDescription = "no description";
            }

            Category? category = null;

            Console.WriteLine(@"Do you wish to classify task in a category? 
                               Select '1' for 'YES' or '2' for 'NO'
                                1. Yes
                                2. No");
Console.WriteLine("category registration is about to start");
            int response = ConsoleHelper.ValidateChoice(1, 2);

            if (response == 1)
            {
                if (_categoryRepository.GetAllCategories().Count == 0)
                {
                    Console.WriteLine(@" no available category,
                        Do you wish to create a category?.
                        Select '1' for 'YES' or '2' for 'NO'
                                1. Yes
                                2. No");

                    int choice = ConsoleHelper.ValidateChoice(1, 2);
                    if (choice == 1)
                    {
                        Console.WriteLine("input category name (required)");
                        string categoryName = Console.ReadLine()?.Trim() ?? "";

                        while (string.IsNullOrWhiteSpace(categoryName))
                        {
                            Console.WriteLine("input a valid name. category name cant be empty");
                            categoryName = Console.ReadLine()?.Trim() ?? "";
                        }

                        Console.WriteLine("input category description or press enter to proceed(optional)");
                        string categoryDescription = Console.ReadLine()?.Trim() ?? "";

                        if (string.IsNullOrWhiteSpace(categoryDescription))
                        {
                            categoryDescription = "NO DESCRIPTION'";
                            Console.WriteLine("category set to 'NO DESCRIPTION'");
                        }

                        Console.WriteLine("input category password or press enter to proceed (optional)");
                        string categoryPassword = Console.ReadLine()?.Trim() ?? "";

                        if (string.IsNullOrWhiteSpace(categoryPassword))
                        {
                            categoryPassword = "";
                            Console.WriteLine("no password set");
                        }

                        category = new Category(categoryName, categoryDescription, categoryPassword);
                        _categoryService.AddCategory(category);
                    }
                    else
                    {
                        Console.WriteLine("task classified under no category");
                    }
                }
                else
                {
                    Console.WriteLine($"select a category from 1-{_categoryRepository.GetAllCategories().Count}");

                    _categoryService.GetAllCategories();

                    int option = ConsoleHelper.ValidateChoice(1, _categoryRepository.GetAllCategories().Count);

                    category = _categoryRepository.GetAllCategories()[option - 1];
                }
            }
            else
            {
                Console.WriteLine("task classified under no category");
            }
            bool isStarted = false;
            TaskStatus status = TaskStatus.Undone;
            Console.WriteLine(@" Do you wish to start now?
                                  Select '1' for 'YES' or '2' for 'NO'
                                    1. Yes
                                    2. No");
            int resp = ConsoleHelper.ValidateChoice(1, 2);
            if (resp == 1)
            {
                status = TaskStatus.In_Progress;
                isStarted = true;
            }

            Console.WriteLine("input task password or press enter to proceed (optional)");
            string taskPassword = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(taskPassword))
            {
                taskPassword = "";
                Console.WriteLine("no password set");
            }
            DateTime? dateStarted = null;
            if (resp == 1)
            {
                dateStarted = DateTime.UtcNow;
            }
            DateTime? dueDate = null;
            Console.WriteLine(@" Select form of duration.
                                  Select '1' for 'Hour' or '2' for 'Day'...
                                    1. Hour
                                    2. Day
                                    3. Month
                                    4. Year");

            int inp = ConsoleHelper.ValidateChoice(1, 4);

            Console.WriteLine(@" Select duration. 1, 2, 3, 4....
                                   to make 1 month, 2 years, 1 hour...");
            bool isConfirmedDuration = int.TryParse(Console.ReadLine(), out int duration);
            while (!isConfirmedDuration && duration < 0)
            {
                Console.WriteLine("invalid input, please input a valid whole number");
                isConfirmedDuration = int.TryParse(Console.ReadLine(), out duration);
            }
            if (inp == 1 && dateStarted.HasValue)
            {
                dueDate = dateStarted.Value.AddHours(duration);
                Console.WriteLine($@"you selected {duration} Hour(s) Task");
            }
            else if (inp == 2 && dateStarted.HasValue)
            {
                dueDate = dateStarted.Value.AddDays(duration);
                Console.WriteLine($@"you selected {duration} Day(s) Task");
            }
            else if (inp == 3 && dateStarted.HasValue)
            {
                dueDate = dateStarted.Value.AddMonths(duration);
                Console.WriteLine($@"you selected {duration} Month(s) Task");
            }
            else if (inp == 4 && dateStarted.HasValue)
            {
                dueDate = dateStarted.Value.AddYears(duration);
                Console.WriteLine($@"you selected {duration} Month(s) Task");
            }
            var task = new TodoItem(id,
                                    taskName,
                                    taskDescription,
                                    category?.Id,
                                    category,
                                    dateStarted,
                                    status,
                                    taskPassword,
                                    dueDate,
                                    isStarted);
            _todoService.AddNewTask(task);
        }
        private void ViewAllTasks()
        {
            _todoService.ViewAllTasks();
            if (_todoItemsRepository.GetAllToDoItems().Count > 0)
            {
                Console.WriteLine($@"do you wish to view or delete a task?
                    
                        SELECT:
                            1 to VIEW
                            2 to DELETE
                            OR 0 to EXIT");
                var tasks = _todoItemsRepository.GetAllToDoItems();
                int option = ConsoleHelper.ValidateChoice(0, 2);
                if (option == 1)
                {
                    if (tasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {tasks.Count} to VIEW task");
                    }
                    else if (tasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{tasks.Count}
                                        the task you want to VIEW");
                    }

                    int choice = ConsoleHelper.ValidateChoice(1, tasks.Count);

                    if (tasks[choice - 1].PassWord != null)
                    {
                        bool hasPassword = ConsoleHelper.AuthorizeTaskViewal(tasks[choice - 1].Id);
                        if (!hasPassword)
                        {
                            Console.WriteLine("VIEWAL UNAUTHORIZE");
                        }
                        else
                        {
                            _todoService.ViewTaskById(tasks[choice - 1].Id);
                        }
                    }

                }

                else if (option == 2)
                {
                    Console.WriteLine($@"SELECT from 1-{tasks.Count}
                                        the task you want to DELETE");
                    int choice = ConsoleHelper.ValidateChoice(1, tasks.Count);

                    _todoService.DeleteTask(tasks[choice - 1].Id);
                }
                else
                {
                    return;
                }
            }
        }

        private void ViewOverdueTasks()
        {
            _todoService.ViewOverDueTasks();
            var tasks = _todoItemsRepository.GetOverDueToDoItems();
            if (tasks.Count > 0)
            {
                Console.WriteLine($@"do you wish to view, delete or update a task?
                 
                    SELECT:
                        1 to VIEW
                     OR 0 to EXIT");

                int option = ConsoleHelper.ValidateChoice(0, 1);
                if (option == 1)
                {
                    if (tasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {tasks.Count} to VIEW task");
                    }
                    else if (tasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{tasks.Count}
                                        the task you want to VIEW");
                    }
                    int choice = ConsoleHelper.ValidateChoice(1, tasks.Count);

                    _todoService.ViewTaskById(tasks[choice - 1].Id);
                }
                else
                {
                    Console.WriteLine("returning to TASK MENU");
                    return;
                }
            }

        }

        private void ViewTodoTasks()
        {
            _todoService.ViewTodoTasks();
            var todotasks = _todoItemsRepository.GetUnDoneToDoItems();

            if (todotasks.Count > 0)
            {
                string taskName = "";
                string taskDescription;
                TaskStatus taskStatus = TaskStatus.Undone;

                Console.WriteLine($@"INPUT from options 0-3
                to UPDATE, VIEW, DELETE or EXIT from TASKS
                
                INPUT:
                    '1' to VIEW
                    '2' to UPDATE
                    '3' to DELETE
                    '0' to EXIT to TASK-MENU");

                int option = ConsoleHelper.ValidateChoice(0, 3);
                if (option == 1)
                {
                    if (todotasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {todotasks.Count} to VIEW task");
                    }
                    else if (todotasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{todotasks.Count}
                                        the task you want to VIEW");
                    }
                    int viewChoice = ConsoleHelper.ValidateChoice(1, todotasks.Count);
                    _todoService.ViewTaskById(todotasks[viewChoice - 1].Id);

                }
                else if (option == 2)
                {
                    if (todotasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {todotasks.Count} to UPDATE task");
                    }
                    else if (todotasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{todotasks.Count}
                                        the task you want to UPDATE");
                    }
                    int updateChoice = ConsoleHelper.ValidateChoice(1, todotasks.Count);

                    Console.WriteLine($@"Do you want to change task name?
                                       SELECT:
                                                1 to change name.
                                                2 to retain existing name ");
                    int updateNameChoice = ConsoleHelper.ValidateChoice(1, 2);
                    if (updateNameChoice == 1)
                    {
                        Console.WriteLine("input new name");
                        taskName = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(taskName))
                        {
                            Console.WriteLine("invalid input, Name can not be empty");
                            taskName = Console.ReadLine();
                        }
                        Console.WriteLine($@"task name updated to {taskName}");
                    }
                    else if (updateNameChoice == 2 || string.IsNullOrWhiteSpace(taskName))
                    {
                        taskName = todotasks[updateChoice - 1].TaskName;
                        Console.WriteLine("existing task name retained");
                    }

                    Console.WriteLine($@"Do you wish to update Task Description?
                                       SELECT:
                                                Input new description or click enter
                                                to retain existing description");
                    taskDescription = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(taskDescription))
                    {
                        taskDescription = todotasks[updateChoice - 1].TaskDescription ?? "No description";
                    }
                    Console.WriteLine($@"Do you wish to update task status?
                                        SELECT:
                                               '1' for IN-PROGRESS
                                               '2' to retain existing status");
                    int taskStatusChoice = ConsoleHelper.ValidateChoice(1, 2);
                    if(taskStatusChoice == 1 && todotasks[updateChoice - 1].Status == TaskStatus.Undone)
                    {
                        taskStatus = TaskStatus.In_Progress;
                        Console.WriteLine($@"IN-PROGRESS selected as new STATUS");
                    }
                   
                    if(taskStatusChoice == 2 )
                    {
                        taskStatus =  todotasks[updateChoice - 1].Status;
                        Console.WriteLine("existing Task Status is retained");
                    }

                        _todoService.EditTask(todotasks[updateChoice - 1].Id, taskDescription,
                                              taskStatus, taskName);
                }
                else if(option == 3)
                {
                    if (todotasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {todotasks.Count} to DELETE task");
                    }
                    else if (todotasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{todotasks.Count}
                                        the task you want to DELETE");
                    }
                    int viewChoice = ConsoleHelper.ValidateChoice(1, todotasks.Count);
                    _todoService.DeleteTask(todotasks[viewChoice - 1].Id);
                }
                else
                {
                    Console.WriteLine("returning to task menu");
                    Thread.Sleep(2000);
                    return;
                }
            }
        }
        private void ViewDoneTasks()
        {
            _todoService.ViewAccomplishedTasks();
            var tasks = _todoItemsRepository.GetDoneToDoItems();
            if (tasks.Count > 0)
            {
                Console.WriteLine($@"do you wish to view, delete or update a task?
                 
                    SELECT:
                        1 to VIEW
                        OR 0 to EXIT");

                int option = ConsoleHelper.ValidateChoice(0, 1);
                if (option == 1)
                {
                    Console.WriteLine($@"SELECT from 1-{tasks.Count}
                                       the task you want to view");
                    int choice = ConsoleHelper.ValidateChoice(1, tasks.Count);

                    _todoService.ViewTaskById(tasks[choice - 1].Id);
                }
                else
                {
                    Console.WriteLine("returning to TASK MENU");
                    return;
                }
            }
        }
        private void ViewTasksInProgress()
        {
            _todoService.ViewTasksInProgress();
            var tasksInprogress = _todoItemsRepository.GetToDoItemsInProgress();
            if (tasksInprogress.Count > 0)
            {
                string taskName = "";
                string taskDescription;
                TaskStatus taskStatus = TaskStatus.Undone;

                Console.WriteLine($@"INPUT from options 0-3
                to UPDATE, VIEW, DELETE or EXIT from TASKS
                
                INPUT:
                    '1' to VIEW
                    '2' to UPDATE
                    '3' to DELETE
                    '0' to EXIT to TASK-MENU");

                int option = ConsoleHelper.ValidateChoice(0, 3);
                if (option == 1)
                {
                    if (tasksInprogress.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {tasksInprogress.Count} to VIEW task");
                    }
                    else if (tasksInprogress.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{tasksInprogress.Count}
                                        the task you want to VIEW");
                    }
                    int viewChoice = ConsoleHelper.ValidateChoice(1, tasksInprogress.Count);
                    _todoService.ViewTaskById(tasksInprogress[viewChoice - 1].Id);

                }
                else if (option == 2)
                {
                    if (tasksInprogress.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {tasksInprogress.Count} to UPDATE task");
                    }
                    else if (tasksInprogress.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{tasksInprogress.Count}
                                        the task you want to UPDATE");
                    }
                    int updateChoice = ConsoleHelper.ValidateChoice(1, tasksInprogress.Count);

                    Console.WriteLine($@"Do you want to change task name?
                                       SELECT:
                                                1 to change name.
                                                2 to retain existing name ");
                    int updateNameChoice = ConsoleHelper.ValidateChoice(1, 2);
                    if (updateNameChoice == 1)
                    {
                        Console.WriteLine("input new name");
                        taskName = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(taskName))
                        {
                            Console.WriteLine("invalid input, Name can not be empty");
                            taskName = Console.ReadLine();
                        }
                        Console.WriteLine($@"task name updated to {taskName}");
                    }
                    else if (updateNameChoice == 2 || string.IsNullOrWhiteSpace(taskName))
                    {
                        taskName = tasksInprogress[updateChoice - 1].TaskName;
                        Console.WriteLine("existing task name retained");
                    }

                    Console.WriteLine($@"Do you wish to update Task Description?
                                       SELECT:
                                                Input new description or click enter
                                                to retain existing description");
                    taskDescription = Console.ReadLine();// ?? "no description";
                    if (string.IsNullOrWhiteSpace(taskDescription))
                    {
                        taskDescription = tasksInprogress[updateChoice - 1].TaskDescription ?? "No description";
                    }
                    Console.WriteLine($@"Do you wish to update task status?
                                        SELECT:
                                               '1' for DONE
                                               '2' to retain existing status");
                    int taskStatusChoice = ConsoleHelper.ValidateChoice(1, 3);
                    if(taskStatusChoice == 1 && tasksInprogress[updateChoice - 1].Status == TaskStatus.In_Progress)
                    {
                        taskStatus = TaskStatus.Done;
                        Console.WriteLine($@"IN-PROGRESS selected as new STATUS");
                    }
                    if(taskStatusChoice == 2)
                    {
                        taskStatus = tasksInprogress[updateChoice - 1].Status;
                    }

                        _todoService.EditTask(tasksInprogress[updateChoice - 1].Id, taskDescription,
                                              taskStatus, taskName);
                }
                else if(option == 3)
                {
                    if (tasksInprogress.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {tasksInprogress.Count} to DELETE task");
                    }
                    else if (tasksInprogress.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{tasksInprogress.Count}
                                        the task you want to DELETE");
                    }
                    int viewChoice = ConsoleHelper.ValidateChoice(1, tasksInprogress.Count);
                    _todoService.DeleteTask(tasksInprogress[viewChoice - 1].Id);
                }
                else
                {
                    Console.WriteLine("returningto task menu");
                    Thread.Sleep(2000);
                    return;
                }
            }
        }
        private void SearchATask()
        {
            Console.WriteLine("make an input to search");
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($@"input can't be empty
                            SELECT 1. to proceed to SEARCH
                            SELECT 2. to EXIT SEARCH");
                int searchChoice = ConsoleHelper.ValidateChoice(1, 2);
                if (searchChoice == 1)
                {
                    Console.WriteLine("make an input to search");
                    input = Console.ReadLine();
                }
                else
                {
                    return;
                }
            }
            _todoService.ViewTaskByUniqueName(input);
            var searchedTasks = _todoItemsRepository.GetByDetail(input);
           if (searchedTasks.Count > 0)
            {
                string taskName = "";
                string? taskDescription;
                TaskStatus taskStatus = TaskStatus.OverDue;

                Console.WriteLine($@"INPUT from options 0-3
                to UPDATE, VIEW, DELETE or EXIT from TASKS
                
                INPUT:
                    '1' to VIEW
                    '2' to UPDATE
                    '3' to DELETE
                    '0' to EXIT to TASK-MENU");

                int option = ConsoleHelper.ValidateChoice(0, 3);
                if (option == 1)
                {
                    if (searchedTasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {searchedTasks.Count} to VIEW task");
                    }
                    else if (searchedTasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{searchedTasks.Count}
                                        the task you want to VIEW");
                    }
                    int viewChoice = ConsoleHelper.ValidateChoice(1, searchedTasks.Count);
                    _todoService.ViewTaskById(searchedTasks[viewChoice - 1].Id);

                }
                else if (option == 2)
                {
                    if (searchedTasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {searchedTasks.Count} to UPDATE task");
                    }
                    else if (searchedTasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{searchedTasks.Count}
                                        the task you want to UPDATE");
                    }
                    int updateChoice = ConsoleHelper.ValidateChoice(1, searchedTasks.Count);

                    Console.WriteLine($@"Do you want to change task name?
                                       SELECT:
                                                1 to change name.
                                                2 to retain existing name ");
                    int updateNameChoice = ConsoleHelper.ValidateChoice(1, 2);
                    if (updateNameChoice == 1)
                    {
                        Console.WriteLine("input new name");
                        taskName = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(taskName))
                        {
                            Console.WriteLine("invalid input, Name can not be empty");
                            taskName = Console.ReadLine();
                        }
                        Console.WriteLine($@"task name updated to {taskName}");
                    }
                    else if (updateNameChoice == 2 || string.IsNullOrWhiteSpace(taskName))
                    {
                        taskName = searchedTasks[updateChoice - 1].TaskName;
                        Console.WriteLine("existing task name retained");
                    }

                    Console.WriteLine($@"Do you wish to update Task Description?
                                       SELECT:
                                                Input new description or click enter
                                                to retain existing description");
                    taskDescription = Console.ReadLine() ?? "no description";
                    if (string.IsNullOrWhiteSpace(taskDescription))
                    {
                        taskDescription = searchedTasks[updateChoice - 1].TaskDescription ?? "No description";
                    }
                    Console.WriteLine($@"Do you wish to update task status?
                                        SELECT:
                                               '1' for IN-PROGRESS
                                               '2' for DONE
                                               '3' to retain existing status");
                    int taskStatusChoice = ConsoleHelper.ValidateChoice(1, 3);
                    if(taskStatusChoice == 1 && searchedTasks[updateChoice - 1].Status == TaskStatus.Undone)
                    {
                        taskStatus = TaskStatus.In_Progress;
                        Console.WriteLine($@"IN-PROGRESS selected as new STATUS");
                    }
                    else
                    {
                        Console.WriteLine("only undone tasks can be changed to IN-PROGRESS");
                    }
                    if(taskStatusChoice == 2 && searchedTasks[updateChoice - 1].Status == TaskStatus.In_Progress)
                    {
                        taskStatus = TaskStatus.Done;
                    }
                    else
                    {
                        Console.WriteLine("Only task in progress can be changed to DONE");
                    }

                        _todoService.EditTask(searchedTasks[updateChoice - 1].Id, taskDescription,
                                              taskStatus, taskName);
                }
                else if(option == 3)
                {
                    if (searchedTasks.Count == 1)
                    {
                        Console.WriteLine($@"SELECT {searchedTasks.Count} to DELETE task");
                    }
                    else if (searchedTasks.Count > 1)
                    {
                        Console.WriteLine($@"SELECT from 1-{searchedTasks.Count}
                                        the task you want to DELETE");
                    }
                    int viewChoice = ConsoleHelper.ValidateChoice(1, searchedTasks.Count);
                    _todoService.DeleteTask(searchedTasks[viewChoice - 1].Id);
                }
                else
                {
                    Console.WriteLine("returningto task menu");
                    Thread.Sleep(2000);
                    return;
                }
            }
        }
    }
}
