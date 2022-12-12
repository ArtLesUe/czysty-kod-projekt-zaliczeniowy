using CkpTodoApp.Models.ApiUser;

namespace CkpTodoApp.Services.ApiUserService;

public interface IApiUserService
{
    void Delete(ApiUserModel apiUser);
    void Save(ApiUserModel apiUser);
    void Update(ApiUserModel apiUser);
    void ChangePassword(ApiUserModel apiUser, string newPassword);
}