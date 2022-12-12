using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.DatabaseControllers;

public class ApiTokenController : IMigrantInterface
{
  private readonly DatabaseService _databaseService;
    
  public ApiTokenController() 
  { 
    _databaseService = new DatabaseService();
  }

  public void MigrateDatabase()
  {
    _databaseService.ExecuteSQL(
      @"CREATE TABLE IF NOT EXISTS 'tokens' (
          'Id' INTEGER NOT NULL UNIQUE,
          'UserId' INTEGER NOT NULL,
          'Token' TEXT NOT NULL UNIQUE,
          PRIMARY KEY ('Id' AUTOINCREMENT)
        );"
    );
  }
}