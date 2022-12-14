using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Responses;
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

        var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
        var apiTokenService = new ApiTokenService.ApiTokenService();
        apiTokenService.Verify(apiToken);

        if (apiToken.UserId != 0)
        {
            return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
        }
        
        Response.StatusCode = 401;
        return new RootResponse { Status = StatusCodeEnum.AuthFailed.ToString() };
    }

    public int LoggedUserId()
    {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        var jsonWebToken = headerValues.FirstOrDefault();

        if (string.IsNullOrEmpty(jsonWebToken)) return 0;

        var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
        var apiTokenService = new ApiTokenService.ApiTokenService();
        apiTokenService.Verify(apiToken);

        return apiToken.UserId;
    }
}
