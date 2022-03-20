namespace TaskManager.Views;

public abstract class SearchView : IView
{
    protected readonly MainView MainView;
    protected readonly string[] SearchersList;

    protected SearchView(MainView mainView)
    {
        MainView = mainView;
        SearchersList = TestController.Instance.GetSearchers();
    }

    public abstract void Start();
}