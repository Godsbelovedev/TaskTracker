using TaskTracker.Model.Entities;

namespace TaskTracker.Infrastructure.Interfaces
{
    public interface ITodoItemsRepositoryInterface
    {
        bool CreateTodo(TodoItem items);
        //TodoItems CurrentTodoItems();
        bool Update(Guid id, string taskDescription, TaskStatus status,
                    string newTaskName, Category? category);
        bool Delete(Guid id);
        // bool ChangeStatus(TaskStatus Status, Guid id);
        TodoItem? GetToDoById(Guid id);
        List<TodoItem>GetTasksByCategoryId(Guid id);

        List<TodoItem>GetByDetail(string searchItem);
        List<TodoItem> GetAllToDoItems();
        List<TodoItem> GetDoneToDoItems();
        List<TodoItem> GetUnDoneToDoItems();  
        List<TodoItem> GetToDoItemsInProgress(); 
        List<TodoItem> GetOverDueToDoItems(); 
    }
}
