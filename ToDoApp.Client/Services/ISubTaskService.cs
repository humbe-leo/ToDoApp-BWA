using ToDoApp.Client.Models;

namespace ToDoApp.Client.Services
{
	public interface ISubTaskService
	{
		Task<List<SubTask>> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default);
		Task<PagedResult<SubTask>> GetSubTasksByGoalIdPagedAsync(int goalId, int pageSize, int pageNumber, CancellationToken ct = default);
		Task<SubTask?> GetSubTaskByIdAsync(int subTaskId, CancellationToken ct = default);
		Task<SubTask> AddSubTaskAsync(SubTask subtask, CancellationToken ct = default);
		bool IsTitleUnique(string title);
		Task<SubTask?> UpdateSubTaskAsync(SubTask subtask, CancellationToken ct = default);
		Task<List<SubTask>?> UpdateSubTaskRangeAsync(List<SubTask> subtasks, CancellationToken ct = default);
		Task<bool> DeleteSubTaskAsync(int subTaskId, CancellationToken ct = default);
		Task<bool> DeleteSubTaskRangeAsync(List<int> subTaskIds, CancellationToken ct = default);
	}
}
