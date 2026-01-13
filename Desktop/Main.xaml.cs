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

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskModel selectedTask)
            {
                selectedTask.IsDone = true;
                TasksListBox.Items.Refresh();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskModel selectedTask)
            {
                Tasks.Remove(selectedTask);
            }
        }
    }
}