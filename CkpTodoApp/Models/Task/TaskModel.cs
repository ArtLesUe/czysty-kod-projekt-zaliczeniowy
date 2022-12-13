using System.Text.Json.Serialization;

namespace CkpTodoApp.Models.Task;

public class TaskModel : ITaskInterface
{
    public TaskModel(string title, string description, int isCheck = 0)
    {
        Id = 0;
        Title = title;
        Description = description;
        IsCheck = isCheck;
    }

    [JsonConstructor] 
    public TaskModel(int id, string title, string description, int isCheck = 0)
    {
        Id = id;
        Title = title;
        Description = description;
        IsCheck = isCheck;
    }

    public int Id { get; }
        
    public string Title { get; set; }

    public string Description { get; set; }
        
    public int IsCheck { get; set; }
}