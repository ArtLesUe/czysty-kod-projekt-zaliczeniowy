using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Services.Auth;

public class AuthService: ControllerBase
{
    public RootResponse CheckAuth()
    {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        var jsonWebToken = headerValues.FirstOrDefault();

            if (string.IsNullOrEmpty(jsonWebToken))
        {
            Response.StatusCode = 401;
            return new RootResponse { Status = "auth-failed" };
        }

        var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
        var apiTokenService = new ApiTokenService.ApiTokenService();
        apiTokenService.Verify(apiToken);

        if (apiToken.UserId != 0)
        {
            return new RootResponse { Status = "OK" };
        }
        
        Response.StatusCode = 401;
        return new RootResponse { Status = "auth-failed" };
    }
}
