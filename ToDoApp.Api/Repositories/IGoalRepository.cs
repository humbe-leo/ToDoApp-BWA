using ToDoApp.Api.Models;

namespace ToDoApp.Api.Repositories
{
	public interface IGoalRepository
	{
		Task<IEnumerable<Goal>> GetAllGoalsAsync(CancellationToken ct = default);
		Task<Goal?> GetGoalByIdAsync(int id, CancellationToken ct = default);
		Task<Goal> AddGoalAsync(Goal goal, CancellationToken ct = default);
		Task<Goal?> UpdateGoalAsync(Goal goal, CancellationToken ct = default);
		Task<bool> DeleteGoalAsync(int id, CancellationToken ct = default);
	}
}
