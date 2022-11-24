using System.Security.Cryptography;
using System.Text;

namespace CkpTodoApp.DatabaseControllers
{
    public class TaskSeederController: ISeederInterface
    {
        private DatabaseManagerController _databaseManagerController;
    
        public TaskSeederController()
        {
            _databaseManagerController = new DatabaseManagerController();
        }

        public void MigrateDatabase()
        {
            _databaseManagerController.ExecuteSQL(
                @"CREATE TABLE IF NOT EXISTS 'tasks' (
                  'Id' INTEGER NOT NULL UNIQUE,
                  'Title' TEXT NOT NULL,
                  'Description' TEXT NOT NULL,
                  'IsCheck' INTEGER NOT NULL,
                  PRIMARY KEY ('Id' AUTOINCREMENT)
                );"
            );
        }

        public void SeedDatabase() { }
    }
}

