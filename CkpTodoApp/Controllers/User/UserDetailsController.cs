using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User
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

    [HttpGet]
    public List<ApiUserModel>? Get(int id)
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string? jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new List<ApiUserModel>();
      }

      ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new List<ApiUserModel>();
      }

      DatabaseManagerController databaseManagerController = new DatabaseManagerController();
      String resultSql = databaseManagerController.ExecuteSQLQuery(
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

      if (resultSql.Length == 0)
        return new List<ApiUserModel>();

      return JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);
    }
  }
}
