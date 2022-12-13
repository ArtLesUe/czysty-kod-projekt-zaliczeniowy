
namespace CkpTodoApp.Models.Event;

public class EventModel : IEventInterface
{
    public EventModel(string title, string description, string startDate, string endDate)
    {
        Id = 0;
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public int Id { get; }
        
    public string Title { get; set; }

    public string Description { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }
}