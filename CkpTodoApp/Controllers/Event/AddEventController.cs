using System.Globalization;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Event;

[Route("api/events/add")]
[ApiController]
public class AddEventController : AuthService
{
  [HttpPost]
  public RootResponse Post(EventRequest eventRequest)
  {  
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return rootResponse;
    }
        
    var newEvent = new EventModel(
      eventRequest.Title ?? "", 
      eventRequest.Description ?? "",
      eventRequest.StartDate ?? DateTime.Now.ToString(CultureInfo.CurrentCulture),
      eventRequest.EndDate ?? DateTime.Now.ToString(CultureInfo.CurrentCulture));

    var eventService = new EventService();
    eventService.Add(newEvent);
  
    Response.StatusCode = StatusCodes.Status201Created;
    return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
  }
}