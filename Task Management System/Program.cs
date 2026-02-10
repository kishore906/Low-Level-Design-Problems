using Task_Management_System;
using Task_Management_System.Enums;
using Task_Management_System.Models;
using Task_Management_System.Strategy;

TaskManagementSystem taskManagementSystem = TaskManagementSystem.GetInstance();

// Create Users
User user1 = taskManagementSystem.CreateUser("Alice", "alice@example.com");
User user2 = taskManagementSystem.CreateUser("Bob", "bob@example.com");

// Create task lists
TaskList taskList1 = taskManagementSystem.CreateTaskList("Enhancements");
TaskList taskList2 = taskManagementSystem.CreateTaskList("Bug Fix");

// Create tasks
Task_Management_System.Models.Task task1 = taskManagementSystem.CreateTask("Enhancement Task", "Launch New Feature",
        "2024-02-15", TaskPriority.LOW, user1.GetId());
Task_Management_System.Models.Task subtask1 = taskManagementSystem.CreateTask("Enhancement sub task", "Design UI/UX",
        "2024-02-14", TaskPriority.MEDIUM, user1.GetId());
Task_Management_System.Models.Task task2 = taskManagementSystem.CreateTask("Bug Fix Task", "Fix API Bug",
        "2024-02-16", TaskPriority.HIGH, user2.GetId());

task1.AddSubtask(subtask1);

taskList1.AddTask(task1);
taskList2.AddTask(task2);

taskList1.Display();

// Update task status
subtask1.StartProgress();

// Assign task
subtask1.SetAssignee(user2);

taskList1.Display();

// Search tasks
List<Task_Management_System.Models.Task> searchResults = taskManagementSystem.SearchTasks("Task", new SortByDueDate());
Console.WriteLine("\nTasks with keyword Task:");
foreach (Task_Management_System.Models.Task task in searchResults)
{
    Console.WriteLine(task.GetTitle());
}

// Filter tasks by status
List<Task_Management_System.Models.Task> filteredTasks = taskManagementSystem.ListTasksByStatus(Task_Management_System.Enums.TaskStatus.TODO);
Console.WriteLine("\nTODO Tasks:");
foreach (Task_Management_System.Models.Task task in filteredTasks)
{
    Console.WriteLine(task.GetTitle());
}

// Mark a task as done
subtask1.CompleteTask();

// Get tasks assigned to a user
List<Task_Management_System.Models.Task> userTaskList = taskManagementSystem.ListTasksByUser(user2.GetId());
Console.WriteLine($"\nTask for {user2.GetName()}:");
foreach (Task_Management_System.Models.Task task in userTaskList)
{
    Console.WriteLine(task.GetTitle());
}

taskList1.Display();

// Delete a task
taskManagementSystem.DeleteTask(task2.GetId());

/*
 OUTPUT:

LOGGER: Task 'Enhancement Task' was updated. Change: subtask_added
--- Task List: Enhancements ---
- Enhancement Task [TODO, LOW, Due: 2024-02-15]
  - Enhancement sub task [TODO, MEDIUM, Due: 2024-02-14]
--------------------------------------------
LOGGER: Task 'Enhancement sub task' was updated. Change: status
LOGGER: Task 'Enhancement sub task' was updated. Change: assignee
--- Task List: Enhancements ---
- Enhancement Task [TODO, LOW, Due: 2024-02-15]
  - Enhancement sub task [IN_PROGRESS, MEDIUM, Due: 2024-02-14]
--------------------------------------------

Tasks with keyword Task:
Enhancement Task
Bug Fix Task

TODO Tasks:
Enhancement Task
Bug Fix Task
LOGGER: Task 'Enhancement sub task' was updated. Change: status

Task for Bob:
Enhancement sub task
--- Task List: Enhancements ---
- Enhancement Task [TODO, LOW, Due: 2024-02-15]
  - Enhancement sub task [DONE, MEDIUM, Due: 2024-02-14]
--------------------------------------------
 */