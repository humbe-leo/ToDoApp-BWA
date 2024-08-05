namespace ToDoApp.Client.Models
{
	public class Goal : BaseModel
    {
        public int GoalId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Progress { get; set; }
        public int CompletedTasks { get; set; }
        public IEnumerable<SubTask>? SubTasks { get; set; }
    }
}
