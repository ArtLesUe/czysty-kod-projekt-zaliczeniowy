using CkpTodoApp.Models;

namespace CkpTodoApp.Services.Event;

public interface IEventService
{
    void Add(EventModel @event);

    void DeleteEventById(int id);

    void EditEventById(int id, string? newTitle, string? newDescription, string? newStartDate, string? newEndDate);
}
