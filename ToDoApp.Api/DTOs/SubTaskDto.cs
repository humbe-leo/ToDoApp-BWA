namespace ToDoApp.Api.DTOs
{
	public class SubTaskDto
	{
		public int SubTaskId { get; set; }
		public int GoalId { get; set; }
		public required string Title { get; set; }
		public DateTime DateCreated { get; set; }
		public bool IsImportant { get; set; }
		public string? Status { get; set; }
	}
}
