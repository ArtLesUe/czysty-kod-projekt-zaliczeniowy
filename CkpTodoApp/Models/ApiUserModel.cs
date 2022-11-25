using CkpTodoApp.DatabaseControllers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CkpTodoApp.Models
{
  public class ApiUserModel : IApiUserInterface
  {
    public ApiUserModel(int id)
    {
      Id = id;

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
        WHERE Id = '" + Id.ToString() + @"';"
      );

      var userList = JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);

      if ((userList == null) || (userList.Count == 0)) 
        throw new Exception("User with specified Id don't exists in database.");

      Name = userList[0].Name;
      Surname = userList[0].Surname;
      Email = userList[0].Email;
      PasswordHashed = userList[0].PasswordHashed;
    }

    [JsonConstructor]
    public ApiUserModel(int id, string name, string surname, string email, string passwordHashed) 
    {
      Id = id;
      Name = name;
      Surname = surname;
      Email = email;
      PasswordHashed = passwordHashed;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string PasswordHashed { get; set; }
  }
}
