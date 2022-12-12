using CkpTodoApp.Models.Event;

namespace CkpTodoApp.Services.Event;

public interface IEventServiceInterface
{
    void Add(EventModel @event);

    void DeleteEventById(int id);

    void EditEventById(int id, string? newTitle, string? newDescription, string? newStartDate, string? newEndDate);
}
