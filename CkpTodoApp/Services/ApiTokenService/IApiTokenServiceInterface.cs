using CkpTodoApp.Models.ApiToken;

namespace CkpTodoApp.Services.ApiTokenService;

public interface IApiTokenInterface
{
    void Verify(ApiTokenModel apiToken);
    void Save(ApiTokenModel apiToken);
}