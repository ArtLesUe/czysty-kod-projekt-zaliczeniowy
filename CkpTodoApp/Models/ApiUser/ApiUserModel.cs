using System.ComponentModel.DataAnnotations;
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
    using (var context = new DatabaseFrameworkService())
    {
      ApiUserModel? user = context.ApiUserModels.Find(id);

      if (user == null)
        throw new Exception("User with specified Id don't exists in database.");

      Id = id;
      Name = user.Name;
      Surname = user.Surname;
      Email = user.Email;
      PasswordHashed = user.PasswordHashed;
      AboutMe = user.AboutMe;
      City = user.City;
      Country = user.Country;
      University = user.University;
    }    
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

  [Key]
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