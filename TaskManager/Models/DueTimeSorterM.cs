namespace TaskManager.Models;

public class DueTimeSorterM : ITaskSorter
{
    public List<Task> Sort(ITaskCollection taskCollection)
    {
        return taskCollection.OrderBy(f => f.DueTime).ToList();
    }
}