using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.DTOs;
using ToDoApp.Api.Services;

namespace ToDoApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GoalController : ControllerBase
	{
		private readonly IGoalService _goalService;

		public GoalController(IGoalService goalService)
		{
			_goalService = goalService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllGoalsAsync(CancellationToken ct = default)
		{
			var goals = await _goalService.GetAllGoalsAsync(ct);
			return Ok(goals);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGoalAsync(int id, CancellationToken ct = default)
		{
			var goal = await _goalService.GetGoalByIdAsync(id, ct);
			if (goal == null)
			{
				return NotFound();
			}
			return Ok(goal);
		}

		[HttpPost]
		public async Task<IActionResult> CreateGoalAsync([FromBody] GoalDto goalDto, CancellationToken ct = default)
		{
			var createdGoal = await _goalService.AddGoalAsync(goalDto, ct);
			return StatusCode((int)HttpStatusCode.Created, createdGoal);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateGoalAsync(int id, [FromBody] GoalDto goalDto, CancellationToken ct = default)
		{
			if (id != goalDto.GoalId)
			{
				return BadRequest();
			}

			var updatedGoal = await _goalService.UpdateGoalAsync(goalDto, ct);
			if (updatedGoal == null)
			{
				return NotFound();
			}
			return Ok(updatedGoal);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGoalAsync(int id, CancellationToken ct = default)
		{
			var result = await _goalService.DeleteGoalAsync(id, ct);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
