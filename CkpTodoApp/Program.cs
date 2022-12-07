using CkpTodoApp.DatabaseControllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) 
                .AllowCredentials());

DatabaseManagerController databaseManagerController = new DatabaseManagerController();
databaseManagerController.InitDatabase();
databaseManagerController.SeedDatabase();

app.Run();