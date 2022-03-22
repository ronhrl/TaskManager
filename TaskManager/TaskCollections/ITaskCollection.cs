namespace TaskManager.TaskCollections;

public interface ITaskCollection : ICollection<Task>
{
    public void Update(Task oldTask, Task newTask);

    public Task GetTaskAtIndex(int i);
}