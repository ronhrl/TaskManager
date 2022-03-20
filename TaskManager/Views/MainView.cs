namespace TaskManager.Views;

public abstract class MainView : IView
{
    // private readonly ViewFactory _viewFactory;
    private readonly IInitialView _initialView;
    private readonly ITasksView _tasksView;
    private readonly ITaskView _taskView;
    private readonly IEditTaskView _editTaskView;
    private readonly ISearchView _searchView;
    private readonly ISearchResultsView _searchResultsView;

    protected MainView(ViewFactory viewFactory)
    {
        // _viewFactory = viewFactory;
        _initialView = viewFactory.CreateInitialView(this);
        _tasksView = viewFactory.CreateTasksView(this);
        _taskView = viewFactory.CreateTaskView(this);
        _editTaskView = viewFactory.CreateEditTaskView(this);
        _searchView = viewFactory.CreateSearchView(this);
        _searchResultsView = viewFactory.CreateSearchResultsView(this);
    }

    public void ShowInitialView()
    {
        _initialView.Start();
    }

    public void ShowTasksView()
    {
        try
        {
            _tasksView.Start();    
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }

    public void ShowTaskView(Task task)
    {
        try
        {
            _taskView.SetSelectedTask(task);
            _taskView.Start();    
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }

    public void ShowEditTaskView(Task task)
    {
        try
        {
            _editTaskView.SetSelectedTask(task);
            _editTaskView.Start();    
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }

    public void ShowSearchView()
    {
        try
        {
            _searchView.Start();    
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }

    public void ShowSearchResultsView(List<Task> results)
    {
        try
        {
            _searchResultsView.SetResults(results);
            _searchResultsView.Start();
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }
    
    // todo add sorter
    
    public abstract void Start();

    private void OnError(Exception e)
    {
        Console.WriteLine($"Error! {e.Message}\nReturning to home page");
        Thread.Sleep(2000);
        _initialView.Start();
    }
}