using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.DatabaseControllers;

public class TaskController: IMigrantInterface
{
    private readonly DatabaseService _databaseService;
    
    public TaskController()
    {
        _databaseService = new DatabaseService();
    }

    public void MigrateDatabase()
    {
        _databaseService.ExecuteSQL(
            @"CREATE TABLE IF NOT EXISTS 'tasks' (
                  'Id' INTEGER NOT NULL UNIQUE,
                  'Title' TEXT NOT NULL,
                  'Description' TEXT NOT NULL,
                  'IsCheck' INTEGER NOT NULL,
                  PRIMARY KEY ('Id' AUTOINCREMENT)
                );"
        );
    }
}