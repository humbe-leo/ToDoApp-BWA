using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ToDoApp.Client.Models;

namespace ToDoApp.Client.Services
{
	public class SubTaskService : ISubTaskService
	{
		private readonly HttpClient _httpClient;
		private List<SubTask> _subtaskItems = new();
		public SubTaskService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<SubTask> AddSubTaskAsync(SubTask subtask, CancellationToken ct = default)
		{
			var response = await _httpClient.PostAsJsonAsync("api/subtask", subtask, cancellationToken: ct);

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Subtask was not created");

			subtask = await response.Content.ReadFromJsonAsync<SubTask>();

			return subtask;
		}

		public Task<bool> DeleteSubTaskAsync(int subTaskId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteSubTaskRangeAsync(List<int> subTaskIds, CancellationToken ct = default)
		{
			var request = new HttpRequestMessage(HttpMethod.Delete, "api/subtask")
			{
				Content = new StringContent(
				   JsonSerializer.Serialize(subTaskIds),
				   Encoding.UTF8,
				   "application/json"
				)
			};

			var response = await _httpClient.SendAsync(request, ct);

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Goal was not deleted");

			return true;
		}

		public Task<SubTask?> GetSubTaskByIdAsync(int subTaskId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public async Task<List<SubTask>> GetSubTasksByGoalIdAsync(int goalId, CancellationToken ct = default)
		{
			return _subtaskItems = await _httpClient.GetFromJsonAsync<List<SubTask>>($"api/subtask/by-goal/{goalId}", ct);
		}

		public async Task<PagedResult<SubTask>> GetSubTasksByGoalIdPagedAsync(int goalId, int pageSize, int pageNumber, CancellationToken ct = default)
		{
			return await _httpClient.GetFromJsonAsync<PagedResult<SubTask>>($"api/subtask/by-goal/{goalId}/paged?pageSize={pageSize}&pageNumber={pageNumber}", ct);
		}

		public bool IsTitleUnique(string title)
		{
			return !_subtaskItems.Any(p => p.Title == title);
		}

		public async Task<SubTask?> UpdateSubTaskAsync(SubTask subtask, CancellationToken ct = default)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/subtask/{subtask.SubTaskId}", subtask, cancellationToken: ct);

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Subtask was not updated");

			subtask = await response.Content.ReadFromJsonAsync<SubTask>();

			return subtask;
		}

		public async Task<List<SubTask>?> UpdateSubTaskRangeAsync(List<SubTask> subtasks, CancellationToken ct = default)
		{
			var response = await _httpClient.PutAsJsonAsync("api/subtask", subtasks, cancellationToken: ct);

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Subtasks were not updated");

			subtasks = await response.Content.ReadFromJsonAsync<List<SubTask>>();

			return subtasks;
		}
	}
}
