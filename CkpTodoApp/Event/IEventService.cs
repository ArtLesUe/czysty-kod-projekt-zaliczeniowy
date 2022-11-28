using CkpTodoApp.Models;

namespace CkpTodoApp.Event;

public interface IEventService
{ 
    void Add(EventModel @event);
}
