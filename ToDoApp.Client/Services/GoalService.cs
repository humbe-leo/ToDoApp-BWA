using System.Net.Http.Json;
using ToDoApp.Client.Models;

namespace ToDoApp.Client.Services
{
    public class GoalService : IGoalService
    {
        private readonly HttpClient _httpClient;
		private List<Goal> _goalItems = new();
		public GoalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Goal> AddGoalAsync(Goal goal, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/goal", goal, cancellationToken: ct);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Goal was not created");
            
            goal = await response.Content.ReadFromJsonAsync<Goal>();
            
            return goal;
        }

        public async Task<bool> DeleteGoalAsync(int goalId, CancellationToken ct = default)
        {
            var response = await _httpClient.DeleteAsync($"api/goal/{goalId}", ct);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Goal was not deleted");

            return true;
        }

        public async Task<List<Goal>> GetAllGoalsAsync(CancellationToken ct = default)
        {
            return _goalItems = await _httpClient.GetFromJsonAsync<List<Goal>>("api/goal", ct);
        }

        public async Task<Goal?> GetGoalByIdAsync(int goalId, CancellationToken ct = default)
        {
            return await _httpClient.GetFromJsonAsync<Goal>($"api/goal/{goalId}", ct);
        }

		public bool IsTitleUnique(string title)
		{
			return !_goalItems.Any(p => p.Title == title);
		}

		public async Task<Goal?> UpdateGoalAsync(Goal goal, CancellationToken ct = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/goal/{goal.GoalId}", goal, cancellationToken: ct);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Goal was not updated");

            goal = await response.Content.ReadFromJsonAsync<Goal>();

            return goal;
        }
    }
}
