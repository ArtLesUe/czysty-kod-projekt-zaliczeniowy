using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.Task;

namespace CkpTodoApp.Services.Task;

public class TaskManager : ITaskManager
{
    private readonly DatabaseServiceController _databaseServiceController = new();
    
    public void Add(TaskModel task)
    {
        _databaseServiceController.ExecuteSQL(
            @"INSERT INTO tasks (Title, Description, IsCheck) VALUES (
          '" + task.Title + @"', 
          '" + task.Description + @"',
            0
            );"
        );
    }

    public void CheckTaskById(int id)
    {
        _databaseServiceController.ExecuteSQL(
            @"UPDATE tasks SET IsCheck=1 WHERE id=" + id + @";"
        );
    }
    
    public void DeleteTaskById(int id)
    {  
       _databaseServiceController.ExecuteSQL(
           @"DELETE FROM tasks WHERE id=" + id + @";"
           );
    }
    
    public void EditTaskById(int id, string? newTitle, string? newDescription)
    {
        if (newDescription != null)
        {
            _databaseServiceController.ExecuteSQL(
                @"UPDATE tasks SET Description = '" + newDescription + @"' WHERE Id = '" + id + @"';"
            );
        } 
        
        if (newTitle != null)
        {
            _databaseServiceController.ExecuteSQL(
                @"UPDATE tasks SET Title = '" + newTitle + @"' WHERE Id = '" + id + @"';"
            );
        }
    }
}
