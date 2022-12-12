using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.DatabaseControllers;

public class EventController : IMigrantInterface
{
    private readonly DatabaseService _databaseService;

    public EventController()
    {
        _databaseService = new DatabaseService();
    }

    public void MigrateDatabase()
    {
        _databaseService.ExecuteSQL(
            @"CREATE TABLE IF NOT EXISTS 'events' (
                  'Id' INTEGER NOT NULL UNIQUE,
                  'Title' TEXT NOT NULL,
                  'Description' TEXT,
                  'StartDate' TEXT NOT NULL,
                  'EndDate' TEXT,
                  PRIMARY KEY ('Id' AUTOINCREMENT)
                );"
        );
    }
}