using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.Event;

[Route("api/events/edit/{id:int}")]
[ApiController]
public class EditEventController : ControllerBase
{
    [HttpPost]
    public RootResponse Post(EventRequest eventRequest, int id)
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

        var eventService = new EventService();
        eventService.EditEventById(
            id, 
            eventRequest.Title, 
            eventRequest.Description,
            eventRequest.StartDate,
            eventRequest.EndDate);
  
        Response.StatusCode = 201;
        return new RootResponse { Status = "OK" };
    }
}
