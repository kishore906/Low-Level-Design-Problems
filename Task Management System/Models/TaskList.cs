using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Models
{
    public class TaskList
    {
        private readonly string id;
        private readonly string name;
        private readonly List<Task> tasks;

        // Constructor
        public TaskList(string name) {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            tasks = new List<Task>();
        }

        public string GetId() => id;
        public string GetName() => name;
    }
}
