using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Event;

[Route(GlobalConstants.BASE_URL_EVENT + "/list")]
[ApiController]
public class EventListController : AuthService
{
  [HttpGet]
  public List<EventModel>? Get()
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return new List<EventModel>();
    }

    var databaseManagerController = new DatabaseService();
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