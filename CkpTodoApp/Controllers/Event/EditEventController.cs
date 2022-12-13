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
    
        if (rootResponse.Status != "OK")
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
  
        Response.StatusCode = 201;
        return new RootResponse { Status = "OK" };
    }
}
