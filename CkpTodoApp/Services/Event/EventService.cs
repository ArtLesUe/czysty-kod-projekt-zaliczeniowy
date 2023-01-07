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
        _databaseService.ExecuteSQL(
            @"UPDATE events SET " +
            @"Description = " + (string.IsNullOrEmpty(newDescription) ? @"Description, " : "'" + newDescription + "', ") +
            @"Title = " + (string.IsNullOrEmpty(newTitle) ? @"Title, " : "'" + newTitle + "', ") +
            @"StartDate = " + (string.IsNullOrEmpty(newStartDate) ? @"StartDate, " : "'" + newStartDate + "', ") +
            @"EndDate = " + (string.IsNullOrEmpty(newEndDate) ? @"EndDate " : "'" + newEndDate + "' ") +
            @"WHERE id = '" + id + @"';"
        );
    }
}
