using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/details/{id:int}")]
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
      Response.StatusCode = 401;
      return new List<ApiUserModel>();
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    apiToken.Verify();

    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new List<ApiUserModel>();
    }

    var databaseManagerController = new DatabaseManagerController();
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