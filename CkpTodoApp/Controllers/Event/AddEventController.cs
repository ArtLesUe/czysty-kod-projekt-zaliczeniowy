using System.Globalization;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.Event;

[Route("api/events/add")]
[ApiController]
public class AddEventController : ControllerBase
{
  [HttpPost]
  public RootResponse Post(EventRequest eventRequest)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    var apiTokenService = new ApiTokenService();
    apiTokenService.Verify(apiToken);

    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }
        
    var newEvent = new EventModel(
      eventRequest.Title ?? "", 
      eventRequest.Description ?? "",
      eventRequest.StartDate ?? DateTime.Now.ToString(CultureInfo.CurrentCulture),
      eventRequest.EndDate ?? DateTime.Now.ToString(CultureInfo.CurrentCulture));

    var eventService = new EventService();
    eventService.Add(newEvent);
  
    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}