using CkpTodoApp.DatabaseControllers;
using System.Text.Json;

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

    public void Verify()
    {
      DatabaseManagerController databaseManagerController = new DatabaseManagerController();

      String resultSql = databaseManagerController.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'UserId', UserId,
            'Token', Token
          )
        )
        FROM tokens
        WHERE Token = '" + Token + @"';"
      );

      var tokenList = JsonSerializer.Deserialize<List<ApiTokenModel>>(resultSql);
      if ((tokenList == null) || (tokenList.Count == 0)) { return; }

      Id = tokenList[0].Id;
      UserId = tokenList[0].UserId;
      Token = tokenList[0].Token;
    }

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
