using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;

namespace CkpTodoApp.Event;

public class EventService : IEventService
{
    private readonly DatabaseManagerController _databaseManagerController = new();
    
    public void Add(EventModel @event)
    {
        _databaseManagerController.ExecuteSQL(
            @"INSERT INTO events (Title, Description, StartDate, EndDate) VALUES (
            '" + @event.Title + @"', 
            '" + @event.Description + @"',
            '" + @event.StartDate + @"',
            '" + @event.EndDate + @"',
            );"
        );
    }
}
