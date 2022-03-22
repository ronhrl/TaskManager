namespace TaskManager.Views;

public abstract class TasksView : IView
{
    protected TaskCollections.ITaskCollection? TaskCollection;
    protected readonly MainView MainView;

    protected TasksView(MainView mainView)
    {
        MainView = mainView;
    }

    public abstract void Start();
}