using TaskManager.Models;

namespace TaskManager;

public interface ITaskSorter
{
    List<Task> Sort(ITaskCollection taskCollection);
}