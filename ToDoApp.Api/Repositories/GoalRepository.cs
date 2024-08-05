using Microsoft.EntityFrameworkCore;
using ToDoApp.Api.Data;
using ToDoApp.Api.Models;

namespace ToDoApp.Api.Repositories
{
	public class GoalRepository : IGoalRepository
	{
		private readonly ToDoAppDbContext _context;
		public GoalRepository(ToDoAppDbContext context)
		{
			_context = context;
		}
		public async Task<Goal> AddGoalAsync(Goal goal, CancellationToken ct = default)
		{
			_context.Goals.Add(goal);
			await _context.SaveChangesAsync(ct);
			return goal;
		}

		public async Task<bool> DeleteGoalAsync(int id, CancellationToken ct = default)
		{
			var goal = await _context.Goals.FindAsync(new object?[] { id }, cancellationToken: ct);
			if (goal == null)
			{
				return false;
			}

			_context.Goals.Remove(goal);
			await _context.SaveChangesAsync(ct);
			return true;
		}

		public async Task<IEnumerable<Goal>> GetAllGoalsAsync(CancellationToken ct = default)
		{
			return await _context.Goals.Include(g => g.SubTasks).ToListAsync(ct);
		}

		public async Task<Goal?> GetGoalByIdAsync(int id, CancellationToken ct = default)
		{
			return await _context.Goals.Include(g => g.SubTasks)
								   .FirstOrDefaultAsync(g => g.GoalId == id, ct);
		}

		public async Task<Goal?> UpdateGoalAsync(Goal goal, CancellationToken ct = default)
		{
			var existingGoal = await _context.Goals.FindAsync(new object?[] { goal.GoalId }, cancellationToken: ct);

			if (existingGoal == null)
			{
				return null;
			}

			existingGoal.Title = goal.Title;
			existingGoal.Progress = goal.Progress;

			_context.Goals.Update(existingGoal);
			await _context.SaveChangesAsync(ct);

			return existingGoal;
		}
	}
}
