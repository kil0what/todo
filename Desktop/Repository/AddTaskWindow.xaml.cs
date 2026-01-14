using System;
using System.Windows;
using Desktop.Entities;
using Desktop.Repository;

namespace Desktop
{
    public partial class AddTaskWindow : Window
    {
        // Свойство, чтобы передать созданную задачу обратно в главное окно
        public TaskModel CreatedTask { get; private set; }

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            CreatedTask = new TaskModel
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                Category = (CategoryComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString(),
                Time = DateTime.Now.ToString("t"), // Текущее время
                Date = DateTime.Now,
                IsDone = false
            };

            // Сохраняем в наш новый репозиторий
            TaskRepository.AddTask(CreatedTask);

            // Закрываем окно с результатом "ОК"
            this.DialogResult = true;
        }

        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}