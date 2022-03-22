using TaskManager.Models;
using TaskManager.TaskCollections;

namespace TaskManager.Sorters;

public interface ITaskSorter
{
    ITaskCollection Sort(ITaskCollection taskCollection);
}