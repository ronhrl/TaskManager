namespace TaskManager.Views;

public abstract class ViewFactory
{
    // public abstract MainView CreateMainView();

    public abstract ITasksView CreateTasksView(MainView mainView);

    public abstract ITaskView CreateTaskView(MainView mainView);

    public abstract IEditTaskView CreateEditTaskView(MainView mainView);

    public abstract IInitialView CreateInitialView(MainView mainView);

    public abstract ISearchView CreateSearchView(MainView mainView);

    public abstract ISearchResultsView CreateSearchResultsView(MainView mainView);
}