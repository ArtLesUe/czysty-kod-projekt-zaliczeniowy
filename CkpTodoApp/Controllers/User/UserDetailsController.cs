using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/details/{id}")]
  [ApiController]
  public class UserDetailsController : ControllerBase
  {
    private readonly ILogger<UserDetailsController> _logger;

    public UserDetailsController(ILogger<UserDetailsController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpGet]
    public ApiUserModel? Get(int id)
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string? jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new ApiUserModel();
      }

      ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new ApiUserModel();
      }

      if (apiToken.UserId == id)
      {
        Response.StatusCode = 406;
        return new ApiUserModel();
      }

      DatabaseManagerController databaseManagerController = new DatabaseManagerController();
      String resultSql = databaseManagerController.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Name', Name,
            'Surname', Surname,
            'Email', Email,
            'PasswordHashed', PasswordHashed,
            'AboutMe', AboutMe,
            'City', City,
            'Country', Country,
            'University', University,
          )
        )
        FROM users
        WHERE Id = '" + id.ToString() + @"';"
      );

      return JsonSerializer.Deserialize<ApiUserModel>(resultSql);
    }
  }
}