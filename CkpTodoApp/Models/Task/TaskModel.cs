using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CkpTodoApp.Models.Task;

public class TaskModel : ITaskInterface
{
    public TaskModel() 
    {
        Title = "";
        Description = "";
    }
  
    public TaskModel(string title, string description, bool isChecked = false)
    {
        Id = 0;
        Title = title;
        Description = description;
        IsChecked = isChecked;
    }

    [JsonConstructor] 
    public TaskModel(int id, string title, string description, bool isChecked = false)
    {
        Id = id;
        Title = title;
        Description = description;
        IsChecked = isChecked;
    }

    [Key]
    public int Id { get; set; }
        
    public string Title { get; set; }

    public string Description { get; set; }
        
    public bool IsChecked { get; set; }
}