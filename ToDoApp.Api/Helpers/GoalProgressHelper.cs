using ToDoApp.Api.Repositories;

namespace ToDoApp.Api.Helpers
{
	public class GoalProgressHelper
	{
		private readonly IGoalRepository _goalRepository;
		private readonly ISubTaskRepository _subTaskRepository;

		public GoalProgressHelper(IGoalRepository goalRepository, ISubTaskRepository subTaskRepository)
		{
			_goalRepository = goalRepository;
			_subTaskRepository = subTaskRepository;
		}

		public async Task UpdateGoalProgressAsync(int goalId, CancellationToken ct = default)
		{
			var goal = await _goalRepository.GetGoalByIdAsync(goalId, ct);
			if (goal != null)
			{
				var subtasks = await _subTaskRepository.GetSubTasksByGoalIdAsync(goalId, ct);
				if (subtasks.Any())
				{
					var completedTasks = subtasks.Count(t => t.Status == true);
					goal.Progress = completedTasks / (decimal)subtasks.Count() * 100;
					await _goalRepository.UpdateGoalAsync(goal, ct);
				}
			}
		}
	}
}
