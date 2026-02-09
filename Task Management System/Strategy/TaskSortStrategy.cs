using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Strategy
{
    public abstract class TaskSortStrategy
    {
        public abstract void Sort(List<Models.Task> tasks);
    }
}
