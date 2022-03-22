using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.TaskCollections;

namespace TaskManager.Sorters;

public class DueTimeSorter : ITaskSorter
{

    public ITaskCollection Sort(ITaskCollection taskCollection)
    {
        ITaskCollection list = new ListTaskCollection(taskCollection.OrderBy(f => f.DueTime).ToList());
        
        return list;
    }
}