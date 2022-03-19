namespace TaskManager.Models;

public interface ITaskManagerFilesStorage
{
    
    public void InsertNewTask(Task t);

    public void DeleteTaskFromDb(Task t);

    public void UpdateTaskInDb(Task t);

    public List<Task> GetTasksFromDb();

    public void InsertNewSubTask(Task t, Task subTask);

    public void DeleteSubTaskFromDb(Task subTask);

    public void UpdateSubTaskInDb(Task task);
}