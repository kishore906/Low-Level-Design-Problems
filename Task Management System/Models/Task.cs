using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Task_Management_System.Enums;
using Task_Management_System.Observer;
using Task_Management_System.State;

namespace Task_Management_System.Models
{
    public class Task
    {
        private readonly string id; // unique identifier

        // Basic Task properties (mutable)
        private string title;
        private string description;
        private string dueDate;
        private TaskPriority priority;

        private readonly User createdBy; // Creator of the Task (immutable)
        private User assignee; // Current assignee of the task
        private TaskState currentState; // Holds the current state of the task (State Pattern)
        private readonly HashSet<Tag> tags; // Tags associated with this task
        private readonly List<Comment> comments; // Comments added to this task
        
        private readonly List<Task> subtasks; // Subtasks (Composite Pattern: Task can contain child Tasks) 

        private readonly List<ActivityLog> activityLogs; // Activity logs 

        // Observers (Observer Pattern: subscribers to task changes)
        private readonly List<ITaskObserver> observers;

        // Lock object to make Task thread-safe
        private readonly object taskLock = new object();

       // Constructor (Builder Pattern)
        public Task(TaskBuilder builder)
        {
            this.id = builder.Id;
            this.title = builder.Title;
            this.description = builder.Description;
            this.dueDate = builder.DueDate;
            this.priority = builder.Priority;
            this.createdBy = builder.CreatedBy;
            this.assignee = builder.Assignee;
            this.tags = builder.Tags;
            this.currentState = new TodoState(); // Initial state of task (State Pattern)

            // Initialize collections
            this.comments = new List<Comment>();
            this.subtasks = new List<Task>();
            this.activityLogs = new List<ActivityLog>();
            this.observers = new List<ITaskObserver>();

            AddLog($"Task created with title: {title}");
        }

        // Assign a user to this task (Thread-safe)
        public void SetAssignee(User user) {
            lock (taskLock)
            {
                this.assignee = user;
                AddLog($"Assigned to {user.GetName()}");

                // Notify all observers about assignee change (Observer Pattern)
                NotifyObservers("assignee");
            }
        }

        // Update priority of task
        public void UpdatePriority(TaskPriority priority) {
            lock(taskLock)
            {
                this.priority = priority;

                // Notify observers that priority changed
                NotifyObservers("priority");
            }
        }

        // Add a new comment to the task
        public void AddComment(Comment comment)
        {
            lock (taskLock)
            {
                comments.Add(comment);
                AddLog($"Comment added by {comment.GetAuthor().GetName()}");

                // Notify observers about comment addition
                NotifyObservers("comment");
            }
        }

        // Add a subtask (Composite Pattern)
        public void AddSubtask(Task subtask)
        {
            lock (taskLock)
            {
                subtasks.Add(subtask);
                AddLog($"Subtask added: {subtask.GetTitle()}");

                // Notify observers about new subtask
                NotifyObservers("subtask_added");
            }
        }

        // -------------------- State Pattern Methods --------------------

        // Change the current state of the task
        public void SetState(TaskState state)
        {
            this.currentState = state;
            AddLog($"Status changed to: {state.GetStatus()}");

            // Notify observers about status change
            NotifyObservers("status");
        }

        // Delegates behavior to the current state
        public void StartProgress() => currentState.StartProgress(this);
        public void CompleteTask() => currentState.CompleteTask(this);
        public void ReopenTask() => currentState.ReopenTask(this);

        // -------------------- Observer Pattern Methods --------------------

        // Register an observer
        public void AddObserver(ITaskObserver observer) => observers.Add(observer);

        // Unregister an observer
        public void RemoveObserver(ITaskObserver observer) => observers.Remove(observer);

        // Notify all observers about a specific change
        public void NotifyObservers(string changeType)
        {
            foreach (var observer in observers)
            {
                observer.Update(this, changeType);
            }
        }

        // -------------------- Utility Methods --------------------

        // Add an entry to activity logs
        public void AddLog(string logDescription)
        {
            activityLogs.Add(new ActivityLog(logDescription));
        }

        // Check if this task has subtasks (Composite Pattern helper)
        public bool IsComposite() => subtasks.Count > 0;

        // Display task hierarchy recursively (Composite Pattern)
        public void Display(string indent = "")
        {
            Console.WriteLine($"{indent}- {title} [{GetStatus()}, {priority}, Due: {dueDate}]");

            foreach (var subtask in subtasks)
            {
                subtask.Display(indent + "  ");
            }
        }

        // ---------------------------- Getters ------------------------------
        public string GetId() => id;
        public string GetTitle() => title;
        public string GetDescription() => description;
        public TaskPriority GetPriority() => priority;
        public string GetDueDate() => dueDate;
        public User GetAssignee() => assignee;
        public Enums.TaskStatus GetStatus() => currentState.GetStatus();

        // ----------------------------- Setters --------------------------
        public void SetTitle(string title) => this.title = title;
        public void SetDescription(string description) => this.description = description;
    }

    // Builder Pattern: Used to construct Task objects step-by-step
    public class TaskBuilder { 
        // Properties
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string DueDate { get; private set; }
        public TaskPriority Priority { get; private set; }
        public User CreatedBy { get; private set; }
        public User Assignee { get; private set; }
        public HashSet<Tag> Tags { get; private set; } = new HashSet<Tag>();

        // Mandatory field (title)
        public TaskBuilder(string title) {
            this.Id = Guid.NewGuid().ToString();
            this.Title = title;
        }

        // Optional fields
        public TaskBuilder SetDescription(string description)
        {
            this.Description = description;
            return this;
        }

        public TaskBuilder SetDueDate(string dueDate)
        {
            this.DueDate = dueDate;
            return this;
        }

        public TaskBuilder SetPriority(TaskPriority priority)
        {
            this.Priority = priority;
            return this;
        }

        public TaskBuilder SetAssignee(User assignee)
        {
            this.Assignee = assignee;
            return this;
        }

        public TaskBuilder SetCreatedBy(User createdBy)
        {
            this.CreatedBy = createdBy;
            return this;
        }

        public TaskBuilder SetTags(HashSet<Tag> tags)
        {
            this.Tags = tags;
            return this;
        }

        // Final build method that creates Task object
        public Task Build()
        {
            // Here we can add validation if needed
            // eg: if(CreatedBy == null) throw new Exception("Creator required");
            return new Task(this);
        }
    }
}


/*
 
 🧱 1. Builder Pattern

📍 Where:

class TaskBuilder { ... }
public Task(TaskBuilder builder) { ... }


📌 Why Builder is used here:

Your Task object has:

Many optional fields
Some mandatory fields (Id, Title)
Some complex fields (Tags, Assignee, CreatedBy)

Without Builder, constructor would look like this 😬:

new Task(id, title, description, dueDate, priority, createdBy, assignee, tags)


❌ Problems:

Constructor explosion
Parameter order bugs
Hard to read
Hard to extend later

✅ Builder solves:

Step-by-step object construction
Readable API
Immutable core fields (id, createdBy)
Validations can be centralized in Build()

📌 Interview reasoning:

Builder is used because Task is a complex object with optional parameters and we want readable, safe construction without constructor overloading explosion.

This Task domain model uses Builder for safe object construction, State to manage task lifecycle transitions, Observer to notify multiple listeners of changes, and Composite to support nested subtasks. The design follows SOLID principles and ensures thread safety using locking.
 
 */