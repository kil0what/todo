using System.Collections.Generic;
using Desktop.Entities;

namespace Desktop.Repository
{
    public static class TaskRepository
    {
        // Список для хранения всех задач в памяти
        private static List<TaskModel> _tasks = new List<TaskModel>();

        public static void AddTask(TaskModel task)
        {
            _tasks.Add(task);
        }

        public static List<TaskModel> GetAllTasks()
        {
            return _tasks;
        }
    }
}