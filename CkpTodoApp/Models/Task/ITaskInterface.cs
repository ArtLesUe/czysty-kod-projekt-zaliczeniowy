namespace CkpTodoApp.Models.Task;

public interface ITaskInterface
{
    public int Id { get; set; }

    string Title { get; set; }

    string Description { get; set; }
        
    bool IsChecked { get; set; }
}