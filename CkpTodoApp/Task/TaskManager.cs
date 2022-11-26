using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;

namespace CkpTodoApp.Task;

public class TaskManager : ITaskManager
{
    private readonly DatabaseManagerController _databaseManagerController = new();
    
    public void Add(TaskModel task)
    {
        _databaseManagerController.ExecuteSQL(
            @"INSERT INTO tasks (Title, Description, IsCheck) VALUES (
          '" + task.Title + @"', 
          '" + task.Description + @"',
            0
            );"
        );
    }

    public void CheckTaskById(int id)
    {
        _databaseManagerController.ExecuteSQL(
            @"UPDATE tasks SET IsCheck=1 WHERE id=" + id + @";"
        );
    }
    
    public void DeleteTaskById(int id)
    {  
       _databaseManagerController.ExecuteSQL(
           @"DELETE FROM tasks WHERE id=" + id + @";"
           );
    }
}
