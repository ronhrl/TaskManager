namespace TaskManager.Views;

public abstract class TasksView : IView
{
    protected ITaskCollection TaskCollection;
    protected readonly MainView MainView;

    protected TasksView(MainView mainView)
    {
        TaskCollection = TestController.Instance.GetTasks();
        MainView = mainView;
    }

    public abstract void Start();
}