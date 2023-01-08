using System.Security.Cryptography;
using System.Text;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.DatabaseControllers;

public class ApiUserController : ISeederInterface
{
  public void SeedDatabase()
  {
    var hasher = MD5.Create();
    var inputBytes = Encoding.ASCII.GetBytes("admin123");
    var hashBytes = hasher.ComputeHash(inputBytes);

    using (var context = new DatabaseFrameworkService())
    {
      if (context.ApiUserModels.Find(1) != null) { return; }
      
      ApiUserModel newUser = new ApiUserModel
      {
        Id = 1,
        Name = "Administrator",
        Surname = "Systemu",
        Email = "admin@admin.pl",
        PasswordHashed = Convert.ToHexString(hashBytes),
        AboutMe = "Kilka slow o sobie",
        City = "Katowice",
        Country = "Polska",
        University = "Uniwersytet Ekonomiczny w Katowicach"
      };

      context.ApiUserModels.Add(newUser);
      context.SaveChanges();
    }
  }
}