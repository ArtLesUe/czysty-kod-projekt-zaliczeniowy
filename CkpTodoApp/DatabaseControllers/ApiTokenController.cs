namespace CkpTodoApp.DatabaseControllers;

public class ApiTokenController : IMigrantInterface
{
  private readonly DatabaseServiceController _databaseServiceController;
    
  public ApiTokenController() 
  { 
    _databaseServiceController = new DatabaseServiceController();
  }

  public void MigrateDatabase()
  {
    _databaseServiceController.ExecuteSQL(
      @"CREATE TABLE IF NOT EXISTS 'tokens' (
          'Id' INTEGER NOT NULL UNIQUE,
          'UserId' INTEGER NOT NULL,
          'Token' TEXT NOT NULL UNIQUE,
          PRIMARY KEY ('Id' AUTOINCREMENT)
        );"
    );
  }
}