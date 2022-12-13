using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Services.DatabaseService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true) 
                .AllowCredentials());

var databaseManagerController = new DatabaseService();
databaseManagerController.InitDatabase();
databaseManagerController.SeedDatabase();

app.Run();