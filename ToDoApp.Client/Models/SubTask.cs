namespace ToDoApp.Client.Models
{
    public class SubTask : BaseModel
    {
        public int SubTaskId { get; set; }
        public int GoalId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsImportant { get; set; }
        public string? Status { get; set; }
    }
}
