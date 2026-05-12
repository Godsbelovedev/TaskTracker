namespace TaskTracker.Application.Interfaces
{
    public interface INotificationServiceInterface
    {
        void GetAllNotifications();
        void GetNotificationsOnTodoTask();
        void GetNotificationsOnOverDueTask();
        void GetNotificationsOnInProgressTask();
        void GetNotification(Guid id);
        
    }
}