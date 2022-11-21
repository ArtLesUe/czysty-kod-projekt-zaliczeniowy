using CkpTodoApp.Interfaces;

namespace CkpTodoApp.Models
{
  public class ApiUserModel : IApiUserInterface
  {
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
