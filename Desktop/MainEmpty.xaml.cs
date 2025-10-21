using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Desktop
{
    public partial class MainEmpty : Window
    {
        private bool isMenuVisible = false;

        public MainEmpty()
        {
            InitializeComponent();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isMenuVisible)
            {
                ShowLeftMenu();
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                if (isMenuVisible && !IsMouseOverMenu() && !IsMouseOverImage())
                {
                    HideLeftMenu();
                }
            };
            timer.Start();
        }

        // Показываем меню с анимацией
        private void ShowLeftMenu()
        {
            isMenuVisible = true;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.From = new Thickness(-199, 0, 800, 399); 
            animation.To = new Thickness(0, 0, 599, 399);      
            animation.Duration = TimeSpan.FromSeconds(0.3);

            LeftMenu.BeginAnimation(MarginProperty, animation);
        }

        private void HideLeftMenu()
        {
            isMenuVisible = false;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.From = new Thickness(0, 0, 599, 399);    
            animation.To = new Thickness(-199, 0, 800, 399);   
            animation.Duration = TimeSpan.FromSeconds(0.3);

            LeftMenu.BeginAnimation(MarginProperty, animation);
        }

        private bool IsMouseOverMenu()
        {
            try
            {
                Point mousePos = Mouse.GetPosition(LeftMenu);
                return mousePos.X >= 0 && mousePos.X <= LeftMenu.ActualWidth &&
                       mousePos.Y >= 0 && mousePos.Y <= LeftMenu.ActualHeight;
            }
            catch
            {
                return false;
            }
        }

        private bool IsMouseOverImage()
        {
            try
            {
                Point mousePos = Mouse.GetPosition(ProfileImage);
                return mousePos.X >= 0 && mousePos.X <= ProfileImage.ActualWidth &&
                       mousePos.Y >= 0 && mousePos.Y <= ProfileImage.ActualHeight;
            }
            catch
            {
                return false;
            }
        }

        private void ChangeImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.EndInit();

                    ProfileImage.Source = bitmap;
                    MessageBox.Show("Изображение профиля изменено!");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeImage();
        }

        // Обработчики для меню
        private void LeftMenu_MouseEnter(object sender, MouseEventArgs e)
        {
       
        }

        private void LeftMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                if (isMenuVisible && !IsMouseOverMenu() && !IsMouseOverImage())
                {
                    HideLeftMenu();
                }
            };
            timer.Start();
        }
    }
}