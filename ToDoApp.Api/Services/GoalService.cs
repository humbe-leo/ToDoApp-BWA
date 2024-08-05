using AutoMapper;
using ToDoApp.Api.DTOs;
using ToDoApp.Api.Models;
using ToDoApp.Api.Repositories;

namespace ToDoApp.Api.Services
{
	public class GoalService : IGoalService
	{
		private readonly IGoalRepository _goalRepository;
		private readonly IMapper _mapper;

		public GoalService(IGoalRepository goalRepository, IMapper mapper)
		{
			_goalRepository = goalRepository;
			_mapper = mapper;
		}
		public async Task<GoalDto> AddGoalAsync(GoalDto goal, CancellationToken ct = default)
		{
			var newGoal = _mapper.Map<Goal>(goal);
			newGoal.DateCreated = DateTime.Now;
			var createdGoal = await _goalRepository.AddGoalAsync(newGoal, ct);
			return _mapper.Map<GoalDto>(createdGoal);
		}

		public async Task<bool> DeleteGoalAsync(int id, CancellationToken ct = default)
		{
			return await _goalRepository.DeleteGoalAsync(id, ct);
		}

		public async Task<IEnumerable<GoalDto>> GetAllGoalsAsync(CancellationToken ct = default)
		{
			var goals = await _goalRepository.GetAllGoalsAsync(ct);
			return _mapper.Map<IEnumerable<GoalDto>>(goals);
		}

		public async Task<GoalDto?> GetGoalByIdAsync(int id, CancellationToken ct = default)
		{
			var goal = await _goalRepository.GetGoalByIdAsync(id, ct);
			return _mapper.Map<GoalDto>(goal);
		}

		public async Task<GoalDto?> UpdateGoalAsync(GoalDto goal, CancellationToken ct = default)
		{
			var updateGoal = _mapper.Map<Goal>(goal);
			var updatedGoal = await _goalRepository.UpdateGoalAsync(updateGoal, ct);
			return _mapper.Map<GoalDto>(updatedGoal);
		}
	}
}
