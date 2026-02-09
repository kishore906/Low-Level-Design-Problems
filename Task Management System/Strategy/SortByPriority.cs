using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Strategy
{
    public class SortByPriority : TaskSortStrategy
    {
        public override void Sort(List<Models.Task> tasks)
        {
            // Higher priority comes first
            tasks.Sort((a, b) => b.GetPriority().CompareTo(a.GetPriority()));
        }
    }
}
