using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Services.ApiUserService;

public class ApiUserService: IApiUserServiceInterface
{
    public void Delete(ApiUserModel apiUser)
    {
        if (apiUser.Id == 0) return;

        using (var context = new DatabaseFrameworkService())
        {
            context.ApiUserModels.Remove(apiUser);
            context.SaveChanges();

            List<ApiTokenModel> tokens = context.ApiTokenModels.Where(f => f.UserId == apiUser.Id).ToList();
            context.ApiTokenModels.RemoveRange(tokens);
            context.SaveChanges();
        }
    }
    
    public void Save(ApiUserModel apiUser)
    {
        using (var context = new DatabaseFrameworkService())
        {
            ApiUserModel newUser = new ApiUserModel
            {
                Name = apiUser.Name,
                Surname = apiUser.Surname,
                Email = apiUser.Email,
                PasswordHashed = apiUser.PasswordHashed,
                AboutMe = apiUser.AboutMe,
                City = apiUser.City,
                Country = apiUser.Country,
                University = apiUser.University
            };
            
            context.ApiUserModels.Add(newUser);
            context.SaveChanges();
        }
    }

    public void Update(ApiUserModel apiUser)
    {
        using (var context = new DatabaseFrameworkService())
        {
            ApiUserModel? user = context.ApiUserModels.Find(apiUser.Id);
            if (user == null) { return; }

            user.Name = apiUser.Name;
            user.Surname = apiUser.Surname;
            user.AboutMe = apiUser.AboutMe;
            user.City = apiUser.City;
            user.Country = apiUser.Country;
            user.University = apiUser.University;

            context.ApiUserModels.Update(user);
            context.SaveChanges();
        }
    }

    public void ChangePassword(ApiUserModel apiUser, string newPassword) 
    {
        using (var context = new DatabaseFrameworkService())
        {
            ApiUserModel? user = context.ApiUserModels.Find(apiUser.Id);
            if (user == null) { return; }

            user.PasswordHashed = newPassword;

            context.ApiUserModels.Update(user);
            context.SaveChanges();
        }
    }
}