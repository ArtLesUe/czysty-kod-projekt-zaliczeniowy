using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CkpTodoApp.Controllers
{
  [Route("/")]
  [ApiController]
  public class RootController : ControllerBase
  {
    private readonly ILogger<RootController> _logger;

    public RootController(ILogger<RootController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpGet]
    public RootResponse Get()
    {
      return new RootResponse { 
        Status = "ok" 
      };
    }
  }
}
