using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using TaskTracker.Infrastructure.Interfaces;

namespace TaskTracker.Application.Implementations
{
    public class NotificationRepository : INotificationRepositoryInterface
    {
        public static void WriteNotificationToFile(List<TaskNotification> notifications)
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, "FileStoragePaths");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, "notification.json");
            var json = JsonSerializer.Serialize(notifications, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        public List<TaskNotification> ReadNotificationsFromFile()
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, "Notifications");

            if(Directory.Exists(folderPath))
            {
                return new List<TaskNotification>();
            }
            var filePath = Path.Combine(folderPath, "notification.json");

            if (!File.Exists(filePath))
            {
               return new List<TaskNotification>();
            }

            var stringOfNotifications = File.ReadAllText(filePath);

            if (stringOfNotifications == null)
            {
                return new List<TaskNotification>();
            }
            var listOfNotifications = JsonSerializer.Deserialize
            <List<TaskNotification>>(stringOfNotifications);

            if (listOfNotifications == null || !listOfNotifications.Any())
            {
                return new List<TaskNotification>();
            }
            return listOfNotifications;
        }
        public List<TaskNotification> GetAllNotifications()
        {
            var notificationList = ReadNotificationsFromFile();
            if (notificationList == null || !notificationList.Any())
            {
                return new List<TaskNotification>();
            }
            return notificationList;
        }

        public TaskNotification? GetNotificationById(Guid id)
        {
            var notificationList = ReadNotificationsFromFile();
            foreach (var item in notificationList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public bool CreateNotifications(TaskNotification taskNotifications)
        {
            var notificationList = new List<TaskNotification>();
            if (taskNotifications != null)
            {
                notificationList.Add(taskNotifications);
                WriteNotificationToFile(notificationList);
                return true;
            }
                
            
            return false;
        }

        public List<TaskNotification> GetNotificationsOnTodoTask()
        {
            var notificationList = ReadNotificationsFromFile();
            var result = new List<TaskNotification>();
            if (notificationList == null ||
                !notificationList.Any())
            {
                 return result;
            }
               
            foreach (var item in notificationList)
            {
                if (item.TodoItem.Status == TaskStatus.Undone)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<TaskNotification> GetNotificationsOnOverDueTask()
        {
            var notificationList = ReadNotificationsFromFile();
            var result = new List<TaskNotification>();
            if (notificationList == null ||
                !notificationList.Any())
            {
                 return result;
            }
               
            foreach (var item in notificationList)
            {
                if (item.TodoItem.Status == TaskStatus.OverDue)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<TaskNotification> GetNotificationsOnInProgressTask()
        {
            var notificationList = ReadNotificationsFromFile();
            var result = new List<TaskNotification>();
            if (notificationList == null ||
                !notificationList.Any())
            {
                 return result;
            }
               
            foreach (var item in notificationList)
            {
                if (item.TodoItem.Status == TaskStatus.In_Progress)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public TaskNotification? GetNotificationByTodoId(Guid todoId)
        {
            var notificationList = ReadNotificationsFromFile();
            foreach (var item in notificationList)
            {
                if (item.Id == todoId)
                {
                    return item;
                }
            }
            return null;
        }

        public bool UpdateNotification(Guid todoId, TaskStatus status, string message)
        {
            var notificationList = ReadNotificationsFromFile();
            foreach (var item in notificationList)
            {
                if (item.TodoItem.Id == todoId)
                {
                    item.NotificationType = status;
                    item.NotificationContent = message;
                    WriteNotificationToFile(notificationList);
                    return true;
                }
            }
            return false;
        }
    }
}