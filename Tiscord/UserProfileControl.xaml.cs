using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Tiscord
{
    public partial class UserProfileControl : UserControl
    {
        private readonly UserService _userService;
        private int _userId;
        private string _accessToken;

        public UserProfileControl()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        public void InitializeProfile(int userId, string accessToken)
        {
            _userId = userId;
            _accessToken = accessToken;
            LoadUserProfile();
        }

        private async void LoadUserProfile()
        {
            _userService.SetAuthorizationHeader(_accessToken);
            var profile = await _userService.GetProfile();
            if (profile != null)
            {
                NameTextBox.Text = profile.Name;
                SurnameTextBox.Text = profile.Surname;
                EmailTextBox.Text = profile.Email;
                PhoneNumberTextBox.Text = profile.PhoneNumber;

                // Принудительное обновление UI
                this.UpdateLayout();
            }
            else
            {
                MessageBox.Show("Не удалось загрузить данные профиля.");
            }
        }

        private async void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            var updatedProfile = new UserProfile
            {
                Name = NameTextBox.Text,
                Surname = SurnameTextBox.Text,
                Email = EmailTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text
            };

            bool success = await _userService.UpdateProfile(_userId, updatedProfile);
            MessageBox.Show(success ? "Профиль обновлен успешно!" : "Ошибка при обновлении профиля.");
        }
    }
}
