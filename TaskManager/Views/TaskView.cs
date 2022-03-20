namespace TaskManager.Views;

public abstract class TaskView : IView
{
    protected Task? SelectedTask;
    protected readonly MainView MainView;
    // public void SetSelectedTask(Task task);

    protected TaskView(MainView mainView, Task? selectedTask = null)
    {
        SelectedTask = selectedTask;
        MainView = mainView;
    }

    public void SetSelectedTask(Task task)
    {
        SelectedTask = task;
    }

    public abstract void Start();
}