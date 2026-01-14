using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Desktop
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу регистрации (создай её аналогично)
            // NavigationService.Navigate(new RegistrationPage());
            MessageBox.Show("Переход на регистрацию");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь будет логика входа
            MessageBox.Show("Логика входа");
        }
    }
}