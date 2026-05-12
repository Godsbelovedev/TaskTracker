namespace TaskTracker
{
    public class TaskNotification
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public string NotificationContent{get; set;} = default!;
        public TaskStatus NotificationType{ get; set;}
        public Guid TodoId{get; set;}
        public TodoItem TodoItem{ get; set; } = default!;
        public DateTime DateSent { get; set; } = DateTime.Now;
        public bool IsRead {get; set; } = false;
        public TaskNotification(string notificationContent, Guid todoId,
        TodoItem todoItem, TaskStatus notificationType)
        {
            NotificationContent = notificationContent;
            TodoId = todoId;
            TodoItem = todoItem;
            NotificationType = notificationType;
        }
    }
}