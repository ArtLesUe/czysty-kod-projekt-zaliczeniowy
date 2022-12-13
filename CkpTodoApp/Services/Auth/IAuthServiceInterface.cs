using CkpTodoApp.Responses;

namespace CkpTodoApp.Services.Auth;

public interface IAuthServiceInterface
{
    RootResponse CheckAuth();
}
