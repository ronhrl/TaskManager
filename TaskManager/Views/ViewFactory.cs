namespace TaskManager.Views;

public abstract class ViewFactory
{
    public abstract MainView CreateMainView();

    public abstract TasksView CreateTasksView(MainView mainView);

    public abstract TaskView CreateTaskView(MainView mainView);

    public abstract EditTaskView CreateEditTaskView(MainView mainView);

    public abstract InitialView CreateInitialView(MainView mainView);

    public abstract SearchView CreateSearchView(MainView mainView);

    public abstract SearchResultsView CreateSearchResultsView(MainView mainView);

    public abstract SortView CreateSortView(MainView mainView);

    public abstract SortResultsView CreateSortResultsView(MainView mainView);
}