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

    private bool _isDone;
    public bool IsDone { get; }

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

    private List<string> _labels;
    public List<string> Labels
    {
        get => _labels;
    }

    private List<Task> _subTasks;

    public List<Task> SubTasks
    {
        get => _subTasks;
    }

    public Task(string title, 
                TaskPriority priority = TaskPriority.Medium,
                string? description = null,
                DateTime? dueDate = null,
                List<string>? labels = null,
                List<Task>? subTasks = null)
    {
        _isDone = false;
        _title = title;
        _priority = priority;
        _creationTime = DateTime.Now;
        _description = description;
        _dueTime = dueDate;
        _labels = labels ?? new List<string>();
        _subTasks = subTasks ?? new List<Task>();
    }

    public void MarkAsDone()
    {
        _isDone = true;
    }

    public void MarkAsUndone()
    {
        _isDone = false;
    }

    public void AddLabel(string label)
    {
        this._labels.Add(label);
    }

    public void RemoveLabel(string label)
    {
        this._labels.Remove(label);
    }

    public void AddSubTask(Task subTask)
    {
        _subTasks.Add(subTask);
    }

    public void RemoveSubTask(Task subTask)
    {
        _subTasks.Remove(subTask);
    }

    public string GetPriorityAsString()
    {
        if (this.Priority == TaskPriority.Low)
        {
            return "Low";
        } 
        if (this.Priority == TaskPriority.Medium)
        {
            return "Medium";
        }
        return "High";
    }

    public override string ToString()
    {
        string task = "";
        task += "Title: " + Title + "\n";
        task += "Creation Time: " + CreationTime + "\n";
        task += "Is Done: " + IsDone + "\n";
        task += "Priority: " + GetPriorityAsString() + "\n";

        if (Description != null)
        {
            task += "Description: " + Description + "\n";
        }

        if (Labels.Count > 0)
        {
            task += "Labels:\n";
            foreach (string label in Labels)
            {
                task += "\t" + label;
            }

            task += "\n";
        }

        if (DueTime != null)
        {
            task += "Due Time: " + DueTime + "\n";
        }

        if (SubTasks.Count > 0)
        {
            task += "Sub Tasks:\n";
            int i = 1;
            foreach (Task subTask in SubTasks)
            {
                task += $"{i}.\n";
                task += subTask.ToString();
                task += "\n";
            }
        }
        
        return task;
    }
}