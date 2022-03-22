using TaskManager.TaskCollections;

namespace TaskManager.Models;

public interface ITaskManagerStorage
{
    
    public void InsertNewTask(Task t);

    public void DeleteTaskFromDb(Task t);

    public void UpdateTaskInDb(Task oldTask, Task newTask);

    public ITaskCollection GetTasksFromDb();

    public void InsertNewSubTask(Task t, Task subTask);

    public void DeleteSubTaskFromDb(Task subTask);

    public void UpdateSubTaskInDb(Task oldTask, Task newTask);

    public void AddLabel(Task t, string label);

    public void UpdateLabel(string oldLabel, string newLabel);

    public void DeleteLabelFromDb(string label);

    public List<string> GetLabelsFromDb(string taskTitle);

    public List<Task> GetSubTasksFromDb(string taskTitle);

}