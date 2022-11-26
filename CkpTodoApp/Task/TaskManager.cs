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
}