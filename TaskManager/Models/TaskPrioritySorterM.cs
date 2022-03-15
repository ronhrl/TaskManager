using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Models;

public class TaskPrioritySorterM : ITaskSorter
{

    public List<Task> Sort(ITaskCollection taskCollection)
    {
        return taskCollection.OrderBy(f => f.Priority).ToList();
    }

    // static void Main(string[] args)
    // {
    //     Task t = new Task("yit", Task.TaskPriority.Low, "fffffff", null, null, null);
    //     Task t2 = new Task("aaarrr", Task.TaskPriority.High, "bbb", null, null, null);
    //     // Task t3 = new Task("ron", Task.TaskPriority.High, "cccc", null, null, null);
    //     Task t4 = new Task("ron", Task.TaskPriority.Medium, "ddddd", null, null, null);
    //     ITaskCollection t1 = new ListTaskCollection();
    //     TaskPrioritySorter p = new TaskPrioritySorter();
    //     t1.Add(t);
    //     t1.Add(t2);
    //     t1.Add(t4);
    //     List<Task> t6 = p.Sort(t1);
    //     foreach (Task t7 in t6)
    //     {
    //         Console.WriteLine(t7.ToString());  
    //     }
    //     //Console.WriteLine(t6.ToString());
    // }
}
