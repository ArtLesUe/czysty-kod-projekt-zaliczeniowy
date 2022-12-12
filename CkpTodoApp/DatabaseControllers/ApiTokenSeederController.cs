namespace CkpTodoApp.DatabaseControllers;

public class ApiTokenSeederController : ISeederInterface
{
  private readonly DatabaseManagerController _databaseManagerController;
    
  public ApiTokenSeederController() 
  { 
    _databaseManagerController = new DatabaseManagerController();
  }

  public void MigrateDatabase()
  {
    _databaseManagerController.ExecuteSQL(
      @"CREATE TABLE IF NOT EXISTS 'tokens' (
          'Id' INTEGER NOT NULL UNIQUE,
          'UserId' INTEGER NOT NULL,
          'Token' TEXT NOT NULL UNIQUE,
          PRIMARY KEY ('Id' AUTOINCREMENT)
        );"
    );
  }

  public void SeedDatabase() { }
}