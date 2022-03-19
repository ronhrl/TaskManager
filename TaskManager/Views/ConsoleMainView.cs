namespace TaskManager.Views;

public class ConsoleMainView : MainView
{
    // private ViewFactory _viewFactory;
    // private IInitialView _initialView;
    // private ITasksView _tasksView;
    // private ITaskView _taskView;
    // private IEditTaskView _editTaskView;
    // private ISearchView _searchView;
    // private ISearchResultsView _searchResultsView;
    //
    // public ConsoleMainView(ViewFactory viewFactory)
    // {
    //     _initialView = CreateInitialView();
    //     _tasksView = CreateTasksView();
    //     _taskView = CreateTaskView();
    //     _editTaskView = CreateEditTaskView();
    //     // _searchView = CreateSearchView();
    //     // _searchResultsView = CreateSearchResultsView();
    // }
    //

    public ConsoleMainView(ViewFactory viewFactory) : base(viewFactory)
    {
    }

    public override void Start()
    {
        ShowInitialView();
    }
    //
    // public void ShowInitialView()
    // {
    //     _initialView.Start();
    // }
    //
    // public void ShowTasksView()
    // {
    //     _tasksView.Start();
    // }
    //
    // public void ShowTaskView(Task task)
    // {
    //     _taskView.SetSelectedTask(task);
    //     _taskView.Start();
    // }
    //
    // public void ShowEditTaskView(Task task)
    // {
    //     _editTaskView.SetSelectedTask(task);
    //     _editTaskView.Start();
    // }
    //
    // public void ShowSearchResults(List<Task> tasks)
    // {
    //     
    // }
    //
    // private IInitialView CreateInitialView()
    // {
    //     return new ConsoleInitialView(this);
    // }
    //
    // private ITasksView CreateTasksView()
    // {
    //     return new ConsoleTasksView(this);
    // }
    //
    // private ITaskView CreateTaskView()
    // {
    //     return new ConsoleTaskView(this);
    // }
    //
    // private IEditTaskView CreateEditTaskView()
    // {
    //     return new ConsoleEditTaskView(this);
    // }
}