namespace TaskTracker.Infrastructure.Interfaces
{
    public interface INotificationRepositoryInterface
    {
         bool CreateNotifications(TaskNotification taskNotifications);
         List<TaskNotification> GetAllNotifications();
         List<TaskNotification> GetNotificationsOnTodoTask();
         List<TaskNotification> GetNotificationsOnOverDueTask();
         List<TaskNotification> GetNotificationsOnInProgressTask();
         TaskNotification? GetNotificationById(Guid id);
         TaskNotification? GetNotificationByTodoId(Guid todoId);
         bool UpdateNotification(Guid todoId, TaskStatus status, string message);
    }
}