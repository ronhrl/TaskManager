namespace TaskManager.Views;

public abstract class SearchResultsView : IView
{
    protected readonly MainView MainView;
    protected List<Task>? Results;

    protected SearchResultsView(MainView mainView, List<Task>? results = null)
    {
        MainView = mainView;
        Results = results;
    }

    public void SetResults(List<Task> results)
    {
        Results = results;
    }
    public abstract void Start();
}