using System.Security.Cryptography;
using System.Text;

namespace CkpTodoApp.DatabaseControllers;

public class ApiUserController : IMigrantInterface, ISeederInterface
{
  private readonly DatabaseServiceController _databaseServiceController;
    
  public ApiUserController()
  {
    _databaseServiceController = new DatabaseServiceController();
  }

  public void MigrateDatabase()
  {
    _databaseServiceController.ExecuteSQL(
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
    var hasher = MD5.Create();
    var inputBytes = Encoding.ASCII.GetBytes("admin123");
    var hashBytes = hasher.ComputeHash(inputBytes);

    _databaseServiceController.ExecuteSQL(
      @"INSERT INTO users (Name, Surname, Email, PasswordHashed, AboutMe, City, Country, University) 
        SELECT 'Administrator', 'Systemu', 'admin@admin.pl', '" + Convert.ToHexString(hashBytes) + @"',
          'Kilka slow o sobie', 'Katowice', 'Polska', 'Uniwersytet Ekonomiczny w Katowicach'
        WHERE NOT EXISTS (SELECT 1 FROM users WHERE id = 1);"
    );
  }
}