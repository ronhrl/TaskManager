using TaskManager.TaskCollections;

namespace TaskManager.Views;

public abstract class MainView : IView
{
    private readonly InitialView _initialView;
    private readonly TasksView _tasksView;
    private readonly TaskView _taskView;
    private readonly EditTaskView _editTaskView;
    private readonly SearchView _searchView;
    private readonly SearchResultsView _searchResultsView;
    private readonly SortView _sortView;
    private readonly SortResultsView _sortResultsView;

    protected MainView(ViewFactory viewFactory)
    {
        _initialView = viewFactory.CreateInitialView(this);
        _tasksView = viewFactory.CreateTasksView(this);
        _taskView = viewFactory.CreateTaskView(this);
        _editTaskView = viewFactory.CreateEditTaskView(this);
        _searchView = viewFactory.CreateSearchView(this);
        _searchResultsView = viewFactory.CreateSearchResultsView(this);
        _sortView = viewFactory.CreateSortView(this);
        _sortResultsView = viewFactory.CreateSortResultsView(this);
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

    public void ShowSearchResultsView(ITaskCollection results)
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

    public void ShowSortView()
    {
        try
        {
            _sortView.Start();
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }
    
    public void ShowSortResultsView(ITaskCollection results)
    {
        try
        {
            _sortResultsView.SetResults(results);
            _sortResultsView.Start();
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }
    
    public abstract void Start();

    private void OnError(Exception e)
    {
        Console.WriteLine($"Error! {e.Message}\nReturning to home page");
        Thread.Sleep(2000);
        _initialView.Start();
    }
}