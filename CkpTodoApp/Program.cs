using CkpTodoApp.Services.DatabaseService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseFrameworkService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true) 
                .AllowCredentials());

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<DatabaseFrameworkService>();
  db.Database.EnsureCreatedAsync();
}

var databaseManagerController = new DatabaseService();
databaseManagerController.InitDatabase();
databaseManagerController.SeedDatabase();

app.Run();