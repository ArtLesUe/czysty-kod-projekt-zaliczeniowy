using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/delete/{id}")]
[ApiController]
public class DeleteTaskController : AuthService
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
            taskManager.DeleteTaskById(id);
        } 
        catch (Exception)
        {
            Response.StatusCode = 406;
            return new RootResponse { Status = "deleting-not-existing-forbidden" };
        }

        return new RootResponse { Status = "checked" };
    }
}