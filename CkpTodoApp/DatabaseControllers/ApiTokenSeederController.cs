namespace CkpTodoApp.DatabaseControllers
{
  public class ApiTokenSeederController : ISeederInterface
  {
    private DatabaseManagerController databaseManagerController;
    
    public ApiTokenSeederController() 
    { 
      databaseManagerController = new DatabaseManagerController();
    }

    public void MigrateDatabase()
    {
      databaseManagerController.ExecuteSQL(
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
}
