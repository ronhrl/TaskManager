using TaskManager.TaskCollections;

namespace TaskManager.Searchers;

public class TaskTitleSearcher : ITaskSearcher
{
    public ITaskCollection Search(ITaskCollection taskCollection, object title)
    {
        string titleToSearch = (string) title;
        ITaskCollection results = new ListTaskCollection();
        foreach (Task task in taskCollection)
        {
            if (!task.Title.Contains(titleToSearch))
            {
                continue;
            }
            results.Add(task);
        }
        return results;
    }
}