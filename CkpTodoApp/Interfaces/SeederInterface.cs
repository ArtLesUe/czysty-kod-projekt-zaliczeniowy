namespace CkpTodoApp.Interfaces
{
  public interface ISeederInterface
  {
    void MigrateDatabase();

    void SeedDatabase();
  }
}
