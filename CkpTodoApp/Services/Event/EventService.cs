using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CkpTodoApp.Services.Event;

public class EventService : IEventServiceInterface
{
    public void Add(EventModel @event)
    {
        using (var context = new DatabaseFrameworkService())
        {
            EventModel newEvent = new EventModel
            {
                Title = @event.Title,
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            };

            context.EventModels.Add(newEvent);
            context.SaveChanges();
        }
    }

    public void DeleteEventById(int id)
    {
        using (var context = new DatabaseFrameworkService())
        {
            EventModel? eventItem = context.EventModels.Find(id);
            if (eventItem == null) { return; }
            context.EventModels.Remove(eventItem);
            context.SaveChanges();
        }
    }

    public void EditEventById(int id, string? newTitle, string? newDescription, string? newStartDate, string? newEndDate)
    {
        using (var context = new DatabaseFrameworkService())
        {
            EventModel? eventItem = context.EventModels.Find(id);
            if (eventItem == null) { return; }

            eventItem.Description = string.IsNullOrEmpty(newDescription) ? eventItem.Description : newDescription;
            eventItem.Title = string.IsNullOrEmpty(newTitle) ? eventItem.Title : newTitle;
            eventItem.StartDate = string.IsNullOrEmpty(newStartDate) ? eventItem.StartDate : newStartDate;
            eventItem.EndDate = string.IsNullOrEmpty(newEndDate) ? eventItem.EndDate : newEndDate;

            context.EventModels.Update(eventItem);
            context.SaveChanges();
        }
    }
}
