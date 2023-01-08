using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [JsonConstructor]
    public EventModel(int id, string title, string description, string startDate, string endDate)
    {
      Id = id;
      Title = title;
      Description = description;
      StartDate = startDate;
      EndDate = endDate;
    }

    [Key]
    public int Id { get; set; }
        
    public string Title { get; set; }

    public string Description { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }
}