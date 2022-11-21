namespace CkpTodoApp.Interfaces
{
  public interface IApiUserInterface
  {
    int Id { get; set; }
    
    string Name { get; set; }

    string Surname { get; set; }

    string Email { get; set; }

    string PasswordHashed { get; set; }
  }
}
