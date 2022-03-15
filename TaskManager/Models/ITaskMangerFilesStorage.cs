namespace TaskManager.Models;

public interface ITaskManagerFilesStorage
{
    
    public void InsertNewTaskM(Task task);

    public void DeleteTaskFromDbM(Task t);

    public void UpdateTaskInDbM(Task task);

    public List<Task> GetTasksFromDbM();
}