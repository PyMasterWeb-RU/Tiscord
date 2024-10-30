using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tiscord
{
    public partial class MainWindow : Window
    {
        private ModuleBase selectedModule;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConfigureModule_Click(object sender, RoutedEventArgs e)
        {
            // Показываем панель настроек модуля
            ModuleSettingsPanel.Visibility = Visibility.Visible;

            // Устанавливаем настройки по умолчанию
            TitleTextBox.Text = "Новый модуль";
            ColorComboBox.SelectedIndex = 0; // LightGreen по умолчанию
        }

        public void ShowUserProfile(int userId, string accessToken)
        {
            var userProfileControl = new UserProfileControl();
            UserProfileControl.InitializeProfile(userId, accessToken);
            MainTabControl.Items.Add(new TabItem { Header = "Профиль", Content = userProfileControl });
        }

        public void InitializeUserProfile(int userId, string accessToken)
        {
            // Делаем вкладку профиля видимой и активируем её
            ProfileTab.Visibility = Visibility.Visible;
            MainTabControl.SelectedItem = ProfileTab;

            // Инициализация UserProfileControl с токеном
            if (UserProfileControl is UserProfileControl userProfileControl)
            {
                userProfileControl.InitializeProfile(userId, accessToken);
            }
        }




        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            var chatModule = new ChatModule
            {
                Width = 300,
                Height = 200,
                Background = (Brush)new BrushConverter().ConvertFromString(((ComboBoxItem)ColorComboBox.SelectedItem).Content.ToString())
            };

            // Устанавливаем заголовок чата
            chatModule.Title = TitleTextBox.Text;

            // Добавляем модуль чата в контейнер
            ModuleContainer.Items.Add(chatModule);

            // Скрываем панель настройки после добавления
            ModuleSettingsPanel.Visibility = Visibility.Collapsed;
        }



        private void ApplySettings_Click(object sender, RoutedEventArgs e)
        {
            if (selectedModule != null)
            {
                selectedModule.Title = TitleTextBox.Text;

                var selectedColor = ColorComboBox.SelectedItem as ComboBoxItem;
                if (selectedColor != null)
                {
                    var colorName = selectedColor.Content.ToString();
                    selectedModule.ModuleColor = (Brush)new BrushConverter().ConvertFromString(colorName);

                    // Отображение нового цвета для отладки
                    MessageBox.Show($"New color: {selectedModule.ModuleColor}");
                }
            }
        }

    }
}
