using TaskManager.Models;
using TaskManager.TaskCollections;

namespace TaskManager.Models;

public interface ITaskSorter
{
    List<Task> Sort(ITaskCollection taskCollection);
}