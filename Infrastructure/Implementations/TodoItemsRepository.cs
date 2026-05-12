using System.Text.Json;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Model.Entities;

namespace TaskTracker
{
    public class TodoItemsRepository : ITodoItemsRepositoryInterface
    {
        private void WriteTasksToFile(List<TodoItem> todoItems)
        {
            var jsonConvert = JsonSerializer.Serialize(todoItems, new JsonSerializerOptions
            { WriteIndented = true });
            var folderPath = Path.Combine(Environment.CurrentDirectory, "FileStoragePaths");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, "tasks.json");
            File.WriteAllText(filePath, jsonConvert);
        }

        public static List<TodoItem> ReadTasksFromFile()
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, "FileStoragePaths");

            if (!Directory.Exists(folderPath))
            {
                return new List<TodoItem>();
            }
            string filePath = Path.Combine(folderPath, "tasks.json");

            if (!File.Exists(filePath))
            {
                return new List<TodoItem>();
            }
            string tasksInJson = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(tasksInJson))
            {
                return new List<TodoItem>();
            }
            var todoList = JsonSerializer.Deserialize<List<TodoItem>>(tasksInJson);

            if (todoList == null || !todoList.Any())
            {
                return new List<TodoItem>();
            }
            return todoList;
        }
        public List<TodoItem> GetByDetail(string searchItem)
        {

            var tasks = ReadTasksFromFile();

            var result = new List<TodoItem>();
            if (!tasks.Any() || tasks == null)
            {
                return result;
            }
            foreach (var item in tasks)
            {
                if (item.TaskName != null && item.TaskName.Contains
                    (searchItem, StringComparison.OrdinalIgnoreCase)
                    || item.TaskDescription != null && item.TaskDescription.Contains
                    (searchItem, StringComparison.OrdinalIgnoreCase)
                  )
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public bool CreateTodo(TodoItem items)
        {
            var listOfTasks = new List<TodoItem>();
            if (items == null)
            {
                return false;
            }
            listOfTasks.Add(items);

            WriteTasksToFile(listOfTasks);
            return true;
        }

        public bool Delete(Guid id)
        {
            var totalTasks = ReadTasksFromFile();
            foreach (var item in totalTasks)
            {
                if (item.Id == id)
                {
                    totalTasks.Remove(item);
                    WriteTasksToFile(totalTasks);
                    return true;
                }
            }
            return false;
        }

        public List<TodoItem> GetAllToDoItems()
        {
            var totalTasks = ReadTasksFromFile();


            if (totalTasks == null || !totalTasks.Any())
            {
                return new List<TodoItem>(); ;
            }
            return totalTasks;
        }

        public List<TodoItem> GetDoneToDoItems()
        {
            var totalTasks = ReadTasksFromFile();
            var result = new List<TodoItem>();
            if (totalTasks == null ||
              !totalTasks.Any())
            {
                return result;
            }

            foreach (var item in totalTasks)
            {
                if (item.Status == TaskStatus.Done)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public TodoItem? GetToDoById(Guid id)
        {
            var totalTasks = ReadTasksFromFile();
            foreach (var item in totalTasks)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<TodoItem> GetToDoItemsInProgress()
        {
            var totalTasks = ReadTasksFromFile();
            var result = new List<TodoItem>();
            if (totalTasks == null ||
              !totalTasks.Any())
                return result;

            foreach (var item in totalTasks)
            {
                if (item.Status == TaskStatus.In_Progress)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<TodoItem> GetUnDoneToDoItems()
        {
            var totalTasks = ReadTasksFromFile();
            var result = new List<TodoItem>();
            if (totalTasks == null ||
              !totalTasks.Any())
                return result;
            foreach (var item in totalTasks)
            {
                if (item.Status == TaskStatus.Undone)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<TodoItem> GetOverDueToDoItems()
        {
            var totalTasks = ReadTasksFromFile();
            var result = new List<TodoItem>();
            if (totalTasks == null ||
               !totalTasks.Any())
            {
                return result;
            }
            foreach (var item in totalTasks)
            {
                if (item.Status == TaskStatus.OverDue)
                    result.Add(item);
            }
            return result;

        }
        public bool Update(Guid id, string newTaskName,
        TaskStatus status, string taskDescription, Category? category)
        {
            var taskToUpdate = ReadTasksFromFile();
            foreach (var item in taskToUpdate)
            {
                if (item.Id == id)
                {
                    item.TaskName = newTaskName;
                    item.TaskDescription = taskDescription;
                    item.Status = status;
                    item.Category = category;
                    WriteTasksToFile(taskToUpdate);
                    return true;
                }
            }

            return false;
        }

        public List<TodoItem> GetTasksByCategoryId(Guid id)
        {
            var result = new List<TodoItem>();
            var tasks = ReadTasksFromFile();

            if (tasks == null || !tasks.Any())
            {
                return result;
            }
            foreach (var task in tasks)
            {
                if (task.CategoryId == id)
                {
                    result.Add(task);
                }
            }
            return result;
        }
    }
}