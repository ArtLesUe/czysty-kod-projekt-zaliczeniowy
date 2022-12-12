using CkpTodoApp.Models;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/check/{id:int}")]
[ApiController]
public class CheckTaskController : ControllerBase
{
    [HttpGet]
    public RootResponse Get(int id)
    {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        string? jsonWebToken = headerValues.FirstOrDefault();

        if (string.IsNullOrEmpty(jsonWebToken))
        {
            Response.StatusCode = 401;
            return new RootResponse { Status = "auth-failed" };
        }

        ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
        apiToken.Verify();

        if (apiToken.UserId == 0)
        {
            Response.StatusCode = 401;
            return new RootResponse { Status = "auth-failed" };
        }

        var taskManager = new TaskManager();
        
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