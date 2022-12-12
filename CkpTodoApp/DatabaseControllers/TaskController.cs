namespace CkpTodoApp.DatabaseControllers;

public class TaskController: IMigrantInterface
{
    private readonly DatabaseServiceController _databaseServiceController;
    
    public TaskController()
    {
        _databaseServiceController = new DatabaseServiceController();
    }

    public void MigrateDatabase()
    {
        _databaseServiceController.ExecuteSQL(
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