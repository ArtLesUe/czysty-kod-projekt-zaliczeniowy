using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/check/{id:int}")]
[ApiController]
public class CheckTaskController : AuthService
{
    [HttpGet]
    public RootResponse Get(int id)
    {
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != "OK")
        {
            return rootResponse;
        }
        
        var taskManager = new TaskService();
        
        try
        {
            taskManager.CheckTaskById(id);
        } 
        catch (Exception)
        {
            Response.StatusCode = 406;
            return new RootResponse { Status = "checking-not-existing-forbidden" };
        }

        return new RootResponse { Status = "checked" };
    }
}