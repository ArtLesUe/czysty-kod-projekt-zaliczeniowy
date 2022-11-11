using CkpTodoApp.DatabaseControllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

DatabaseManagerController databaseManagerController = new DatabaseManagerController();
databaseManagerController.InitDatabase();

app.Run();