namespace CkpTodoApp.Models;

public interface IEventInterface
{
    string Title { get; set; }

    string Description { get; set; }

    string StartDate { get; set; }

    string EndDate { get; set; }
}