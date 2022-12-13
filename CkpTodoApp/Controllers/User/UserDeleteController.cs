using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/delete/{id:int}")]
[ApiController]
public class UserDeleteController : AuthService
{
  [HttpGet]
  public RootResponse Get(int id)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != "OK")
    {
      return rootResponse;
    }

    if (id == 0)
    {
      Response.StatusCode = 406;
      return new RootResponse { Status = "self-deletion-forbidden" };
    }

    try 
    { 
      var apiUserModel = new ApiUserModel(id);
      var apiUserService = new ApiUserService();
      apiUserService.Delete(apiUserModel);
    } 
    catch (Exception)
    {
      Response.StatusCode = 406;
      return new RootResponse { Status = "deleting-not-existing-forbidden" };
    }

    return new RootResponse { Status = "deleted" };
  }
}