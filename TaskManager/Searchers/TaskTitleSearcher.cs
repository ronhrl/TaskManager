namespace TaskManager.Searchers;

public class TaskTitleSearcher : ITaskSearcher
{
    public List<Task> Search(TaskCollections.ITaskCollection taskCollection, object title)
    {
        string titleToSearch = (string) title;
        List<Task> results = new List<Task>();
        foreach (Task task in taskCollection)
        {
            if (!task.Title.Equals(titleToSearch))
            {
                continue;
            }
            results.Add(task);
        }
        return results;
    }
}