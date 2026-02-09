using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Task_Management_System.Enums;
using Task_Management_System.State;

namespace Task_Management_System.Models
{
    public class Task
    {
        private readonly string id;
        private string title;
        private string description;
        private string dueDate;
        private TaskPriority priority;
        private readonly User createdBy;
        private User assignee;
        private TaskState currentState;
        private readonly HashSet<Tag> tags;
        private readonly List<Comment> comments;
        private readonly List<Task> subtasks;
        private readonly List<ActivityLog> activityLogs;

        // Getters
        public string GetId() => id;
        public string GetTitle() => title;
        public string GetDescription() => description;
        public TaskPriority GetPriority() => priority;
        public string GetDueDate() => dueDate;
        public User GetAssignee() => assignee;
        //public TaskStatus GetStatus() => CurrentState.GetStatus();

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
            //this.currentState = new TodoState(); // Initial state
            this.comments = new List<Comment>();
            this.subtasks = new List<Task>();
            this.activityLogs = new List<ActivityLog>();
            //this.observers = new List<ITaskObserver>();
            //AddLog($"Task created with title: {title}");
        }

        // Setter
        public void SetTitle(string title) => this.title = title;
        public void SetDescription(string description) => this.description = description;
    }

    // Builder Pattern
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

        public TaskBuilder(string title) {
            this.Id = Guid.NewGuid().ToString();
            this.Title = title;
        }

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

        public Task Build()
        {
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
 
 */