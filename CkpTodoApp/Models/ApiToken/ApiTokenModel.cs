using System.ComponentModel.DataAnnotations;

namespace CkpTodoApp.Models.ApiToken;

public class ApiTokenModel : IApiTokenInterface 
{
  public ApiTokenModel()
  {
    Token = "";
  }
  
  public ApiTokenModel(int id, int userId, string token)
  {
    Id = id;
    UserId = userId;
    Token = token;
  }

  [Key]
  public int Id { get; set; }

  public int UserId { get; set; }

  public string Token { get; set; }
}