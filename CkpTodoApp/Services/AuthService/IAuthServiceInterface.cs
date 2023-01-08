using CkpTodoApp.Responses;

namespace CkpTodoApp.Services.AuthService;

public interface IAuthServiceInterface
{
    RootResponse CheckAuth();

    int LoggedUserId();
}
