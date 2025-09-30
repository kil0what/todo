using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Проверяем, является ли текст placeholder'ом
                if (textBox.Name == "EmailTextBox" && textBox.Text == "pangcheo121o@gmail.com")
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
                // Возвращаем placeholder если поле пустое
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox.Name == "EmailTextBox")
                    {
                        textBox.Text = "pangcheo121o@gmail.com";
                        textBox.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCACACA");
                    }
                    else if (textBox.Name == "PasswordTextBox")
                    {
                        textBox.Text = "Введите пароль";
                        textBox.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCACACA");
                    }
                }
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            REGISTRATION registrationWindow = new REGISTRATION();
            registrationWindow.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainEmpty mainEmptyWindow = new MainEmpty();
            mainEmptyWindow.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}