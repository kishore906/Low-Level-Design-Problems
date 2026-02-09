using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Strategy
{
    public class SortByDueDate : TaskSortStrategy
    {
        public override void Sort(List<Models.Task> tasks)
        {
            tasks.Sort((a, b) => string.Compare(a.GetDueDate(), b.GetDueDate(), StringComparison.Ordinal));
        }
    }
}
