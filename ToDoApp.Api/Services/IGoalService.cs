using ToDoApp.Api.DTOs;

namespace ToDoApp.Api.Services
{
	public interface IGoalService
	{
		Task<IEnumerable<GoalDto>> GetAllGoalsAsync(CancellationToken ct = default);
		Task<GoalDto?> GetGoalByIdAsync(int goalId, CancellationToken ct = default);
		Task<GoalDto> AddGoalAsync(GoalDto goal, CancellationToken ct = default);
		Task<GoalDto?> UpdateGoalAsync(GoalDto goal, CancellationToken ct = default);
		Task<bool> DeleteGoalAsync(int goalId, CancellationToken ct = default);
	}
}
