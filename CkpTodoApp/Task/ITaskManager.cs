using CkpTodoApp.Models;

namespace CkpTodoApp.Task;

public interface ITaskManager
{ 
    void Add(TaskModel task);
    
    void CheckTaskById(int id);
    
    void DeleteTaskById(int id);
    
    void EditTaskById(int id, string? newTitle, string? newDescription);
}