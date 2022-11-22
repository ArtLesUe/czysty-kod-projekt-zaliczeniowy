using CkpTodoApp.DatabaseControllers;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using CkpTodoApp.Models;

namespace CkpTodoApp.Requests
{
  public class UserLoginRequest
  {
    public string? Login { get; set; }

    public string? Password { get; set; }

    public bool Validate()
    {
      return Login != null && 
        Password != null && 
        Login.Length > 0 && 
        Password.Length > 0;
    }

    public int Authenticate()
    {
      MD5 hasher = MD5.Create();
      byte[] inputBytes = Encoding.ASCII.GetBytes(Password);
      byte[] hashBytes = hasher.ComputeHash(inputBytes);

      DatabaseManagerController databaseManagerController = new DatabaseManagerController();
      String resultSql = databaseManagerController.ExecuteSQLQuery(
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
