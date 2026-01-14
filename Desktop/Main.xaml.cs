using System;
using System.Collections.ObjectModel;
using System.Windows;
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

            Tasks = new ObservableCollection<TaskModel>
            {
                new TaskModel { Title = "Сходить на рыбалку", Time = "9:00am", Description = "Взять удочки", IsDone = false, Date = DateTime.Now },
                new TaskModel { Title = "Почитать книгу", Time = "11:00am", Description = "2 главы", IsDone = false, Date = DateTime.Now }
            };

            TasksListBox.ItemsSource = Tasks;
        }
        // Кнопка "Готово" - переносит задачу в разряд выполненных
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskModel selectedTask)
            {
                selectedTask.IsDone = true;

                // Чтобы UI увидел изменения в CheckBox и тексте, обновляем список
                TasksListBox.Items.Refresh();

                MessageBox.Show($"Задача '{selectedTask.Title}' выполнена!", "Ура");
            }
            else
            {
                MessageBox.Show("Сначала выбери задачу из списка!");
            }
        }

        // Кнопка "Удалить" - убирает задачу совсем
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

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
    AddTaskWindow addWindow = new AddTaskWindow();

            // Указываем владельца, чтобы оно открылось по центру главного окна
            addWindow.Owner = this;

            // Если в том окне нажали "Создать" (DialogResult = true)
            if (addWindow.ShowDialog() == true)
            {
                // Добавляем новую задачу в наш список на экране
                // Важно: в AddTaskWindow свойство должно называться CreatedTask или NewTask
                if (addWindow.CreatedTask != null)
                {
                    Tasks.Add(addWindow.CreatedTask);
                }
            }
        }
    }
}