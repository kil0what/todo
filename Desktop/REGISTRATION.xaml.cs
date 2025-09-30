using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для REGISTRATION.xaml
    /// </summary>
    public partial class REGISTRATION : Window
    {
        public REGISTRATION()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateFields();
        }

        private bool ValidateFields()
        {
            string name = UserNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;
            string confirmPassword = ConfirmPasswordTextBox.Text;
            bool allValid = FieldValidator.ValidateAllFields(name, email, password, confirmPassword);
            RegisterButton.IsEnabled = allValid;

            return allValid;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                MainEmpty mainEmptyWindow = new MainEmpty();
                mainEmptyWindow.Show();
                this.Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Возврат на главное окно
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
    public static class FieldValidator
    {
        public static bool ValidateEmail(string email)
        {
            // Паттерн "*@*.*" где * - любое количество символов
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Проверяем наличие @ и точки после @
            int atIndex = email.IndexOf('@');
            if (atIndex <= 0 || atIndex == email.Length - 1)
                return false;

            // Проверяем наличие точки после @
            string afterAt = email.Substring(atIndex + 1);
            return afterAt.Contains(".") && afterAt.IndexOf('.') > 0 && afterAt.IndexOf('.') < afterAt.Length - 1;
        }

        public static bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 3;
        }

        public static bool ValidateAllFields(string name, string email, string password, string confirmPassword)
        {
            return ValidateName(name) &&
                   ValidateEmail(email) &&
                   ValidatePassword(password) &&
                   password == confirmPassword;
        }
    }
}