namespace ToDoApp.Client.Models
{
	public class PagedResult<T>
	{
		public required List<T> Items { get; set; }
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public bool PreviousPage { get; set; }
		public bool NextPage { get; set; }
	}
}
