using CkpTodoApp.Models;

namespace CkpTodoApp.Task;

public interface ITaskManager
{ 
    void Add(TaskModel task);
}