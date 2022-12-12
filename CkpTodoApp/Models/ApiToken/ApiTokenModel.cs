using System.Text.Json;
using CkpTodoApp.DatabaseControllers;

namespace CkpTodoApp.Models.ApiToken;

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
    var databaseManagerController = new DatabaseServiceController();

    var resultSql = databaseManagerController.ExecuteSQLQuery(
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
    var databaseManagerController = new DatabaseServiceController();
      
    databaseManagerController.ExecuteSQL(
      @"INSERT INTO tokens (UserId, Token) VALUES (
          " + UserId + @",
          '" + Token + @"'
        );"
    );
  }
}