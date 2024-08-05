namespace ToDoApp.Api.Models
{
	public class SubTask
	{
		public int SubTaskId { get; set; }
		public int GoalId { get; set; }
		public required string Title { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Status { get; set; }
		public bool IsImportant { get; set; }

		public required Goal Goal { get; set; }
	}
}
