using CkpTodoApp.Commons;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Event;

[Route("api/events/edit/{id:int}")]
[ApiController]
public class EditEventController : AuthService
{
    [HttpPost]
    public RootResponse Post(EventRequest eventRequest, int id)
    {
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
        {
            return rootResponse;
        }

        var eventService = new EventService();
        eventService.EditEventById(
            id, 
            eventRequest.Title, 
            eventRequest.Description,
            eventRequest.StartDate,
            eventRequest.EndDate);
  
        Response.StatusCode = StatusCodes.Status201Created;
        return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
    }
}
