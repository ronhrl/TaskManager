using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.TaskCollections;

namespace TaskManager.Sorters;

public class CreationTimeSorter : ITaskSorter
{

    public ITaskCollection Sort(ITaskCollection taskCollection)
    {
        ITaskCollection list = new ListTaskCollection(taskCollection.OrderBy(f => f.CreationTime).ToList());
        
        return list;
    }
}