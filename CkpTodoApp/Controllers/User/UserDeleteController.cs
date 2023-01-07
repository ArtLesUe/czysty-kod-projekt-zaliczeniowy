using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/delete/{id:int}")]
[ApiController]
public class UserDeleteController : AuthService
{
  [HttpDelete]
  public RootResponse Delete(int id)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return rootResponse;
    }

    if (id == LoggedUserId())
    {
      Response.StatusCode = StatusCodes.Status406NotAcceptable;
      return new RootResponse { Status = StatusCodeEnum.SelfDeletionForbidden.ToString() };
    }

    try 
    { 
      var apiUserModel = new ApiUserModel(id);
      var apiUserService = new ApiUserService();
      apiUserService.Delete(apiUserModel);
    } 
    catch (Exception)
    {
      Response.StatusCode = StatusCodes.Status406NotAcceptable;
      return new RootResponse { Status = StatusCodeEnum.DeletingNotExistingForbidden.ToString() };
    }

    return new RootResponse { Status = StatusCodeEnum.Deleted.ToString() };
  }
}