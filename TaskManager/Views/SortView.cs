namespace TaskManager.Views;

public abstract class SortView : IView
{
    protected readonly MainView MainView;
    protected string[]? SortersList;

    protected SortView(MainView mainView)
    {
        MainView = mainView;
    }
    public abstract void Start();
}