using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoList
{
    internal class Task
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public Task(int id, string description, DateTime dueDate)
        {
            ID = id;
            Description = description;
            DueDate = dueDate;
        }

    }
}
