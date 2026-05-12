using TaskTracker.Model.Entities;

namespace TaskTracker
{
    public class TodoItem
    {
        public Guid Id{ get; set; }
        public string TaskName{get; set;} = default!;
        public string? TaskDescription{get; set;}
        public Guid? CategoryId{get; set;}
        public Category? Category{get; set;}
        public TaskStatus Status{get; set;}
        public string? PassWord{get; set;}
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
        public DateTime? DateStarted{get; set;}
        public DateTime? CompleteAt{get; set;}
        public DateTime? DueDate{get; set;}
        public bool IsStarted{get; set;} = false;
        public bool IsCompleted{get; set;} = false;
        public TodoItem( Guid id, string taskName,string? taskDescription, 
              Guid? categoryId, Category? category, DateTime? dateStarted, 
              TaskStatus status, string? passWord, DateTime? dueDate, bool isStarted)
            {
                Id = id;
                TaskName = taskName;
                TaskDescription = taskDescription;
                CategoryId = categoryId;
                Category = category;
                Status = status;
                PassWord = passWord;
                DateStarted = dateStarted;
                DueDate = dueDate;
                IsStarted = isStarted;
            }
    }
}