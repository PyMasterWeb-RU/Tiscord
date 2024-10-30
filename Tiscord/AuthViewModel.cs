using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Tiscord
{
    public class AuthViewModel
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;

        public AuthViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:4200/api/") };
        }

        public async Task Login(string email, string password)
        {
            var authData = new { email, password };
            var response = await _httpClient.PostAsJsonAsync("auth/login", authData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                _accessToken = result.AccessToken;
                MessageBox.Show("Успешная авторизация!");

                // Создаем и показываем основное окно
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // Вызов метода для инициализации профиля
                mainWindow.InitializeUserProfile(result.User.Id, _accessToken);

                // Закрываем окно авторизации
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is LoginWindow)?.Close();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации. Проверьте данные.");
            }
        }

        public async Task<bool> Register(string email, string password, string username)
        {
            var authData = new { email, password, username };
            var response = await _httpClient.PostAsJsonAsync("auth/register", authData);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Успешная регистрация!");
                return true;
            }
            else
            {
                MessageBox.Show("Ошибка регистрации.");
                return false;
            }
        }
    }

    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
