using TaskTracker.Application.Implementations;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;

namespace TaskTracker.Menu
{
    public class NotificationMenu
    {
        private readonly INotificationServiceInterface _notificationService;
        private readonly INotificationRepositoryInterface _notificationRepository;
        private readonly ITodoItemsRepositoryInterface _todoRepository;

        public NotificationMenu()
        {
            _notificationService = new NotificationService();
            _notificationRepository = new NotificationRepository();
            _todoRepository = new TodoItemsRepository();
        }
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("WELCOME TO TASK MENU");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1 ➤ View All Notification");
                Console.WriteLine("2 ➤ View Notifications on Undone Task");
                Console.WriteLine("3 ➤ View Notifications On Overdue Tasks");
                Console.WriteLine("4 ➤ View Notifications Tasks In-Progress");
                Console.WriteLine("0 ➤ return to main Menu");
                Console.WriteLine("-----------------------------------------");

                int choice = ConsoleHelper.ValidateChoice(0, 4);

                switch (choice)
                {
                    case 1:
                        ViewAllNotifications();
                        break;

                    case 2:
                        ViewNotificationsOnTodoTask();
                        break;

                    case 3:
                        ViewNotificationsOnOverDueTask();
                        break;

                    case 4:
                        ViewNotificationsOnInProgressTask();
                        break;

                    case 0:
                        return;

                }
            }
        }
        private void ViewAllNotifications()
        {
            _notificationService.GetAllNotifications();
            Console.WriteLine($@"input option between 1-2 to 
                                1. VIEW A NOTIFICATION
                             OR 2. RETURN TO MENU");  
            int choice = ConsoleHelper.ValidateChoice(1, 2);
            var notifications = _notificationRepository.GetAllNotifications();
            if(choice == 1)
            {
                _notificationService.GetNotification(notifications[choice-1].Id);
            }
            else
            {
                return;
            }

        }
        private void ViewNotificationsOnTodoTask()
        {
             _notificationService.GetNotificationsOnTodoTask();
            Console.WriteLine($@"input option between 1-2 to 
                                1. VIEW A NOTIFICATION
                             OR 2. RETURN TO MENU");  
            int choice = ConsoleHelper.ValidateChoice(1, 2);
            var notifications = _notificationRepository.GetNotificationsOnTodoTask();
            if(choice == 1)
            {
                _notificationService.GetNotification(notifications[choice-1].Id);
            }
            else
            {
                return;
            }
        }
        private void ViewNotificationsOnOverDueTask()
        {
             _notificationService.GetNotificationsOnOverDueTask();
            Console.WriteLine($@"input option between 1-2 to 
                                1. VIEW A NOTIFICATION
                             OR 2. RETURN TO MENU");  
            int choice = ConsoleHelper.ValidateChoice(1, 2);
            var notifications = _notificationRepository.GetNotificationsOnOverDueTask();
            if(choice == 1)
            {
                _notificationService.GetNotification(notifications[choice-1].Id);
            }
            else
            {
                return;
            }
        }
        private void ViewNotificationsOnInProgressTask()
        {
             _notificationService.GetNotificationsOnInProgressTask();
            Console.WriteLine($@"input option between 1-2 to 
                                1. VIEW A NOTIFICATION
                             OR 2. RETURN TO MENU");  
            int choice = ConsoleHelper.ValidateChoice(1, 2);
            var notifications = _notificationRepository.GetNotificationsOnInProgressTask();
            if(choice == 1)
            {
                _notificationService.GetNotification(notifications[choice-1].Id);
            }
            else
            {
                return;
            }
        }
    }
}
