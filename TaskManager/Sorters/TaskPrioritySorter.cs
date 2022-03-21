using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.TaskCollections;

namespace TaskManager.Sorters;

public class TaskPrioritySorter : ITaskSorter
{

    public ITaskCollection Sort(ITaskCollection taskCollection)
    {
        ITaskCollection list = new ListTaskCollection(taskCollection.OrderBy(f => f.Priority).ToList());
        
        return list;
    }
}