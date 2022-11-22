namespace CkpTodoApp.DatabaseControllers
{
    public interface ISeederInterface
    {
        void MigrateDatabase();

        void SeedDatabase();
    }
}
