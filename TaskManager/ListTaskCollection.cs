using System.Collections;

namespace TaskManager;

public class ListTaskCollection : ITaskCollection
{
    public int Count
    {
        get => _taskList.Count;
    }
    public bool IsReadOnly { get; }

    private List<Task> _taskList;

    public ListTaskCollection()
    {
        _taskList = new List<Task>();
        IsReadOnly = false;
    }

    public ListTaskCollection(List<Task> taskList)
    {
        _taskList = new List<Task>();
        foreach (Task task in taskList)
        {
            _taskList.Add(task);
        }
        IsReadOnly = false;
    }

    public IEnumerator<Task> GetEnumerator()
    {
        return this._taskList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Task item)
    {
        if (Contains(item))
        {
            throw new InvalidOperationException("Task with the same title already exists.");
        }
        this._taskList.Add(item);
    }

    public void Clear()
    {
        this._taskList.Clear();
    }

    public bool Contains(Task item)
    {
        foreach (Task currTask in _taskList)
        {
            if (item.Equals(currTask))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(Task[] array, int arrayIndex)
    {
        this._taskList.CopyTo(array, arrayIndex);
    }

    public bool Remove(Task item)
    {
        return this._taskList.Remove(item);
    }
    
    public void Update(Task oldTask, Task newTask)
    {
        for (int i = 0; i < _taskList.Count; i++)
        {
            if (!oldTask.Equals(_taskList[i]))
            {
                continue;
            }

            if (ContainsExcept(newTask, i))
            {
                throw new InvalidOperationException("Task with the same title already exists.");
            }
            
            oldTask.CopyTaskValues(newTask);
        }

        throw new KeyNotFoundException("Task not found.");
    }

    public Task GetTaskAtIndex(int i)
    {
        return _taskList[i];
    }

    private bool ContainsExcept(Task task, int index)
    {
        for (int i = 0; i < _taskList.Count; i++)
        {
            if (task.Equals(_taskList[i]) && i != index)
            {
                return true;
            }
        }

        return false;
    }
    
    // public ITaskCollection Sort(IComparable property)
    // {
    //     throw new NotImplementedException();
    // }
}