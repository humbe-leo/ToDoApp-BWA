using ToDoApp.Api.DTOs;

namespace ToDoApp.Api.Services
{
	public interface ISubTaskService
	{
		Task<IEnumerable<SubTaskDto>> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default);
		Task<PagedResult<SubTaskDto>> GetSubTasksByGoalIdPagedAsync(int goalId, int pageSize, int pageNumber, CancellationToken ct = default);
		Task<SubTaskDto?> GetSubTaskByIdAsync(int subTaskId, CancellationToken ct = default);
		Task<SubTaskDto> AddSubTaskAsync(SubTaskDto subtask, CancellationToken ct = default);
		Task<SubTaskDto?> UpdateSubTaskAsync(SubTaskDto subtask, CancellationToken ct = default);
		Task<List<SubTaskDto>?> UpdateSubTaskRangeAsync(List<SubTaskDto> subtasks, CancellationToken ct = default);
		Task<bool> DeleteSubTaskAsync(int subTaskId, CancellationToken ct = default);
		Task<bool> DeleteSubTaskRangeAsync(List<int> subTaskIds, CancellationToken ct = default);
	}
}
