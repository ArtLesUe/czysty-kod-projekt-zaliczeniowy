namespace CkpTodoApp.Interfaces
{
  public interface IDatabaseManagerInterface
  {
    void InitDatabase();

    void SeedDatabase();

    void ExecuteSQL(string sql);

    string DatabasePath();
  }
}
