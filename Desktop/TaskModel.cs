using System;

namespace Desktop.Entities
{
    public class TaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
        public string Category { get; set; }
    }
}