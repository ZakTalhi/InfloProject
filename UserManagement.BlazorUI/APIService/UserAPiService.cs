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
}
