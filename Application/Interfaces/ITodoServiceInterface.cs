using TaskTracker.Model.Entities;

namespace TaskTracker.Application.Interfaces
{
    public interface ITodoServiceInterface
    {
         void AddNewTask(TodoItem todoItem);
         void EditTask(Guid id, string TaskDescription, 
         TaskStatus Status, string TaskName);
         void DeleteTask(Guid id);
        void GetTasksByCategoryId(Guid id); 
         void ViewTaskById(Guid id);  
         void ViewTaskByUniqueName(string searchItem);  
         void ViewAllTasks();
         void ViewTodoTasks();
         void ViewOverDueTasks();
         void ViewAccomplishedTasks();
         void ViewTasksInProgress(); 
         void UpdateOverDueTask();
    }
}
   