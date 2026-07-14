using LoadTestingSystem.Desktop.DTOs;
using LoadTestingSystem.Desktop.Services;
using System.Windows;
using System.Windows.Controls;

namespace LoadTestingSystem.Desktop.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            var request = new LoginRequest
            {
                Login = login,
                Password = password
            };

            bool success = await _authService.LoginAsync(request);

            if (success)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Помилка");
            }
        }
    }
}
