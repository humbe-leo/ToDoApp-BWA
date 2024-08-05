using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Api.Data;
using ToDoApp.Api.Models;

namespace ToDoApp.Api.Repositories
{
	public class SubTaskRepository : ISubTaskRepository
	{
		private readonly ToDoAppDbContext _context;
        public SubTaskRepository(ToDoAppDbContext context)
        {
            _context = context;
        }
        public async Task<SubTask> AddSubTaskAsync(SubTask subtask, CancellationToken ct = default)
		{
			_context.SubTasks.Add(subtask);
			await _context.SaveChangesAsync(ct);
			return subtask;
		}

		public async Task<bool> DeleteSubTaskAsync(int id, CancellationToken ct = default)
		{
			var task = await _context.SubTasks.FindAsync(new object?[] { id }, cancellationToken: ct);
			if (task == null)
			{
				return false;
			}

			_context.SubTasks.Remove(task);
			await _context.SaveChangesAsync(ct);
			return true;
		}

		public async Task<bool> DeleteSubTaskRangeAsync(List<int> ids, CancellationToken ct = default)
		{
			var tasks = await _context.SubTasks.Where(p => ids.Contains(p.SubTaskId)).ToListAsync(ct);
			if (tasks == null || !tasks.Any())
			{
				return false;
			}

			_context.SubTasks.RemoveRange(tasks);
			await _context.SaveChangesAsync(ct);
			return true;
		}

		public async Task<SubTask?> GetSubTaskByIdAsync(int id, CancellationToken ct = default)
		{
			return await _context.SubTasks.FindAsync(new object?[] { id }, cancellationToken: ct);
		}

		public async Task<IEnumerable<SubTask>> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default)
		{
			return await _context.SubTasks.Where(t => t.GoalId == goalId).ToListAsync(ct);
		}

		public async Task<SubTask?> UpdateSubTaskAsync(SubTask subtask, CancellationToken ct = default)
		{
			var existingSubTask = await _context.SubTasks.FindAsync(new object?[] { subtask.SubTaskId }, cancellationToken: ct);

			if (existingSubTask == null)
			{
				return null;
			}

			existingSubTask.Title = subtask.Title;
			existingSubTask.Status = subtask.Status;
			existingSubTask.IsImportant = subtask.IsImportant;

			_context.SubTasks.Update(existingSubTask);
			await _context.SaveChangesAsync(ct);

			return existingSubTask;
		}

		public async Task<List<SubTask>> UpdateSubTaskRangeAsync(IEnumerable<SubTask> subtasks, CancellationToken ct = default)
		{
			var updatedSubTasks = new List<SubTask>();

			foreach (var subtask in subtasks)
			{
				var existingSubTask = await _context.SubTasks.FindAsync(new object?[] { subtask.SubTaskId }, cancellationToken: ct);
				if (existingSubTask != null)
				{
					existingSubTask.Title = subtask.Title;
					existingSubTask.Status = subtask.Status;
					existingSubTask.IsImportant = subtask.IsImportant;
					_context.SubTasks.Update(existingSubTask);
					updatedSubTasks.Add(existingSubTask);
				}
			}

			if (updatedSubTasks.Any())
			{
				await _context.SaveChangesAsync(ct);
			}

			return updatedSubTasks;
		}
	}
}
