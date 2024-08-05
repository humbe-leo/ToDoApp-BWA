using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.DTOs;
using ToDoApp.Api.Services;

namespace ToDoApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubTaskController : ControllerBase
	{
		private readonly ISubTaskService _subTaskService;

		public SubTaskController(ISubTaskService subTaskService)
		{
			_subTaskService = subTaskService;
		}

		[HttpGet("by-goal/{goalId}")]
		public async Task<IActionResult> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default)
		{
			var subtasks = await _subTaskService.GetSubTasksByGoalIdAsync(goalId, ct);
			return Ok(subtasks);
		}

		[HttpGet("by-goal/{goalId}/paged")]
		public async Task<IActionResult> GetSubTasksByGoalIdPagedAsync(int goalId, [FromQuery] int pageSize, [FromQuery] int pageNumber, CancellationToken ct = default)
		{
			var subtasks = await _subTaskService.GetSubTasksByGoalIdPagedAsync(goalId, pageSize, pageNumber, ct);
			return Ok(subtasks);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSubTaskAsync(int id, CancellationToken ct = default)
		{
			var subTask = await _subTaskService.GetSubTaskByIdAsync(id, ct);
			if (subTask == null)
			{
				return NotFound();
			}
			return Ok(subTask);
		}

		[HttpPost]
		public async Task<IActionResult> CreateSubTaskAsync([FromBody] SubTaskDto subTaskDto, CancellationToken ct = default)
		{
			var createdSubTask = await _subTaskService.AddSubTaskAsync(subTaskDto, ct);
			return StatusCode((int)HttpStatusCode.Created, createdSubTask);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSubTaskAsync(int id, [FromBody] SubTaskDto subTaskDto, CancellationToken ct = default)
		{
			if (id != subTaskDto.SubTaskId)
			{
				return BadRequest();
			}

			var updatedSubTask = await _subTaskService.UpdateSubTaskAsync(subTaskDto, ct);
			if (updatedSubTask == null)
			{
				return NotFound();
			}
			return Ok(updatedSubTask);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateSubTaskAsync([FromBody] List<SubTaskDto> subTaskDtos, CancellationToken ct = default)
		{
			if (!subTaskDtos.Any())
			{
				return BadRequest();
			}

			var updatedSubTask = await _subTaskService.UpdateSubTaskRangeAsync(subTaskDtos, ct);
			if (updatedSubTask == null || !updatedSubTask.Any())
			{
				return NotFound();
			}
			return Ok(updatedSubTask);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubTaskAsync(int id, CancellationToken ct = default)
		{
			var result = await _subTaskService.DeleteSubTaskAsync(id, ct);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteSubTaskRangeAsync([FromBody] List<int> ids, CancellationToken ct = default)
		{
			var result = await _subTaskService.DeleteSubTaskRangeAsync(ids, ct);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
