namespace TaskManager.Searchers;

public class TaskLabelSearcher : ITaskSearcher
{
    public List<Task> Search(TaskCollections.ITaskCollection taskCollection, object label)
    {
        string labelToSearch = (string)label;
        List<Task> results = new List<Task>();
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