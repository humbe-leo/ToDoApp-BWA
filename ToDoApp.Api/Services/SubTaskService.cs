using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoApp.Api.DTOs;
using ToDoApp.Api.Helpers;
using ToDoApp.Api.Models;
using ToDoApp.Api.Repositories;

namespace ToDoApp.Api.Services
{
	public class SubTaskService : ISubTaskService
	{
		private readonly ISubTaskRepository _subTaskRepository;
		private readonly GoalProgressHelper _goalProgressHelper;
		private readonly IMapper _mapper;
		public SubTaskService(ISubTaskRepository subTaskRepository, GoalProgressHelper goalProgressHelper, IMapper mapper)
		{
			_subTaskRepository = subTaskRepository;
			_goalProgressHelper = goalProgressHelper;
			_mapper = mapper;
		}

		public async Task<SubTaskDto> AddSubTaskAsync(SubTaskDto subtask, CancellationToken ct = default)
		{
			var newSubTask = _mapper.Map<SubTask>(subtask);
			newSubTask.DateCreated = DateTime.Now;
			newSubTask.Status = false;
			newSubTask.IsImportant = false;
			var createdSubTask = await _subTaskRepository.AddSubTaskAsync(newSubTask, ct);
			await _goalProgressHelper.UpdateGoalProgressAsync(createdSubTask.GoalId, ct);
			return _mapper.Map<SubTaskDto>(createdSubTask);
		}

		public async Task<bool> DeleteSubTaskAsync(int subTaskId, CancellationToken ct = default)
		{
			var subtask = await _subTaskRepository.GetSubTaskByIdAsync(subTaskId, ct);
			if (subtask == null)
			{
				return false;
			}

			var result = await _subTaskRepository.DeleteSubTaskAsync(subTaskId, ct);
			if (result)
			{
				await _goalProgressHelper.UpdateGoalProgressAsync(subtask.GoalId, ct);
			}

			return result;
		}

		public async Task<bool> DeleteSubTaskRangeAsync(List<int> subTaskIds, CancellationToken ct = default)
		{
			if (subTaskIds == null || !subTaskIds.Any())
			{
				return false;
			}
			
			var subtask = await _subTaskRepository.GetSubTaskByIdAsync(subTaskIds[0], ct);
			if (subtask == null)
			{
				return false;
			}

			var result = await _subTaskRepository.DeleteSubTaskRangeAsync(subTaskIds, ct);
			if (result)
			{
				await _goalProgressHelper.UpdateGoalProgressAsync(subtask.GoalId, ct);
			}

			return result;
		}

		public async Task<SubTaskDto?> GetSubTaskByIdAsync(int subTaskId, CancellationToken ct = default)
		{
			var subtask = await _subTaskRepository.GetSubTaskByIdAsync(subTaskId, ct);
			return _mapper.Map<SubTaskDto>(subtask);
		}

		public async Task<IEnumerable<SubTaskDto>> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default)
		{
			var subtasks = await _subTaskRepository.GetSubTasksByGoalIdAsync(goalId, ct);
			return _mapper.Map<IEnumerable<SubTaskDto>>(subtasks);
		}

		public async Task<PagedResult<SubTaskDto>> GetSubTasksByGoalIdPagedAsync(int goalId, int pageSize, int pageNumber, CancellationToken ct = default)
		{
			var subtasks = await _subTaskRepository.GetSubTasksByGoalIdAsync(goalId, ct);

			int totalCount = subtasks.Count();
			int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

			subtasks = subtasks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

			var items = _mapper.Map<List<SubTaskDto>>(subtasks);

			return new PagedResult<SubTaskDto>
			{
				Items = items,
				TotalCount = totalCount,
				Page = pageNumber,
				PageSize = pageSize,
				TotalPages = totalPages
			};
		}

		public async Task<SubTaskDto?> UpdateSubTaskAsync(SubTaskDto subtask, CancellationToken ct = default)
		{
			var updateSubTask = _mapper.Map<SubTask>(subtask);
			var updatedSubTask = await _subTaskRepository.UpdateSubTaskAsync(updateSubTask, ct);
			if (updatedSubTask != null)
			{
				await _goalProgressHelper.UpdateGoalProgressAsync(updatedSubTask.GoalId, ct);
			}
			return _mapper.Map<SubTaskDto>(updatedSubTask);
		}

		public async Task<List<SubTaskDto>?> UpdateSubTaskRangeAsync(List<SubTaskDto> subtasks, CancellationToken ct = default)
		{
			var updateSubTasks = _mapper.Map<List<SubTask>>(subtasks);
			var updatedSubTasks = await _subTaskRepository.UpdateSubTaskRangeAsync(updateSubTasks, ct);
			if (updatedSubTasks.Any())
			{
				await _goalProgressHelper.UpdateGoalProgressAsync(updatedSubTasks[0].GoalId, ct);
			}
			return _mapper.Map<List<SubTaskDto>>(updatedSubTasks);
		}
	}
}
