using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Observer
{
    public class ActivityLogger : ITaskObserver
    {
        public void Update(Models.Task task, string changeType) {
            Console.WriteLine($"LOGGER: Task '{task.GetTitle()}' was updated. Change: {changeType}");
        }
    }
}
