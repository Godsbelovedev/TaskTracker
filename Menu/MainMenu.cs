using System.Runtime.CompilerServices;
using TaskTracker.Application.Implementations;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;

namespace TaskTracker.Menu
{
    public class MainMenu
    {
        private readonly INotificationRepositoryInterface _notification;
        private readonly ITodoServiceInterface _todoService;
        public MainMenu()
        {
            _notification = new NotificationRepository();
            _todoService = new TodoService();
        }
        TaskMenu taskMenu = new TaskMenu();
        CategoryMenu categoryMenu = new CategoryMenu();
        NotificationMenu notificationMenu = new NotificationMenu();
        public void Start()
        {
            int result;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"welcome {Environment.UserName}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@" 
                ████████╗ █████╗ ███████╗██╗  ██╗
                ╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝
                   ██║   ███████║███████╗█████╔╝ 
                   ██║   ██╔══██║╚════██║██╔═██╗ 
                   ██║   ██║  ██║███████║██║  ██╗
                   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
                        TASK TRACKER SYSTEM
                         ");
                Console.ResetColor();
                

                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Stay organized. Stay focused.");
                Console.WriteLine("-----------------------------------------");
                var overdueTasksNotification = _notification.GetNotificationsOnOverDueTask();
                var undoneTasksNotification = _notification.GetNotificationsOnTodoTask();
                var TasksInProgressNotification = _notification.GetNotificationsOnInProgressTask();
                _todoService.UpdateOverDueTask();
          
                Console.WriteLine("task started");
                if (overdueTasksNotification.Count() > 0)
                {
                    Console.WriteLine($@"you have {overdueTasksNotification.Count} Overdue Task Notification(s)");
                }
                if (undoneTasksNotification.Count() > 0)
                {
                    Console.WriteLine($@"you have {undoneTasksNotification.Count} undone Task Notification(s)");
                }
                if (TasksInProgressNotification.Count() > 0)
                {  
                    Console.WriteLine($@"you have {TasksInProgressNotification.Count} Task In Progress Notification(s)");
                }
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1 ➤ Manage Tasks");
                Console.WriteLine("2 ➤ Manage Categories");
                Console.WriteLine("3 ➤ Manage Reminder");
                Console.WriteLine("4 ➤ Exit");
                Console.WriteLine("-----------------------------------------");

                //ConsoleHelper.ValidateChoice(1, 3);
                result = ConsoleHelper.ValidateChoice(1, 4);

                switch (result)
                {
                    case 1:
                        ManageTasks();
                        break;

                    case 2:
                        ManageCategories();
                        break;

                    case 3:
                        ManageReminder();
                        break;
                    case 4:
                        Console.WriteLine("Bye! 🤦‍♂️🫡");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                }
            }


            //     int choice;
            //     bool isConfirm;
            //    do
            //    {
            //     Console.WriteLine("Enter a choice between 1-3 ");
            //      string input = Console.ReadLine();
            //      isConfirm = int.TryParse(input, out choice);
            //      if(!isConfirm || choice < 1 || choice > 3)
            //         {
            //             Console.WriteLine("invalid input, select from options 1-3");
            //         }
            //    } while (!isConfirm || choice < 1 || choice > 3);


        }
        private void ManageTasks()
        {
            taskMenu.Start();
        }
        private void ManageCategories()
        {
            categoryMenu.Start();
        }
        private void ManageReminder()
        {
            notificationMenu.Start();
        }
    }
}