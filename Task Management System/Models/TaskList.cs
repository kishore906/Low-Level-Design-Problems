using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Models
{
    // This class is like a container that groups multiple 'Task' objects
    public class TaskList
    {
        private readonly string id; // unique identifier for the task list (immutable)
        private readonly string name; // Name of the list (immutable)
        private readonly List<Task> tasks; // Collection of tasks belonging to this list
        private readonly object listlock = new object(); // Lock object to make TaskList thread-safe

        // Constructor
        public TaskList(string name) {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            tasks = new List<Task>();
        }

        // Getters for id and name (encapsulation)
        public string GetId() => id;
        public string GetName() => name;

        // Adds a task to the list (thread-safe)
        public void AddTask(Models.Task task) { 
            lock (listlock) // ensures that only one thread modifies the list at a time
            {
                tasks.Add(task);
            }
        }

        // Returns all tasks in the list (thread-safe)
        public List<Models.Task> GetTasks()
        {
            lock (listlock)
            {
                // We return a new list so external callers cannot modify internal state
                return new List<Models.Task>(tasks); // return copy
            }
        }

        // Displays the task list and all tasks inside it
        public void Display()
        {
            Console.WriteLine($"--- Task List: {name} ---");

            // Iterate over each task and display it
            // Task.Display() already handles subtasks recursively (Composite Pattern)
            foreach (var task in tasks)
            {
                task.Display("");
            }
            Console.WriteLine("--------------------------------------------");
        }
    }
}

/*
 Responsibilities of TaskList

This class:

--> Holds a list of Task objects
--> Generates unique id for the list
--> Is thread-safe (multiple users/services can add tasks concurrently)
--> Allows adding tasks
--> Allows reading tasks safely
--> Displays all tasks in that list

It does not:

--> Control task behavior (that’s Task’s job)
--> Manage task lifecycle (that’s TaskState)
--> Notify observers (that’s Task’s job)

Explanation:
TaskList acts as a thread-safe container that groups multiple tasks into logical lists (like board columns). It encapsulates task collection management and uses defensive copying to prevent external mutation of internal state.
 */