namespace TaskManager;

public interface ITaskSearcher
{
    List<Task> Search(ITaskCollection taskCollection, object param);
}