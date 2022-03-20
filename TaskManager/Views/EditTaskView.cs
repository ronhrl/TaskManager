namespace TaskManager.Views;

public abstract class EditTaskView : IView
{
    protected readonly MainView MainView;
    protected Task? SelectedTask;
    protected Task? UpdatedTask;

    protected EditTaskView(MainView mainView, Task? selectedTask = null)
    {
        MainView = mainView;
        SelectedTask = selectedTask;
        if (SelectedTask != null)
        {
            UpdatedTask = new Task(SelectedTask.Title);
            UpdatedTask.CopyTaskValues(SelectedTask);
        }
        else
        {
            UpdatedTask = null;
        }
    }

    public void SetSelectedTask(Task task)
    {
        SelectedTask = task;
        SetUpdatedTask(SelectedTask);
    }
    
    private void SetUpdatedTask(Task task)
    {
        UpdatedTask ??= new Task(task.Title);
        UpdatedTask.CopyTaskValues(task);
    }
    public abstract void Start();
}