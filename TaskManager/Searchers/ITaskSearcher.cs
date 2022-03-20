namespace TaskManager.Searchers;

public interface ITaskSearcher
{
    List<Task> Search(TaskCollections.ITaskCollection taskCollection, object param);
}