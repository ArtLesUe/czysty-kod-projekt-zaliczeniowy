using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace CkpTodoApp.Controllers
{
  [Route("api/events/list")]
  [ApiController]
  public class TasksEventController : ControllerBase
  {
    [HttpGet]
    public List<EventModel>? Get()
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      var jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new List<EventModel>();
      }

      var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new List<EventModel>();
      }

      var databaseManagerController = new DatabaseManagerController();
      var resultSql = databaseManagerController.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Title', Title,
            'Description', Description,
            'StartDate', StartDate,
            'EndDate', EndDate
          )
        )
        FROM events
        ORDER BY id ASC;"
      );

      return JsonSerializer.Deserialize<List<EventModel>>(resultSql);
    }
  }
}
