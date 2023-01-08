using Microsoft.Data.Sqlite;
using System.Reflection;
using CkpTodoApp.DatabaseControllers;

namespace CkpTodoApp.Services.DatabaseService;

public class DatabaseService : IDatabaseServiceInterface
{
  public string DatabasePathFramework()
  {
    var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    var appFolder = folder ?? Environment.CurrentDirectory;
    var dbFolder = Path.Combine(appFolder, "database");

    Directory.CreateDirectory(dbFolder);

    var dbFile = Path.Combine(dbFolder, "database.db");
    return dbFile;
  }

  public void SeedDatabase()
  {
    var apiUserSeederController = new ApiUserController();
    apiUserSeederController.SeedDatabase();
  }
}