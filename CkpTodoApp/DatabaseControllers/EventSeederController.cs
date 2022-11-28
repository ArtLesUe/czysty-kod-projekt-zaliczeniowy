using System.Security.Cryptography;
using System.Text;

namespace CkpTodoApp.DatabaseControllers
{
    public class EventSeederController : ISeederInterface
    {
        private DatabaseManagerController _databaseManagerController;

        public EventSeederController()
        {
            _databaseManagerController = new DatabaseManagerController();
        }

        public void MigrateDatabase()
        {
            _databaseManagerController.ExecuteSQL(
                @"CREATE TABLE IF NOT EXISTS 'events' (
                  'Id' INTEGER NOT NULL UNIQUE,
                  'Title' TEXT NOT NULL,
                  'Description' TEXT,
                  'DateStart' DATETIME NOT NULL,
                  'DateEnd' DATETIME NOT NULL,
                  PRIMARY KEY ('Id' AUTOINCREMENT)
                );"
            );
        }

        public void SeedDatabase() { }
    }
}

