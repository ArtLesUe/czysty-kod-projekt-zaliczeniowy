using CkpTodoApp.Models.Task;

namespace CkpTodoApp.Services.Task;

public interface ITaskServiceInterface
{ 
    void Add(TaskModel task);
    
    void CheckTaskById(int id);
    
    void DeleteTaskById(int id);
    
    void EditTaskById(int id, string? newTitle, string? newDescription);
}
