using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Models.Task;
using Microsoft.EntityFrameworkCore;

namespace CkpTodoApp.Services.DatabaseService
{
  public class DatabaseFrameworkService : DbContext
  {
    public DbSet<ApiUserModel> ApiUserModels { get; set; }
    public DbSet<ApiTokenModel> ApiTokenModels { get; set; }
    public DbSet<EventModel> EventModels { get; set; }
    public DbSet<TaskModel> TaskModels { get; set; }

    private DatabaseService databaseService = new DatabaseService();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=" + databaseService.DatabasePathFramework() + ";");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ApiUserModel>().ToTable("users");
      modelBuilder.Entity<ApiTokenModel>().ToTable("tokens");
      modelBuilder.Entity<EventModel>().ToTable("events");
      modelBuilder.Entity<TaskModel>().ToTable("tasks");
    }
  }
}
