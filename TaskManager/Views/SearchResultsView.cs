using TaskManager.TaskCollections;

namespace TaskManager.Views;

public abstract class SearchResultsView : IView
{
    protected readonly MainView MainView;
    protected ITaskCollection? Results;

    protected SearchResultsView(MainView mainView, ITaskCollection? results = null)
    {
        MainView = mainView;
        Results = results;
    }

    public void SetResults(ITaskCollection results)
    {
        Results = results;
    }
    public abstract void Start();
}