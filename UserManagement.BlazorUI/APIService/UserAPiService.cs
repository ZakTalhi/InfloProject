using System.Net.Http.Json;
using System.Text.Json; 
using UserManagement.Web.Models.Users;

namespace UserManagement.BlazorUI.APIService;

public class UserAPiService
{
    private readonly HttpClient _httpClient;

    public UserAPiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserListItemViewModel>> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("users");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<UserListItemViewModel>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if(result == null)
        {
            return new List<UserListItemViewModel>();
        }

        return result;
    }

    public async Task<UserListItemViewModel> GetUserAsync(int id)
    {
        var response = await _httpClient.GetAsync($"users/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<UserListItemViewModel>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
       if(result == null)
        {
            return new UserListItemViewModel()
            {
                Forename = "",
                Surname = "",
                Email = "",
            };
        }

        return result;
    }

    public async Task<bool> CreateUserAsync(UserListItemViewModel user)
    {
        var response = await _httpClient.PostAsJsonAsync("users", user);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> UpdateUserAsync(UserListItemViewModel user)
    {
        var response = await _httpClient.PutAsJsonAsync($"users/{user.Id}", user);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> DeleteUserAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"users/{id}");
        response.EnsureSuccessStatusCode();

        return true;
    }
}
