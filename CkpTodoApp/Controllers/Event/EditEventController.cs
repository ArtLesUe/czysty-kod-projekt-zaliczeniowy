using CkpTodoApp.Commons;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Event;

[Route(GlobalConstants.BASE_URL_EVENT + "/edit/{id:int}")]
[ApiController]
public class EditEventController : AuthService
{
    [HttpOptions]
    public void Options()
    {
      return;
    }

    [HttpPatch]
    public RootResponse Patch(EventRequest eventRequest, int id)
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
