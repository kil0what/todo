using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using Desktop.Entities;

namespace Desktop
{
    public partial class Main : Window
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }

        public Main(UserModel user)
        {
            InitializeComponent();

            if (user != null)
            {
                UserNameLabel.Text = user.Name;
            }

            // Инициализируем список задач
            Tasks = new ObservableCollection<TaskModel>
            {
                new TaskModel { Title = "Сходить на рыбалку", Time = "9:00am", Description = "Взять удочки", IsDone = false, Date = DateTime.Now, Category = "Отдых" },
                new TaskModel { Title = "Почитать книгу", Time = "11:00am", Description = "2 главы", IsDone = false, Date = DateTime.Now, Category = "Учеба" }
            };

            TasksListBox.ItemsSource = Tasks;
        }

        // --- ЛАБОРАТОРНАЯ №6: ФИЛЬТРАЦИЯ ---
        private void Category_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TextBlock clickedCategory)
            {
                string category = clickedCategory.Text;

                if (category == "Все")
                {
                    // Сбрасываем фильтр — показываем всё
                    TasksListBox.ItemsSource = Tasks;
                }
                else
                {
                    // Фильтруем основной список Tasks по нажатой категории
                    var filtered = Tasks.Where(t => t.Category == category).ToList();
                    TasksListBox.ItemsSource = filtered;
                }
            }
        }

        // Кнопка "Готово"
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskModel selectedTask)
            {
                selectedTask.IsDone = true;
                TasksListBox.Items.Refresh();
                MessageBox.Show($"Задача '{selectedTask.Title}' выполнена!", "Ура");
            }
            else
            {
                MessageBox.Show("Сначала выбери задачу из списка!");
            }
        }

        // Кнопка "Удалить"
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskModel selectedTask)
            {
                var result = MessageBox.Show("Точно удалить задачу?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Tasks.Remove(selectedTask);
                }
            }
            else
            {
                MessageBox.Show("Выбери, что удалять-то.");
            }
        }

        // Открытие окна добавления задачи (Лаба 5)
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addWindow = new AddTaskWindow();
            addWindow.Owner = this;

            if (addWindow.ShowDialog() == true)
            {
                if (addWindow.CreatedTask != null)
                {
                    Tasks.Add(addWindow.CreatedTask);
                }
            }
        }
    }
}