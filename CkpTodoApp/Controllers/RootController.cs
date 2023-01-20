using CkpTodoApp.Commons;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers;

[Route("/")]
[ApiController]
public class RootController : ControllerBase
{
  [HttpOptions]
  public void Options()
  {
    return;
  }

  [HttpGet]
  public RootResponse Get()
  {
    return new RootResponse { 
      Status = StatusCodeEnum.Ok.ToString()
    };
  }
}