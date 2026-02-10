# Task Management System

A Low-Level Design (LLD) implementation of a **Task Management System** in C#, demonstrating multiple design patterns to solve common software design challenges. This project serves as a reference for applying GoF (Gang of Four) patterns in a real-world scenario.

## Requirements

1. The task management system should allow users to create, update, and delete tasks.
2. Each task should have a title, description, due date, priority, and status (e.g., pending, in_progress, completed).
3. Users should be able to assign tasks to other users and set reminders for tasks.
4. The system should support searching and filtering tasks based on various criteria (e.g., priority, due date, assigned user).
5. Users should be able to mark tasks as completed and view their task history.
6. The system should handle concurrent access to tasks and ensure data consistency.
7. The system should be extensible to accommodate future enhancements and new features.

## Classes, Interfaces and Enumerations

1. The User class represents a user in the task management system, with properties such as id, name, and email.
2. The TaskStatus enum defines the possible states of a task, such as pending, in_progress, and completed.
3. The TaskPriority enum defines the priority of the task, such as low, medium, high, and critical.
4. The Task class represents a task in the system, with properties like id, title, description, due date, priority, status, and assigned user.
5. The TaskManagerSystem class is the core of the task management system and follows the Singleton pattern to ensure a single instance of the task manager.
6. The TaskManagerSystem class uses necessary data structures to handle access to tasks and ensure thread safety.
7. The TaskManagerSystem class provides methods for creating, updating, deleting, searching, and filtering tasks, as well as marking tasks as completed and retrieving task history for a user.
8. The TaskManagementSystem class serves as the entry point of the application and demonstrates the usage of the task management system.

## 🚀 Features & Design Patterns

This project intentionally applies the following design patterns to create a robust, scalable, and maintainable architecture:

### 1. Singleton Pattern

- **Usage**: `TaskManagementSystem` class.
- **Purpose**: Ensures a single instance of the system manages all global states (users, tasks, and lists) throughout the application lifecycle.
- **Implementation**: Uses double-checked locking to ensure thread safety during initialization.

### 2. Builder Pattern

- **Usage**: `TaskBuilder` class (used via `TaskManagementSystem.CreateTask`).
- **Purpose**: Simplifies the creation of complex `Task` objects which have multiple optional parameters (tags, priority, due date, etc.).
- **Benefit**: Avoids "constructor telescoping" and ensures objects are always created in a valid state.

### 3. Observer Pattern

- **Usage**: `ITaskObserver` interface and `ActivityLogger` class.
- **Purpose**: Decouples the `Task` entity from the logging/notification system.
- **Behavior**: When a task changes (e.g., status update, assignee change, new comment), all registered observers (like `ActivityLogger`) are notified automatically without the `Task` class needing to know the details of the subscribers.

### 4. State Pattern

- **Usage**: `TaskState` abstract class with Concrete States (`TodoState`, `InProgressState`, `DoneState`).
- **Purpose**: Encapsulates state-specific behaviors and transition logic.
- **Behavior**: A task delegates behavior to its current state object. For example, a task in the `Todo` state can be started (moving to `InProgress`), but a task in the `Done` state cannot be started again. This eliminates complex `if-else` or `switch` statements in the `Task` class.

### 5. Strategy Pattern

- **Usage**: `TaskSortStrategy` abstract class with Concrete Strategies (`SortByDueDate`, `SortByPriority`).
- **Purpose**: Allows the sorting algorithm for search results to be selected or switched at runtime.
- **Benefits**: Adheres to the Open/Closed Principle—new sorting strategies can be added without modifying the search logic.

### 6. Composite Pattern

- **Usage**: `Task` class containing a list of `subtasks`.
- **Purpose**: Allows individual tasks and compositions of tasks (subtasks) to be treated uniformly. A task can contain other tasks, forming a hierarchical tree structure.

## 📂 Project Structure

- **Models**: Core domain entities (`Task`, `User`, `TaskList`, `Comment`, `Tag`).
- **Enums**: Enumerations for `TaskStatus`, `TaskPriority`.
- **Observer**: Interfaces (`ITaskObserver`) and implementations (`ActivityLogger`) for event handling.
- **State**: State classes managing the task lifecycle (`TaskState`, `TodoState`, etc.).
- **Strategy**: Algorithms for sorting tasks (`TaskSortStrategy`, etc.).
- **Program.cs**: Entry point demonstrating the system usage.
