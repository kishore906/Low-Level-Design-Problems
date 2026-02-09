using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.State
{
    public abstract class TaskState
    {
        public abstract void StartProgress(Models.Task task);
        public abstract void CompleteTask(Models.Task task);
        public abstract void ReopenTask(Models.Task task);
        public abstract Enums.TaskStatus GetStatus();
    }
}

/*
 📌 Why State Pattern is used:

Task behavior changes based on current state:

From TODO → can Start
From IN_PROGRESS → can Complete
From DONE → can Reopen
Some transitions are invalid

❌ Without State pattern:
You would have big if-else or switch:

if (status == TODO) { ... }
else if (status == IN_PROGRESS) { ... }


This leads to:

God methods
Hard to add new states
Violates Open/Closed Principle

✅ State pattern:

Encapsulates state-specific behavior
Makes transitions explicit
Easy to add new states later
Eliminates conditional logic explosion

📌 Interview reasoning:

State pattern is used to model task lifecycle transitions cleanly and avoid conditional logic scattered across Task methods.
 */