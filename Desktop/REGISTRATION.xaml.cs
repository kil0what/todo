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

            // Простая валидация без FieldValidator
            bool allValid = ValidateName(name) &&
                           ValidateEmail(email) &&
                           ValidatePassword(password) &&
                           password == confirmPassword;

            RegisterButton.IsEnabled = allValid;
            return allValid;
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || email == "example@mail.ru")
                return false;

            int atIndex = email.IndexOf('@');
            if (atIndex <= 0 || atIndex == email.Length - 1)
                return false;

            string afterAt = email.Substring(atIndex + 1);
            return afterAt.Contains(".") && afterAt.IndexOf('.') > 0 && afterAt.IndexOf('.') < afterAt.Length - 1;
        }

        private bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) &&
                   password != "Введите пароль" &&
                   password.Length >= 6;
        }

        private bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   name != "Введите имя пользователя" &&
                   name.Length >= 3;
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
            this.Close();
        }
    }
}