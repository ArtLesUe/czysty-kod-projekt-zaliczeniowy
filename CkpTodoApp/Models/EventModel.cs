using CkpTodoApp.Commons;

namespace CkpTodoApp.Models
{
    public class EventModel : IEventInterface
    {
        public EventModel(string title, string description, DateTime startDate, DateTime endDate)
        {
            Id = UniqueNumber.GetUniqueNumber();
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

