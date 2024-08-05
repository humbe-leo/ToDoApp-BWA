namespace ToDoApp.Api.DTOs
{
	public class GoalDto
	{
		public int GoalId { get; set; }
		public required string Title { get; set; }
		public DateTime DateCreated { get; set; }
		public double Progress { get; set; }
        public int CompletedTasks { get; set; }
        public IEnumerable<SubTaskDto>? SubTasks { get; set; }
	}
}
