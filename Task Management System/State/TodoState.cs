using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.State
{
    public class TodoState : TaskState
    {
        public override void StartProgress(Models.Task task)
        {
            //task.SetState(new InProgressState());
        }

        public override void CompleteTask(Models.Task task)
        {
            Console.WriteLine("Cannot complete a task that is not in progress.");
        }

        public override void ReopenTask(Models.Task task)
        {
            Console.WriteLine("Task is already in TO-DO state.");
        }

        public override Enums.TaskStatus GetStatus()
        {
            return Enums.TaskStatus.TODO;
        }
    }
}
