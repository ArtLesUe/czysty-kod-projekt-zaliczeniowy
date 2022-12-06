using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace CkpTodoApp.Requests
{
  public class UserRegisterRequest
  {
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? AboutMe { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? University { get; set; }

    public static bool IsValidEmail(string email)
    {
      try
      {
        MailAddress mail = new MailAddress(email);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public static bool IsEmailNotDuplicated(string email)
    {
      DatabaseManagerController databaseManagerController = new DatabaseManagerController();
      String resultSql = databaseManagerController.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Email', Email
          )
        )
        FROM users
        WHERE Email = '" + email + @"';"
      );

      var userList = JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);
      if ((userList == null) || (userList.Count == 0)) { return true; }

      return false;
    }

    public bool Validate()
    {
      return !string.IsNullOrEmpty(Name) &&
        !string.IsNullOrEmpty(Surname) &&
        !string.IsNullOrEmpty(Email) &&
        !string.IsNullOrEmpty(Password) &&
        IsValidEmail(Email) &&
        IsEmailNotDuplicated(Email);
    }
  }
}
