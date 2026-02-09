using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.State
{
    public class InProgressState : TaskState
    {
        public override void StartProgress(Models.Task task)
        {
            Console.WriteLine("Task is already in progress.");
        }

        public override void CompleteTask(Models.Task task)
        {
            //task.SetState(new DoneState());
        }

        public override void ReopenTask(Models.Task task)
        {
            //task.SetState(new TodoState());
        }

        public override Enums.TaskStatus GetStatus() => Enums.TaskStatus.IN_PROGRESS;
    }
}
