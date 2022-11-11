using CkpTodoApp.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;

namespace CkpTodoApp.DatabaseControllers
{
  public class ApiUserSeederController : ISeederInterface
  {
    private DatabaseManagerController databaseManagerController;
    
    public ApiUserSeederController()
    {
      databaseManagerController = new DatabaseManagerController();
    }

    public void MigrateDatabase()
    {
      databaseManagerController.ExecuteSQL(
        @"CREATE TABLE IF NOT EXISTS 'users' (
          'Id' INTEGER NOT NULL UNIQUE,
          'Name' TEXT NOT NULL,
          'Surname' TEXT NOT NULL,
          'Email' TEXT NOT NULL UNIQUE,
          'PasswordHashed' TEXT NOT NULL,
          PRIMARY KEY ('Id' AUTOINCREMENT)
        );"
      );
    }
}
