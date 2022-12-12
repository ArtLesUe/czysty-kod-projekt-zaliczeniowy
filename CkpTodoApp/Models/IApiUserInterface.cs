namespace CkpTodoApp.Models;

public interface IApiUserInterface
{
    int Id { get; set; }

    string Name { get; set; }

    string Surname { get; set; }

    string Email { get; set; }

    string PasswordHashed { get; set; }

    string AboutMe { get; set; }

    string City { get; set; }

    string Country { get; set; }

    string University { get; set; }
}