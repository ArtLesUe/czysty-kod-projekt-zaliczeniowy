using CkpTodoApp.Models;

namespace CkpTodoApp.Event;

public interface IEventService
{
    void Add(EventModel @event);

    void DeleteEventById(int id);

    void EditEventById(int id, string? newTitle, string? newDescription, DateTime? newStartDate, DateTime? newEndDate);
}
