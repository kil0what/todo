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
using System.Text.RegularExpressions;

namespace Desktop
{
    public partial class REGISTRATION : Window
    {
        public REGISTRATION()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Foreground == Brushes.Gray || textBox.Foreground.ToString() == "#FFCACACA")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCACACA");

                if (textBox == UserNameTextBox)
                {
                    textBox.Text = "Введите имя пользователя";
                }
                else if (textBox == EmailTextBox)
                {
                    textBox.Text = "exam@yandex.ru";
                }
                else if (textBox == PasswordTextBox)
                {
                    textBox.Text = "Введите пароль";
                }
                else if (textBox == ConfirmPasswordTextBox)
                {
                    textBox.Text = "Повторите пароль";
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateFields();
        }

        private bool ValidateFields()
        {
            // Проверка на null всех элементов
            if (UserNameTextBox == null || EmailTextBox == null ||
                PasswordTextBox == null || ConfirmPasswordTextBox == null ||
                RegisterButton == null)
            {
                return false;
            }

            try
            {
                string name = (UserNameTextBox.Foreground.ToString() == "#FFCACACA" || UserNameTextBox.Text == "Введите имя пользователя") ? "" : UserNameTextBox.Text;
                string email = (EmailTextBox.Foreground.ToString() == "#FFCACACA" || EmailTextBox.Text == "exam@yandex.ru") ? "" : EmailTextBox.Text;
                string password = (PasswordTextBox.Foreground.ToString() == "#FFCACACA" || PasswordTextBox.Text == "Введите пароль") ? "" : PasswordTextBox.Text;
                string confirmPassword = (ConfirmPasswordTextBox.Foreground.ToString() == "#FFCACACA" || ConfirmPasswordTextBox.Text == "Повторите пароль") ? "" : ConfirmPasswordTextBox.Text;

                bool allValid = FieldValidator.ValidateName(name) &&
                               FieldValidator.ValidateEmail(email) &&
                               FieldValidator.ValidatePassword(password) &&
                               FieldValidator.ValidatePasswordConfirmation(password, confirmPassword);

                RegisterButton.IsEnabled = allValid;
                return allValid;
            }
            catch (Exception ex)
            {
                RegisterButton.IsEnabled = false;
                return false;
            }
        }

            private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
                if (UserNameTextBox == null || EmailTextBox == null ||
                    PasswordTextBox == null || ConfirmPasswordTextBox == null)
                {
                    MessageBox.Show("Ошибка инициализации полей", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    string name = (UserNameTextBox.Foreground.ToString() == "#FFCACACA" || UserNameTextBox.Text == "Введите имя пользователя") ? "" : UserNameTextBox.Text;
                    string email = (EmailTextBox.Foreground.ToString() == "#FFCACACA" || EmailTextBox.Text == "exam@yandex.ru") ? "" : EmailTextBox.Text;
                    string password = (PasswordTextBox.Foreground.ToString() == "#FFCACACA" || PasswordTextBox.Text == "Введите пароль") ? "" : PasswordTextBox.Text;
                    string confirmPassword = (ConfirmPasswordTextBox.Foreground.ToString() == "#FFCACACA" || ConfirmPasswordTextBox.Text == "Повторите пароль") ? "" : ConfirmPasswordTextBox.Text;

                    var validationResult = FieldValidator.ValidateRegistration(name, email, password, confirmPassword);

                    if (validationResult.IsValid)
                    {
                        // СОХРАНЕНИЕ В РЕПОЗИТОРИЙ
                        var newUser = new Desktop.Entities.UserModel
                        {
                            Name = name,
                            Email = email,
                            Password = password
                        };

                        Desktop.Repository.UserRepository.Register(newUser);

                        MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        MainEmpty mainEmptyWindow = new MainEmpty();
                        mainEmptyWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(validationResult.GetErrorMessage(), "Ошибки валидации",
                                      MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public static class FieldValidator
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Проверка по паттерну *@*.*
            string pattern = @"^.+@.+\..+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 3;
        }

        public static bool ValidatePasswordConfirmation(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public static ValidationResult ValidateRegistration(string name, string email, string password, string confirmPassword)
        {
            var result = new ValidationResult();

            if (!ValidateName(name))
                result.Errors.Add("Имя должно содержать не менее 3 символов");

            if (!ValidateEmail(email))
                result.Errors.Add("Email должен соответствовать формату *@*.*");

            if (!ValidatePassword(password))
                result.Errors.Add("Пароль должен содержать не менее 6 символов");

            if (!ValidatePasswordConfirmation(password, confirmPassword))
                result.Errors.Add("Пароли не совпадают");

            result.IsValid = result.Errors.Count == 0;
            return result;
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public string GetErrorMessage()
        {
            return string.Join("\n", Errors);
        }
    }

