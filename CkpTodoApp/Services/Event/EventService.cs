using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Services.Event;

public class EventService : IEventServiceInterface
{
    private readonly DatabaseService.DatabaseService _databaseService = new();

    public void Add(EventModel @event)
    {
        _databaseService.ExecuteSQL(
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
        _databaseService.ExecuteSQL(
            @"DELETE FROM events WHERE id=" + id + @";"
            );
    }

    public void EditEventById(int id, string? newTitle, string? newDescription, string? newStartDate, string? newEndDate)
    {
        if (newDescription != null)
        {
            _databaseService.ExecuteSQL(
                @"UPDATE events SET Description = '" + newDescription + @"' WHERE id = '" + id + @"';"
            );
        }

        if (newTitle != null)
        {
            _databaseService.ExecuteSQL(
                @"UPDATE events SET Title = '" + newTitle + @"' WHERE id = '" + id + @"';"
            );
        }

        if (newStartDate != null)
        {
            _databaseService.ExecuteSQL(
                @"UPDATE events SET StartDate = '" + newStartDate + @"' WHERE id = '" + id + @"';"
            );
        }

        if (newEndDate != null)
        {
            _databaseService.ExecuteSQL(
                @"UPDATE events SET EndDate = '" + newEndDate + @"' WHERE id = '" + id + @"';"
            );
        }
    }
}
