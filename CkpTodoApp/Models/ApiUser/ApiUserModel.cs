using System.Text.Json;
using System.Text.Json.Serialization;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Models.ApiUser;

public class ApiUserModel : IApiUserInterface
{
  public ApiUserModel() 
  {
    Id = 0;
    Name = "";
    Surname = "";
    Email = "";
    PasswordHashed = "";
    AboutMe = "";
    City = "";
    Country = "";
    University = "";
  }
    
  public ApiUserModel(int id)
  {
    Id = id;

    var databaseManagerController = new DatabaseService();
    var resultSql = databaseManagerController.ExecuteSQLQuery(
      @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Name', Name,
            'Surname', Surname,
            'Email', Email,
            'PasswordHashed', PasswordHashed,
            'AboutMe', AboutMe,
            'City', City,
            'Country', Country,
            'University', University
          )
        )
        FROM users
        WHERE Id = '" + Id + @"';"
    );

    var userList = JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);

    if ((userList == null) || (userList.Count == 0)) 
      throw new Exception("User with specified Id don't exists in database.");

    Name = userList[0].Name;
    Surname = userList[0].Surname;
    Email = userList[0].Email;
    PasswordHashed = userList[0].PasswordHashed;
    AboutMe = userList[0].AboutMe;
    City = userList[0].City;
    Country = userList[0].Country;
    University = userList[0].University;
  }

  [JsonConstructor]
  public ApiUserModel(int id, string name, string surname, string email, string passwordHashed, string aboutMe,
    string city, string country, string university) 
  {
    Id = id;
    Name = name;
    Surname = surname;
    Email = email;
    PasswordHashed = passwordHashed;
    AboutMe = aboutMe;
    City = city;
    Country = country;
    University = university;
  }

  public int Id { get; set; }

  public string Name { get; set; }

  public string Surname { get; set; }

  public string Email { get; set; }

  public string PasswordHashed { get; set; }

  public string AboutMe { get; set; }

  public string City { get; set; }

  public string Country { get; set; }

  public string University { get; set; }
}