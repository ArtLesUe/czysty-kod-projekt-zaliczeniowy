using CkpTodoApp.Models.Task;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CkpTodoApp.Services.Task;

public class TaskService : ITaskServiceInterface
{
    public void Add(TaskModel task)
    {
        using (var context = new DatabaseFrameworkService())
        {
            TaskModel newTask = new TaskModel
            {
                Title = task.Title,
                Description = task.Description,
                IsChecked = false
            };

            context.TaskModels.Add(newTask);
            context.SaveChanges();
        }
    }

    public void CheckTaskById(int id)
    {
        using (var context = new DatabaseFrameworkService())
        {
            TaskModel? task = context.TaskModels.Find(id);
            if (task == null) { return; }
            task.IsChecked = true;
            context.TaskModels.Update(task);
            context.SaveChanges();
        }
    }
    
    public void DeleteTaskById(int id)
    {
        using (var context = new DatabaseFrameworkService())
        {
            TaskModel? task = context.TaskModels.Find(id);
            if (task == null) { return; }
            context.TaskModels.Remove(task);
            context.SaveChanges();
        }
    }
    
    public void EditTaskById(int id, string? newTitle, string? newDescription)
    {
        using (var context = new DatabaseFrameworkService())
        {
            TaskModel? task = context.TaskModels.Find(id);
            if (task == null) { return; }

            task.Description = string.IsNullOrEmpty(newDescription) ? task.Description : newDescription;
            task.Title = string.IsNullOrEmpty(newTitle) ? task.Title : newTitle;

            context.TaskModels.Update(task);
            context.SaveChanges();
        }
    }
}
