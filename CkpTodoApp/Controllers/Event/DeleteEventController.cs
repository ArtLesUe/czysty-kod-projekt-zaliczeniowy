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
    
        if (rootResponse.Status != "OK")
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
            Response.StatusCode = 406;
            return new RootResponse { Status = "deleting-not-existing-forbidden" };
        }

        return new RootResponse { Status = "checked" };
    }
}