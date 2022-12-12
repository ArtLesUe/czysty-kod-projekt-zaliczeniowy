using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Services.Task;

public class TaskService : ITaskServiceInterface
{
    private readonly DatabaseService.DatabaseService _databaseService = new();
    
    public void Add(TaskModel task)
    {
        _databaseService.ExecuteSQL(
            @"INSERT INTO tasks (Title, Description, IsCheck) VALUES (
          '" + task.Title + @"', 
          '" + task.Description + @"',
            0
            );"
        );
    }

    public void CheckTaskById(int id)
    {
        _databaseService.ExecuteSQL(
            @"UPDATE tasks SET IsCheck=1 WHERE id=" + id + @";"
        );
    }
    
    public void DeleteTaskById(int id)
    {  
       _databaseService.ExecuteSQL(
           @"DELETE FROM tasks WHERE id=" + id + @";"
           );
    }
    
    public void EditTaskById(int id, string? newTitle, string? newDescription)
    {
        if (newDescription != null)
        {
            _databaseService.ExecuteSQL(
                @"UPDATE tasks SET Description = '" + newDescription + @"' WHERE Id = '" + id + @"';"
            );
        } 
        
        if (newTitle != null)
        {
            _databaseService.ExecuteSQL(
                @"UPDATE tasks SET Title = '" + newTitle + @"' WHERE Id = '" + id + @"';"
            );
        }
    }
}
