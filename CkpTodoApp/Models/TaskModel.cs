namespace CkpTodoApp.Models
{
    public class TaskModel : ITaskInterface
    {
        public TaskModel(string title, string description, bool isCheck) 
        {
            Title = title;
            Description = description;
            IsCheck = isCheck;
        }
        
        public string Title { get; set; }

        public string Description { get; set; }
        
        public bool IsCheck { get; set; }
    }
}

