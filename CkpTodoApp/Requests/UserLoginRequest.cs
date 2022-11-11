using CkpTodoApp.DatabaseControllers;

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

  }
}
