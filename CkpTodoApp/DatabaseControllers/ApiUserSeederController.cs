using System.Security.Cryptography;
using System.Text;

namespace CkpTodoApp.DatabaseControllers
{
  public class ApiUserSeederController : ISeederInterface
  {
    private DatabaseManagerController databaseManagerController;
    
    public ApiUserSeederController()
    {
      databaseManagerController = new DatabaseManagerController();
    }

    public void MigrateDatabase()
    {
      databaseManagerController.ExecuteSQL(
        @"CREATE TABLE IF NOT EXISTS 'users' (
          'Id' INTEGER NOT NULL UNIQUE,
          'Name' TEXT NOT NULL,
          'Surname' TEXT NOT NULL,
          'Email' TEXT NOT NULL UNIQUE,
          'PasswordHashed' TEXT NOT NULL,
          'AboutMe' TEXT,
          'City' TEXT,
          'Country' TEXT,
          'University' TEXT,
          PRIMARY KEY ('Id' AUTOINCREMENT)
        );"
      );
    }

    public void SeedDatabase()
    {
      MD5 hasher = MD5.Create();
      byte[] inputBytes = Encoding.ASCII.GetBytes("admin123");
      byte[] hashBytes = hasher.ComputeHash(inputBytes);

      databaseManagerController.ExecuteSQL(
        @"INSERT INTO users (Name, Surname, Email, PasswordHashed, AboutMe, City, Country, University) 
        SELECT 'Administrator', 'Systemu', 'admin@admin.pl', '" + Convert.ToHexString(hashBytes) + @"',
          'Kilka slow o sobie', 'Katowice', 'Polska', 'Uniwersytet Ekonomiczny w Katowicach'
        WHERE NOT EXISTS (SELECT 1 FROM users WHERE id = 1);"
      );
    }
  }
}
