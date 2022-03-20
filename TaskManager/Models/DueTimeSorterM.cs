namespace TaskManager.Models;

public class DueTimeSorterM : ITaskSorter
{
    public DueTimeSorterM()
    {
        
    }
    public List<Task> Sort(ITaskCollection taskCollection)
    {
        return taskCollection.OrderBy(f => f.DueTime).ToList();
    }
}