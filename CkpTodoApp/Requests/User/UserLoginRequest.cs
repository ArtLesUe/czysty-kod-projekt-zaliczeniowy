using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Requests.User
{
  public class UserLoginRequest
  {
    public string? Login { get; set; }

    public string? Password { get; set; }

    public bool Validate()
    {
      return !string.IsNullOrEmpty(Login) && 
        !string.IsNullOrEmpty(Password);
    }

    public int Authenticate()
    {
      MD5 hasher = MD5.Create();
      byte[] inputBytes = Encoding.ASCII.GetBytes(Password);
      byte[] hashBytes = hasher.ComputeHash(inputBytes);

      DatabaseService databaseService = new DatabaseService();
      String resultSql = databaseService.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Name', Name,
            'Surname', Surname,
            'Email', Email,
            'PasswordHashed', PasswordHashed
          )
        )
        FROM users
        WHERE Email = '" + Login + @"' AND PasswordHashed = '" + 
        Convert.ToHexString(hashBytes) + @"';"
      );

      var userList = JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);
      if ((userList == null) || (userList.Count == 0)) { return 0; }
      int userId = userList[0].Id;

      return userId;
    }
  }
}
