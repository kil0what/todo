using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop
{
    public partial class MainWindow : Window
    {
        // Данные единственного пользователя (студента)
        private const string StudentEmail = "student@university.com";
        private const string StudentPassword = "123456";

        public MainWindow()
        {
            InitializeComponent();
            LoginButton.IsEnabled = false; // Изначально кнопка неактивна
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Name == "EmailTextBox" && textBox.Text == "student@university.com")
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.Black;
                }
                else if (textBox.Name == "PasswordTextBox" && textBox.Text == "Введите пароль")
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.Black;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox.Name == "EmailTextBox")
                    {
                        textBox.Text = "student@university.com";
                        textBox.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCACACA");
                    }
                    else if (textBox.Name == "PasswordTextBox")
                    {
                        textBox.Text = "Введите пароль";
                        textBox.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCACACA");
                    }
                }
            }
            ValidateLoginFields(); // Проверяем поля после потери фокуса
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateLoginFields(); // Проверяем поля при изменении текста
        }

        private bool ValidateLoginFields()
        {
            try
            {
                if (EmailTextBox == null || PasswordTextBox == null || LoginButton == null)
                    return false;

                string email = EmailTextBox.Text;
                string password = PasswordTextBox.Text;

                // Проверяем что поля не пустые и не содержат placeholder'ы
                bool isEmailValid = !string.IsNullOrWhiteSpace(email) &&
                                   email != "student@university.com" &&
                                   email.Contains("@") && email.Contains("."); // Проверка на @ и .

                bool isPasswordValid = !string.IsNullOrWhiteSpace(password) &&
                                      password != "Введите пароль" &&
                                      password.Length >= 6; // Пароль не менее 6 символов

                bool allValid = isEmailValid && isPasswordValid;
                LoginButton.IsEnabled = allValid;

                return allValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;

            // Проверяем логин и пароль
            if (email == StudentEmail && password == StudentPassword)
            {
                // Успешный вход
                MainEmpty mainEmptyWindow = new MainEmpty();
                mainEmptyWindow.Show();
                this.Close();
            }
            else
            {
                // Неверные данные
                MessageBox.Show("Неверный email или пароль!", "Ошибка входа",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            REGISTRATION registrationWindow = new REGISTRATION();
            registrationWindow.Show();
            this.Close();
        }
    }
}