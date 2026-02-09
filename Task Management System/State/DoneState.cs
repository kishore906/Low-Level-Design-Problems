using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.State
{
    public class DoneState : TaskState
     {
        public override void StartProgress(Models.Task task)
        {
            Console.WriteLine("Cannot start a completed task. Reopen it first.");
        }

        public override void CompleteTask(Models.Task task)
        {
            Console.WriteLine("Task is already done.");
        }

        public override void ReopenTask(Models.Task task)
        {
            //task.SetState(new TodoState());
        }

        public override Enums.TaskStatus GetStatus() => Enums.TaskStatus.DONE;
    }
}
