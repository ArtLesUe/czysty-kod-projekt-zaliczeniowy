namespace CkpTodoApp.Models
{
    public interface ITaskInterface
    {
        string Title { get; set; }

        string Description { get; set; }
        
        bool IsCheck { get; set; }
    }
}

