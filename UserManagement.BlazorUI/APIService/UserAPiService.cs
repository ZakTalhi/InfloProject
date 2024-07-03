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
        try
        {
            var response = await _httpClient.GetAsync("users");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<UserListItemViewModel>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result ?? new List<UserListItemViewModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching users: {ex.Message}");
            return new List<UserListItemViewModel>();
        }
    }

    public async Task<UserListItemViewModel?> GetUserAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"users/{id}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UserListItemViewModel>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result == null)
            {
                return new UserListItemViewModel
                {
                    Forename = string.Empty,
                    Surname = string.Empty,
                    Email = string.Empty
                };
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateUserAsync(UserListItemViewModel user)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("users", user);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(UserListItemViewModel user)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"users/{user.Id}", user);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user with ID {user.Id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(long id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"users/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
            return false;
        }
    }
}

