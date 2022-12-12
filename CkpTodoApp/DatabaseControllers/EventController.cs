namespace CkpTodoApp.DatabaseControllers;

public class EventController : IMigrantInterface
{
    private readonly DatabaseServiceController _databaseServiceController;

    public EventController()
    {
        _databaseServiceController = new DatabaseServiceController();
    }

    public void MigrateDatabase()
    {
        _databaseServiceController.ExecuteSQL(
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