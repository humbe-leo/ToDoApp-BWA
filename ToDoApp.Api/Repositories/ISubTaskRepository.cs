using ToDoApp.Api.Models;

namespace ToDoApp.Api.Repositories
{
	public interface ISubTaskRepository
	{
		Task<IEnumerable<SubTask>> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default);
		Task<SubTask?> GetSubTaskByIdAsync(int id, CancellationToken ct = default);
		Task<SubTask> AddSubTaskAsync(SubTask subtask, CancellationToken ct = default);
		Task<SubTask?> UpdateSubTaskAsync(SubTask subtask, CancellationToken ct = default);
		Task<List<SubTask>> UpdateSubTaskRangeAsync(IEnumerable<SubTask> subtasks, CancellationToken ct = default);
		Task<bool> DeleteSubTaskAsync(int id, CancellationToken ct = default);
		Task<bool> DeleteSubTaskRangeAsync(List<int> ids, CancellationToken ct = default);
	}
}
