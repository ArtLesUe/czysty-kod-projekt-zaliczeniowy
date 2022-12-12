using Microsoft.Data.Sqlite;
using System.Reflection;
using CkpTodoApp.DatabaseControllers;

namespace CkpTodoApp.Services.DatabaseService;

public class DatabaseService : IDatabaseServiceInterface
{
  public string DatabasePath()
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
    apiUserSeederController.MigrateDatabase();
    apiUserSeederController.SeedDatabase();

    var apiTokenSeederController = new ApiTokenController();
    apiTokenSeederController.MigrateDatabase();
      
    var taskSeederController = new TaskController();
    taskSeederController.MigrateDatabase();

    var eventSeederController = new EventController();
    eventSeederController.MigrateDatabase();
  }

  public void ExecuteSQL(string sql)
  {
    var connection = new SqliteConnection("Data Source=" + DatabasePath());
    var command = connection.CreateCommand();

    connection.Open();
    command.CommandText = sql;
    command.ExecuteNonQuery();
    connection.Close();
  }

  public string ExecuteSQLQuery(string sql)
  {
    var connection = new SqliteConnection("Data Source=" + DatabasePath());
    var command = connection.CreateCommand();
    var sqlResult = "";

    connection.Open();
    command.CommandText = sql;

    using (var reader = command.ExecuteReader())
    {
      while (reader.Read())
      {
        sqlResult = reader.GetString(0);
      }
    }

    connection.Close();
    return sqlResult;
  }

  public void InitDatabase() => ExecuteSQL("");
}