using CkpTodoApp.Models.ApiToken;

namespace CkpTodoApp.Services.ApiTokenService;

public interface IApiTokenInterface
{
    void Save(ApiTokenModel apiToken);
}