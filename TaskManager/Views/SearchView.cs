namespace TaskManager.Views;

public abstract class SearchView : IView
{
    protected readonly MainView MainView;
    protected string[]? SearchersList;

    protected SearchView(MainView mainView)
    {
        MainView = mainView;
    }

    public abstract void Start();
}