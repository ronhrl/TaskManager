using TaskManager.TaskCollections;

namespace TaskManager.Searchers;

public class TaskLabelSearcher : ITaskSearcher
{
    public ITaskCollection Search(ITaskCollection taskCollection, object label)
    {
        string labelToSearch = (string)label;
        ITaskCollection results = new ListTaskCollection();
        foreach (Task task in taskCollection)
        {
            if (!task.Labels.Contains(labelToSearch))
            {
                continue;
            }
            results.Add(task);
        }

        return results;
    }
}