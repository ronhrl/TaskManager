namespace TaskManager;

public class Task
{
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    };

    // required fields
    private string _title;
    public string Title
    {
        get => _title; 
        // set => _title = value;
    }
    
    private DateTime _creationTime;
    public DateTime CreationTime
    {
        get => _creationTime;
        // set => _creationTime = value;
    }

    private TaskPriority _priority;
    public TaskPriority Priority
    {
        get => _priority;
        // set => _priority = value;
    }

    // optional fields
    private string? _description;
    public string? Description
    {
        get => _description;
    }
    
    private DateTime? _dueTime;
    public DateTime? DueTime
    {
        get => _dueTime;
    }

    public Task(string title, 
                TaskPriority priority = TaskPriority.Medium,
                string? description = null,
                DateTime? dueDate = null)
    {
        _title = title;
        _priority = priority;
        _creationTime = DateTime.Now;
        _description = description;
        _dueTime = dueDate;
    }
}