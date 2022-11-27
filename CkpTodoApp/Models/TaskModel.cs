using CkpTodoApp.Commons;

namespace CkpTodoApp.Models
{
    public class TaskModel : ITaskInterface
    {
        public TaskModel(string title, string description, bool isCheck = false)
        {
            Id = UniqueNumber.GetUniqueNumber();
            Title = title;
            Description = description;
            IsCheck = isCheck;
        }

        public int Id { get; }
        
        public string Title { get; set; }

        public string Description { get; set; }
        
        public bool IsCheck { get; set; }
    }
}

