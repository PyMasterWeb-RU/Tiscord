using System.Windows;

namespace Tiscord
{
    public partial class RegisterWindow : Window
    {
        private readonly AuthViewModel _authViewModel;

        public RegisterWindow()
        {
            InitializeComponent();
            _authViewModel = new AuthViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Это событие позволяет взаимодействовать с содержимым PasswordBox
            // например, изменять видимость placeholder-а.
            PasswordPlaceholder.Visibility = PasswordBox.Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string username = UsernameTextBox.Text;

            bool isRegistered = await _authViewModel.Register(email, password, username);
            if (isRegistered)
            {
                // Закрываем текущее окно и открываем окно авторизации
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
        }

    }
}
