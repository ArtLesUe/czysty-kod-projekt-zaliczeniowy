using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Services.AuthService;

public class AuthService: ControllerBase, IAuthServiceInterface
{
    public RootResponse CheckAuth()
    {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        var jsonWebToken = headerValues.FirstOrDefault();

        if (string.IsNullOrEmpty(jsonWebToken))
        {
            Response.StatusCode = 401;
            return new RootResponse { Status = StatusCodeEnum.AuthFailed.ToString() };
        }

        using (var context = new DatabaseFrameworkService())
        {
            ApiTokenModel? apiToken = context.ApiTokenModels.Where(f => f.Token == jsonWebToken).First();

            if (apiToken == null) 
            {
                Response.StatusCode = 401;
                return new RootResponse { Status = StatusCodeEnum.AuthFailed.ToString() }; 
            }

            return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
        }
    }

    public int LoggedUserId()
    {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        var jsonWebToken = headerValues.FirstOrDefault();

        if (string.IsNullOrEmpty(jsonWebToken)) return 0;

        using (var context = new DatabaseFrameworkService())
        {
            ApiTokenModel? apiToken = context.ApiTokenModels.Where(f => f.Token == jsonWebToken).First();

            if (apiToken == null)
            {
                return 0;
            }

            return apiToken.UserId;
        }
    }
}
