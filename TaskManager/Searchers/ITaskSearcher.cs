using TaskManager.TaskCollections;

namespace TaskManager.Searchers;

public interface ITaskSearcher
{
    ITaskCollection Search(TaskCollections.ITaskCollection taskCollection, object param);
}