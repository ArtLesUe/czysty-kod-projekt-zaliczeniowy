using CkpTodoApp.Constants;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Event;

[Route("api/events/delete/{id:int}")]
[ApiController]
public class DeleteEventController : AuthService
{
    [HttpGet]
    public RootResponse Get(int id)
    {
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
        {
            return rootResponse;
        }

        var eventService = new EventService();
            
        try
        {
            eventService.DeleteEventById(id);
        } 
        catch (Exception)
        {
            Response.StatusCode = StatusCodes.Status406NotAcceptable;
            return new RootResponse { Status = StatusCodeEnum.DeletingNotExistingForbidden.ToString() };
        }

        return new RootResponse { Status = StatusCodeEnum.Deleted.ToString() };
    }
}