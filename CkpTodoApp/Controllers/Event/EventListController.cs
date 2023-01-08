using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Models.Task;
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

    using (var context = new DatabaseFrameworkService())
    {
      List<EventModel> events = context.EventModels.Where(f => f.Id > 0).ToList();
      return events;
    }
  }
}