using CkpTodoApp.Models;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Event;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers;

[Route("api/events/edit/{id:int}")]
[ApiController]
public class EditEventController : ControllerBase
{
    [HttpPost]
    public RootResponse Post(EventRequest eventRequest, int id)
    {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        string? jsonWebToken = headerValues.FirstOrDefault();

        if (string.IsNullOrEmpty(jsonWebToken))
        {
            Response.StatusCode = 401;
            return new RootResponse { Status = "wrong-auth" };
        }

        ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
        apiToken.Verify();

        if (apiToken.UserId == 0)
        {
            Response.StatusCode = 401;
            return new RootResponse { Status = "wrong-auth" };
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
