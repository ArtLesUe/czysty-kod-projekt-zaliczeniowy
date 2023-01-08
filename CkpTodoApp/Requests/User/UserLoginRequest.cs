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
      byte[] inputBytes = Encoding.ASCII.GetBytes(Password ?? "");
      byte[] hashBytes = hasher.ComputeHash(inputBytes);

      using (var context = new DatabaseFrameworkService())
      {
        ApiUserModel user = context.ApiUserModels.Where(f => f.Email == Login).Where(f => f.PasswordHashed == Convert.ToHexString(hashBytes)).First();
        if (user == null) { return 0; }
        return user.Id;
      }
    }
  }
}
