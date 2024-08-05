using ToDoApp.Client.Models;

namespace ToDoApp.Client.Services
{
    public interface IGoalService
    {
        Task<List<Goal>> GetAllGoalsAsync(CancellationToken ct = default);
        Task<Goal?> GetGoalByIdAsync(int goalId, CancellationToken ct = default);
        Task<Goal> AddGoalAsync(Goal goal, CancellationToken ct = default);
        bool IsTitleUnique(string title);
		Task<Goal?> UpdateGoalAsync(Goal goal, CancellationToken ct = default);
        Task<bool> DeleteGoalAsync(int goalId, CancellationToken ct = default);
    }
}
