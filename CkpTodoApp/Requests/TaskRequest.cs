namespace CkpTodoApp.Requests;

public class TaskRequest
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}