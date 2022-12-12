using Microsoft.Data.Sqlite;
using System.Reflection;

namespace CkpTodoApp.DatabaseControllers
{
  public class DatabaseManagerController : IDatabaseManagerInterface
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
      var apiUserSeederController = new ApiUserSeederController();
      apiUserSeederController.MigrateDatabase();
      apiUserSeederController.SeedDatabase(); //TODO

      var apiTokenSeederController = new ApiTokenSeederController();
      apiTokenSeederController.MigrateDatabase();
      apiTokenSeederController.SeedDatabase();
      
      var taskSeederController = new TaskSeederController();
      taskSeederController.MigrateDatabase();
      taskSeederController.SeedDatabase();

      var eventSeederController = new EventSeederController();
      eventSeederController.MigrateDatabase();
      eventSeederController.SeedDatabase();
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
      String sqlResult = "";

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
}
