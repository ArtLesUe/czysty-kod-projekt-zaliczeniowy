using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.Event;

namespace CkpTodoApp.Services.Event;

public class EventService : IEventService
{
    private readonly DatabaseServiceController _databaseServiceController = new();

    public void Add(EventModel @event)
    {
        _databaseServiceController.ExecuteSQL(
            @"INSERT INTO events (Title, Description, StartDate, EndDate) VALUES (
            '" + @event.Title + @"', 
            '" + @event.Description + @"',
            '" + @event.StartDate + @"',
            '" + @event.EndDate + @"'
            );"
        );
    }

    public void DeleteEventById(int id)
    {
        _databaseServiceController.ExecuteSQL(
            @"DELETE FROM events WHERE id=" + id + @";"
            );
    }

    public void EditEventById(int id, string? newTitle, string? newDescription, string? newStartDate, string? newEndDate)
    {
        if (newDescription != null)
        {
            _databaseServiceController.ExecuteSQL(
                @"UPDATE tasks SET Description='" + newDescription + @"'WHERE id=" + id + @";"
            );
        }

        if (newTitle != null)
        {
            _databaseServiceController.ExecuteSQL(
                @"UPDATE events SET Title='" + newTitle + @"' WHERE id=" + id + @";"
            );
        }

        if (newStartDate != null)
        {
            _databaseServiceController.ExecuteSQL(
                @"UPDATE events SET StartDate='" + newStartDate + @"' WHERE id=" + id + @";"
            );
        }

        if (newEndDate != null)
        {
            _databaseServiceController.ExecuteSQL(
                @"UPDATE events SET EndDate='" + newEndDate + @"' WHERE id=" + id + @";"
            );
        }
    }
}
