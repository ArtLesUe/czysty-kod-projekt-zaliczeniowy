using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/details/{id:int}")]
[ApiController]
public class UserDetailsController : ControllerBase
{
  [HttpGet]
  public List<ApiUserModel>? Get(int id)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = StatusCodes.Status401Unauthorized;
      return new List<ApiUserModel>();
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    var apiTokenService = new ApiTokenService();
    apiTokenService.Verify(apiToken);
    
    if (apiToken.UserId == 0)
    {
      Response.StatusCode = StatusCodes.Status401Unauthorized;
      return new List<ApiUserModel>();
    }

    var databaseManagerController = new DatabaseService();
    var resultSql = databaseManagerController.ExecuteSQLQuery(
      @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Name', Name,
            'Surname', Surname,
            'Email', Email,
            'PasswordHashed', '',
            'AboutMe', AboutMe,
            'City', City,
            'Country', Country,
            'University', University
          )
        )
        FROM users
        WHERE Id = '" + id.ToString() + @"';"
    );

    return resultSql.Length == 0 ? new List<ApiUserModel>() : JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);
  }
}