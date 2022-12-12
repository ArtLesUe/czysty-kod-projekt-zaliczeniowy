namespace CkpTodoApp.Models.ApiToken;

public interface IApiTokenInterface
{
    int Id { get; set; }

    int UserId { get; set; }

    string Token { get; set; }

    void Save();
}