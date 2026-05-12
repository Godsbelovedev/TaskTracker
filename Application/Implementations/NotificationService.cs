using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;

namespace TaskTracker.Application.Implementations
{
    public class NotificationService : INotificationServiceInterface
    {
        private readonly INotificationRepositoryInterface _notification;
        public NotificationService()
        {
            _notification = new NotificationRepository();
        }

        public void GetAllNotifications()
        {
            int numbering = 1;
            var notifications = _notification.GetAllNotifications();
            if(notifications == null || !notifications.Any())
            {
                Console.WriteLine("No notification found");
                Console.WriteLine(new string('-', 40));
                return;
            }
            foreach(var item in notifications)
            {
                Console.WriteLine($@"{numbering}.  
                                  Reminder-Type : {item.NotificationType}
                                       {Environment.NewLine}");

                        Console.WriteLine(new string('-', 40));
                        numbering++;            
            }
        }
//        
        public void GetNotification(Guid id)
        {
            var notification = _notification.GetNotificationById(id);
            if(notification == null)
            {
                Console.WriteLine("Notification not found");
                 Console.WriteLine(new string('-', 40));
                return;
            }
             Console.WriteLine($@"    {notification.Id}
                        Reminder-Type : {notification.NotificationType}
                        Content       : {notification.NotificationContent}");
        }

        public void GetNotificationsOnInProgressTask()
        {
             int numbering = 1;
            var notifications = _notification.GetNotificationsOnInProgressTask();
            if(notifications == null || !notifications.Any())
            {
                Console.WriteLine("No notification found");
                 Console.WriteLine(new string('-', 40));
                return;
            }
            foreach(var item in notifications)
            {
                Console.WriteLine($@"{numbering}.  
                                  Reminder-Type : {item.NotificationType}
                                       {Environment.NewLine}");
                        Console.WriteLine(new string('-', 40));
                        numbering++;         
                        
            }
        }

        public void GetNotificationsOnOverDueTask()
        {
             int numbering = 1;
            var notifications = _notification.GetNotificationsOnOverDueTask();
            if(notifications == null || !notifications.Any())
            {
                Console.WriteLine("No notification found");
                 Console.WriteLine(new string('-', 40));
                return;
            }
            foreach(var item in notifications)
            {
                Console.WriteLine($@"{numbering}.  
                                  Reminder-Type : {item.NotificationType}
                                       {Environment.NewLine}");

                        Console.WriteLine(new string('-', 40));
                        numbering++;               
            }
        }

        public void GetNotificationsOnTodoTask()
        {
            int numbering = 1;
            var notifications = _notification.GetNotificationsOnTodoTask();
            if(notifications == null || !notifications.Any())
            {
                Console.WriteLine("No notification found");
                 Console.WriteLine(new string('-', 40));
                return;
            }
            foreach(var item in notifications)
            { Console.WriteLine($@"{numbering}.  
                                  Reminder-Type : {item.NotificationType}
                                       {Environment.NewLine}");

                        Console.WriteLine(new string('-', 40));
                        numbering++;         
            }
        }

    }
}