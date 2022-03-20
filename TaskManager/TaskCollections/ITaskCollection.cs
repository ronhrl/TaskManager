namespace TaskManager.TaskCollections;

public interface ITaskCollection : ICollection<Task>
{
    // ITaskCollection Sort(IComparable property);

    public void Update(Task oldTask, Task newTask);

    public Task GetTaskAtIndex(int i);
}