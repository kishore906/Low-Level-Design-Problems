namespace Task_Management_System.Observer
{
    public interface ITaskObserver
    {
        void Update(Models.Task task, string changeType);
    }
}

/*
 
 📌 Why Observer Pattern is used:

Whenever something changes:

Assignee updated
Priority updated
Comment added
Status changed

Multiple components may need notification: (only for example)

Email service
Notification service
Activity feed
WebSocket clients
Analytics

❌ Without Observer:

Task would directly depend on:

EmailService.Notify()
WebSocketService.Push()
AuditService.Record()


This creates:

Tight coupling
Hard to extend
Hard to test

✅ Observer pattern:

Loose coupling
Easy to add new listeners
Task doesn’t care who is listening

📌 Interview reasoning:

Observer pattern is used to decouple Task from notification mechanisms and allow multiple subscribers to react to task changes.
 */