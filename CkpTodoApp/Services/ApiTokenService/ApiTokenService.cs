using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;

namespace CkpTodoApp.Services.ApiTokenService;

public class ApiTokenService: IApiTokenInterface
{
    public void Verify(ApiTokenModel apiToken)
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
        WHERE Token = '" + apiToken.Token + @"';"
        );

        var tokenList = JsonSerializer.Deserialize<List<ApiTokenModel>>(resultSql);
        if ((tokenList == null) || (tokenList.Count == 0)) { return; }

        apiToken.Id = tokenList[0].Id;
        apiToken.UserId = tokenList[0].UserId;
        apiToken.Token = tokenList[0].Token;
    }

    public void Save(ApiTokenModel apiToken)
    {
        var databaseManagerController = new DatabaseServiceController();
      
        databaseManagerController.ExecuteSQL(
            @"INSERT INTO tokens (UserId, Token) VALUES (
          " + apiToken.UserId + @",
          '" + apiToken.Token + @"'
        );"
        );
    }
}