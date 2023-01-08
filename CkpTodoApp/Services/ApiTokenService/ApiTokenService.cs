using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Services.ApiTokenService;

public class ApiTokenService: IApiTokenInterface
{
    public void Save(ApiTokenModel apiToken)
    {
        using (var context = new DatabaseFrameworkService())
        {
            ApiTokenModel newToken = new ApiTokenModel
            {
                Id = 0,
                UserId = apiToken.UserId,
                Token = apiToken.Token
            };

            context.ApiTokenModels.Add(newToken);
            context.SaveChanges();
        }
    }
}