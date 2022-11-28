namespace CkpTodoApp.Models
{
    public interface IEventInterface
    {
        string Title { get; set; }

        string Description { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }
    }
}
