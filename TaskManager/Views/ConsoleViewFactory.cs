namespace TaskManager.Views;

public class ConsoleViewFactory : ViewFactory
{
    public override MainView CreateMainView()
    {
        return new ConsoleMainView(this);
    }

    public override TasksView CreateTasksView(MainView mainView)
    {
        return new ConsoleTasksView(mainView);
    }

    public override TaskView CreateTaskView(MainView mainView)
    {
        return new ConsoleTaskView(mainView);
    }

    public override EditTaskView CreateEditTaskView(MainView mainView)
    {
        return new ConsoleEditTaskView(mainView);
    }

    public override InitialView CreateInitialView(MainView mainView)
    {
        return new ConsoleInitialView(mainView);
    }

    public override SearchView CreateSearchView(MainView mainView)
    {
        return new ConsoleSearchView(mainView);
    }

    public override SearchResultsView CreateSearchResultsView(MainView mainView)
    {
        return new ConsoleSearchResultsView(mainView);
    }

    public override SortView CreateSortView(MainView mainView)
    {
        return new ConsoleSortView(mainView);
    }

    public override SortResultsView CreateSortResultsView(MainView mainView)
    {
        return new ConsoleSortResultsView(mainView);
    }
}