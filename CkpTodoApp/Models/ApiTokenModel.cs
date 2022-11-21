using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Interfaces;

namespace CkpTodoApp.Models
{
  public class ApiTokenModel : IApiTokenInterface 
  {
    public ApiTokenModel(int id, int userId, string token)
    {
      Id = id;
      UserId = userId;
      Token = token;
    }

    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }

    public void Save()
    {
      DatabaseManagerController databaseManagerController = new DatabaseManagerController();
      databaseManagerController.ExecuteSQL(
        @"INSERT INTO tokens (UserId, Token) VALUES (
          " + UserId.ToString() + @",
          '" + Token + @"'
        );"
      );
    }
  }
}
