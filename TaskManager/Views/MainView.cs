namespace TaskManager.Views;

public abstract class MainView : IView
{
    private ViewFactory _viewFactory;
    private IInitialView _initialView;
    private ITasksView _tasksView;
    private ITaskView _taskView;
    private IEditTaskView _editTaskView;
    private ISearchView _searchView;
    private ISearchResultsView _searchResultsView;

    protected MainView(ViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
        _initialView = _viewFactory.CreateInitialView(this);
        _tasksView = _viewFactory.CreateTasksView(this);
        _taskView = _viewFactory.CreateTaskView(this);
        _editTaskView = _viewFactory.CreateEditTaskView(this);
        _searchView = _viewFactory.CreateSearchView(this);
        _searchResultsView = _viewFactory.CreateSearchResultsView(this);
    }

    public void ShowInitialView()
    {
        _initialView.Start();
    }

    public void ShowTasksView()
    {
        _tasksView.Start();
    }

    public void ShowTaskView(Task task)
    {
        _taskView.SetSelectedTask(task);
        _taskView.Start();
    }

    public void ShowEditTaskView(Task task)
    {
        _editTaskView.SetSelectedTask(task);
        _editTaskView.Start();
    }

    public void ShowSearchView()
    {
        _searchView.Start();
    }

    public void ShowSearchResultsView(List<Task> results)
    {
        _searchResultsView.SetResults(results);
        _searchResultsView.Start();
    }
    
    // todo add sorter
    
    public abstract void Start();
}