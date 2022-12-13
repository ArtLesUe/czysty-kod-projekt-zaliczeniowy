using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Services.ApiUserService;

public class ApiUserService: IApiUserServiceInterface
{
    
    public void Delete(ApiUserModel apiUser)
    {
        if (apiUser.Id == 0) return;
        var databaseManagerController = new DatabaseService.DatabaseService();
        databaseManagerController.ExecuteSQL(@"DELETE FROM users WHERE Id = '" + apiUser.Id + @"'");
        databaseManagerController.ExecuteSQL(@"DELETE FROM tokens WHERE UserId = '" + apiUser.Id + @"'");
    }
    
    public void Save(ApiUserModel apiUser)
    {
        var databaseManagerController = new DatabaseService.DatabaseService();
        databaseManagerController.ExecuteSQL(
            @"INSERT INTO users (Name, Surname, Email, PasswordHashed, AboutMe, City, Country, University) VALUES (
          '" + apiUser.Name + @"', 
          '" + apiUser.Surname + @"', 
          '" + apiUser.Email + @"', 
          '" + apiUser.PasswordHashed + @"',
          '" + apiUser.AboutMe + @"', 
          '" + apiUser.City + @"', 
          '" + apiUser.Country + @"', 
          '" + apiUser.University + @"'
        );"
        );
    }

    public void Update(ApiUserModel apiUser)
    {
        var databaseManagerController = new DatabaseService.DatabaseService();
        databaseManagerController.ExecuteSQL(
            @"UPDATE users SET 
          Name='" + apiUser.Name + @"', 
          Surname='" + apiUser.Surname + @"', 
          AboutMe='" + apiUser.AboutMe + @"', 
          City='" + apiUser.City + @"', 
          Country='" + apiUser.Country + @"', 
          University='" + apiUser.University + @"' 
          WHERE Id = '" + apiUser.Id + @"';"
        );
    }

    public void ChangePassword(ApiUserModel apiUser, string newPassword) 
    {
        var databaseManagerController = new DatabaseService.DatabaseService();
        databaseManagerController.ExecuteSQL(
            @"UPDATE users SET 
          PasswordHashed='" + newPassword + @"'
          WHERE Id = '" + apiUser.Id + @"';"
        );
    }
}