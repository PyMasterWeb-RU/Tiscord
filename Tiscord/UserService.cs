using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using Tiscord;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:4200/api/") };
    }

    // Метод для установки заголовка авторизации с токеном
    public void SetAuthorizationHeader(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
    }

    public async Task<UserProfile> GetProfile()
    {
        // Запрос профиля с авторизацией
        return await _httpClient.GetFromJsonAsync<UserProfile>("user");
    }

    public async Task<bool> UpdateProfile(int userId, UserProfile profile)
    {
        var response = await _httpClient.PutAsJsonAsync($"user", profile);
        return response.IsSuccessStatusCode; // Возвращаем true, если обновление прошло успешно
    }
}
