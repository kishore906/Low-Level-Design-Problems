using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Management_System.Enums;
using Task_Management_System.Models;
using Task_Management_System.Observer;
using Task_Management_System.Strategy;

namespace Task_Management_System
{
    /*
     *TaskManagementSystem is a Singleton service/facade that coordinates creation, storage, searching, and retrieval of users, tasks, and task lists. It integrates Builder for task creation, Observer for notifications, and Strategy for flexible sorting.
     */
    public class TaskManagementSystem
    {
        // Singleton instance
        private static TaskManagementSystem instance;

        // Lock object for thread-safe singleton initialization
        private static readonly object lockobject = new object();

        // In-memory storage for users, tasks, and task lists
        private readonly Dictionary<string, User> users = new Dictionary<string, User>();
        private readonly Dictionary<string, Models.Task> tasks = new Dictionary<string, Models.Task>();
        private readonly Dictionary<string, TaskList> taskLists = new Dictionary<string, TaskList>();

        // Private constructor to prevent direct instantiation (Singleton)
        private TaskManagementSystem() { }

        // Returns the single instance of TaskManagementSystem (Double-checked locking Singleton)
        public static TaskManagementSystem GetInstance()
        {
            if (instance == null)
            {
                lock (lockobject)
                {
                    if (instance == null)
                    {
                        instance = new TaskManagementSystem();
                    }
                }
            }
            return instance;
        }

        // Creates a new user and stores it in memory
        public User CreateUser(string name, string email) {
            User user = new User(name, email);
            users[user.GetId()] = user;
            return user;
        }

        // Creates a new task list
        public TaskList CreateTaskList(string listName)
        {
            TaskList taskList = new TaskList(listName);
            taskLists[taskList.GetId()] = taskList; // store list by Id
            return taskList;
        }

        // Creates a new task and associates it with a creator
        public Models.Task CreateTask(string title, string description, string dueDate,
                              TaskPriority priority, string createdByUserId)
        {
            // Validate creator exists
            if (!users.TryGetValue(createdByUserId, out User? createdBy) || createdBy is null)
            {
                throw new ArgumentException("User not found.");
            }

            // Build task using Builder Pattern
            Models.Task task = new TaskBuilder(title)
                    .SetDescription(description)
                    .SetDueDate(dueDate)
                    .SetPriority(priority)
                    .SetCreatedBy(createdBy)
                    .Build();

            // Attach an observer for logging / notifications (Observer Pattern)
            task.AddObserver(new ActivityLogger());

            // Store task in memory
            tasks[task.GetId()] = task;

            return task;
        }

        // Returns all tasks assigned to a given user
        public List<Models.Task> ListTasksByUser(string userId)
        {
            // Validate user exists
            if (!users.TryGetValue(userId, out User? user))
            {
                return new List<Models.Task>();
            }

            // Filter tasks by assignee
            return tasks.Values
                        .Where(task => task.GetAssignee() == user)
                        .ToList();
        }

        // Returns all tasks with a given status (e.g., TODO, IN_PROGRESS, DONE)
        public List<Models.Task> ListTasksByStatus(Enums.TaskStatus status)
        {
            return tasks.Values
                        .Where(task => task.GetStatus() == status)
                        .ToList();
        }

        // Deletes a task by ID
        public void DeleteTask(string taskId)
        {
            tasks.Remove(taskId);
        }

        // Searches tasks by keyword and sorts using a pluggable strategy (Strategy Pattern)
        public List<Models.Task> SearchTasks(string keyword, TaskSortStrategy sortingStrategy)
        {
            List<Models.Task> matchingTasks = new List<Models.Task>();

            // Find tasks whose title or description contains the keyword
            foreach (var task in tasks.Values)
            {
                if (task.GetTitle().Contains(keyword) ||
                    task.GetDescription().Contains(keyword))
                {
                    matchingTasks.Add(task);
                }
            }

            // Apply sorting strategy (e.g., by priority, due date, etc.)
            sortingStrategy.Sort(matchingTasks);

            return matchingTasks;
        }
    }
}

