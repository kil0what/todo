using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Desktop
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        // Кнопка "Зарегистрироваться"
        private void DoRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Сюда потом добавишь сохранение в репозиторий
            MessageBox.Show("Регистрация успешна!");
            NavigationService.Navigate(new LoginPage());
        }

        // Кнопка "Назад" к логину
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}