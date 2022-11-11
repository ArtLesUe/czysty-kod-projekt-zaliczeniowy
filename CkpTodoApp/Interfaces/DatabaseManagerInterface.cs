namespace CkpTodoApp.Interfaces
{
  public interface IDatabaseManagerInterface
  {
    void InitDatabase();


    void ExecuteSQL(string sql);

    string DatabasePath();
  }
}
