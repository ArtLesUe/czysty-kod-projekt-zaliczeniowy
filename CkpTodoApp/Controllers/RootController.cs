using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers;

[Route("/")]
[ApiController]
public class RootController : ControllerBase
{
  [HttpGet]
  public RootResponse Get()
  {
    return new RootResponse { 
      Status = "ok" 
    };
  }
}