namespace CkpTodoApp.Models;

public interface ITaskInterface
{
    string Title { get; set; }

    string Description { get; set; }
        
    int IsCheck { get; set; }
}