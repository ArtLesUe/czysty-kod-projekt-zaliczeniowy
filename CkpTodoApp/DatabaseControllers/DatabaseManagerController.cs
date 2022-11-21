using CkpTodoApp.Interfaces;
using Microsoft.Data.Sqlite;
using System.Reflection;

namespace CkpTodoApp.DatabaseControllers
{
  public class DatabaseManagerController : IDatabaseManagerInterface
  {
    public DatabaseManagerController() { }

    public string DatabasePath()
    {
      string? folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      string appFolder = folder != null ? folder : Environment.CurrentDirectory;
      string dbFolder = Path.Combine(appFolder, "database");
      Directory.CreateDirectory(dbFolder);
      string dbFile = Path.Combine(dbFolder, "database.db");
      return dbFile;
    }

    public void SeedDatabase()
    {
      ApiUserSeederController apiUserSeederController = new ApiUserSeederController();
      apiUserSeederController.MigrateDatabase();
      apiUserSeederController.SeedDatabase();
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

    public void InitDatabase()
    {
      ExecuteSQL("");
    }
  }
}
