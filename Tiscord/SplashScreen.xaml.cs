using System.Threading.Tasks;
using System.Windows;

namespace Tiscord
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            ShowSplashScreen();
        }

        private async void ShowSplashScreen()
        {
            await Task.Delay(5000); // Задержка в 3 секунды
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
