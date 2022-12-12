namespace CkpTodoApp.Services.DatabaseService;

public interface IDatabaseServiceInterface
{
    void InitDatabase();

    void SeedDatabase();

    void ExecuteSQL(string sql);

    string ExecuteSQLQuery(string sql);

    string DatabasePath();
}