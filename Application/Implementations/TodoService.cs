using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;

namespace TaskTracker.Application.Implementations
{
    public class TodoService : ITodoServiceInterface
    {
        private readonly ITodoItemsRepositoryInterface _todoRepository;
        private readonly INotificationRepositoryInterface _notificationRepository;

        public TodoService()
        {
            _todoRepository = new TodoItemsRepository();
            _notificationRepository = new NotificationRepository();
        }
        public void AddNewTask(TodoItem todoItem)
        {
            var newTask = _todoRepository.CreateTodo(todoItem);
            if (newTask == false)
            {
                Console.WriteLine("create fail");
                return;
            }
            string message = $@"Your Task {todoItem.TaskName} described as
            {todoItem.TaskDescription} with {todoItem.Id} in 
            {todoItem.Category?.Name ?? "No category"} is {todoItem.Status}";

            var notification = new TaskNotification(message, todoItem.Id,
                                              todoItem, todoItem.Status);
            var newNotification = _notificationRepository
                                .CreateNotifications(notification);

            if (newNotification == false)
            {
                Console.WriteLine("no notification added");
                return;
            }
            Console.WriteLine("new notification added");
            Console.WriteLine("task create successfully");
        }

        public void DeleteTask(Guid id)
        {
            var taskToDelete = _todoRepository.Delete(id);
            if (taskToDelete == false)
            {
                Console.WriteLine("delete fail");
                return;
            }
            Console.WriteLine("Delete Successful");
        }

        public void EditTask(Guid id, string taskDescription,
         TaskStatus status, string taskName)
        {

            var task = _todoRepository.GetToDoById(id);
            if (task == null)
            {
                Console.WriteLine("task do not exist");
            }

            var taskToUpdate = _todoRepository.Update(id, taskDescription,
            status, taskName, task?.Category ?? null);

            if (taskToUpdate == false)
            {
                Console.WriteLine("update fail");
                return;
            }

            string message = $@"Your Task {taskName} with
             {id} in {task?.Category?.Name ?? "No category"} is 
             {status}";
            var notification = _notificationRepository.GetNotificationByTodoId(id);

            if (notification == null)
            {
                Console.WriteLine("related notification not found");
                return;
            }
            var notificationToUpdate = _notificationRepository.UpdateNotification
           (notification.Id, status, message);

            if (notificationToUpdate == false)
            {
                Console.WriteLine($@"related reminder notification not updated");
                return;
            }
            Console.WriteLine($@"Update successful");
        }

        public void GetTasksByCategoryId(Guid id)
        {
            int numbering = 1;
            var tasks = _todoRepository.GetTasksByCategoryId(id);
            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("no recorded task");
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
                numbering++;
            }
        }


        public void UpdateOverDueTask()
        {
            var tasks = _todoRepository.GetAllToDoItems();
            var notifications = _notificationRepository.GetAllNotifications();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].DueDate == DateTime.UtcNow)
                {
                    tasks[i].Status = TaskStatus.OverDue;
                    _todoRepository.Update(tasks[i].Id, tasks[i].TaskDescription ?? "No Description",
                                            tasks[i].Status, tasks[i].TaskName, tasks[i].Category);
                }
                for (int j = 0; j < notifications.Count; j++)
                {
                    if (tasks[i].Id == notifications[j].TodoId)
                    {
                        notifications[j].NotificationContent = $@"Your Task {tasks[i].TaskName} with {tasks[i].Id} in
                                                          {tasks[i].Category?.Name ?? "No category"} is {tasks[i].Status}";
                        _notificationRepository.UpdateNotification(tasks[i].Id, tasks[i].Status, notifications[j].NotificationContent);
                    }
                }
            }
        }


        public void ViewAccomplishedTasks()
        {
            int numbering = 1;
            var tasks = _todoRepository.GetDoneToDoItems();
            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("no recorded task");
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
                numbering++;
            }

        }

        public void ViewAllTasks()
        {
            int numbering = 1;
            var tasks = _todoRepository.GetAllToDoItems();
            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("no task recorded");
                Console.WriteLine(new string('-', 40));
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public void ViewOverDueTasks()
        {
            int numbering = 1;
            var tasks = _todoRepository.GetOverDueToDoItems();
            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("record not found");
                Console.WriteLine(new string('-', 40));
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public void ViewTaskById(Guid id)
        {
            var item = _todoRepository.GetToDoById(id);
            if (item == null)
            {
                Console.WriteLine("task not found");
                Console.WriteLine(new string('-', 40));
                return;
            }
            Console.WriteLine($@"  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Description: {item.TaskDescription ?? "N/A"}
                            Task Category   : {item.Category?.Name ?? "N/A"}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            Date Started    : {item.DateStarted?.ToString("yyyy-MM-dd hh:mm:ss tt") ?? "Not set"}
                            Due Date        : {item.DueDate?.ToString("yyyy-MM-dd hh:mm:ss tt")}
                            Date Ended      : {item.CompleteAt?.ToString("yyyy-MM-dd hh:mm:ss tt") ?? "not completed"}");
        }

        public void ViewTaskByUniqueName(string searchItem)
        {
            int numbering = 1;
            var tasks = _todoRepository.GetByDetail(searchItem);
            if (tasks.Count == 0)
            {
                Console.WriteLine("task not found");
                Console.WriteLine(new string('-', 40));
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public void ViewTasksInProgress()
        {
            int numbering = 1;
            var tasks = _todoRepository.GetToDoItemsInProgress();
            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("record not found");
                Console.WriteLine(new string('-', 40));
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public void ViewTodoTasks()
        {
            int numbering = 1;
            var tasks = _todoRepository.GetUnDoneToDoItems();
            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("record not found");
                Console.WriteLine(new string('-', 40));
                return;
            }
            foreach (var item in tasks)
            {
                Console.WriteLine($@"{numbering}.  Task ID : {item.Id}

                            Task Name       : {item.TaskName}
                            Task Status     : {item.Status}
                            Date Created    : {item.CreatedAt:yyyy-MM-dd hh:mm:ss tt}
                            {Environment.NewLine}");
                Console.WriteLine(new string('-', 40));
            }
        }

    }
}