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
            '" + @event.EndDate + @"'
            );"
        );
    }

    public void DeleteEventById(int id)
    {
        _databaseManagerController.ExecuteSQL(
            @"DELETE FROM events WHERE id=" + id + @";"
            );
    }

    public void EditEventById(int id, string? newTitle, string? newDescription, string? newStartDate, string? newEndDate)
    {
        if (newDescription != null)
        {
            _databaseManagerController.ExecuteSQL(
                @"UPDATE tasks SET Description='" + newDescription + @"'WHERE id=" + id + @";"
            );
        }

        if (newTitle != null)
        {
            _databaseManagerController.ExecuteSQL(
                @"UPDATE events SET Title='" + newTitle + @"' WHERE id=" + id + @";"
            );
        }

        if (newStartDate != null)
        {
            _databaseManagerController.ExecuteSQL(
                @"UPDATE events SET StartDate='" + newStartDate + @"' WHERE id=" + id + @";"
            );
        }

        if (newEndDate != null)
        {
            _databaseManagerController.ExecuteSQL(
                @"UPDATE events SET EndDate='" + newEndDate + @"' WHERE id=" + id + @";"
            );
        }
    }
}
