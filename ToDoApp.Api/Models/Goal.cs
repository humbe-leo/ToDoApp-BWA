namespace ToDoApp.Api.Models
{
	public class Goal
	{
		public int GoalId { get; set; }
		public required string Title { get; set; }
		public DateTime DateCreated { get; set; }
		public decimal Progress { get; set; }

		public ICollection<SubTask>? SubTasks { get; set; }
	}
}
