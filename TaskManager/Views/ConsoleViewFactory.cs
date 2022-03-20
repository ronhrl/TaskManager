namespace TaskManager.Views;

public class ConsoleViewFactory : ViewFactory
{
    public override MainView CreateMainView()
    {
        return new ConsoleMainView(this);
    }

    public override ITasksView CreateTasksView(MainView mainView)
    {
        return new ConsoleTasksView(mainView);
    }

    public override ITaskView CreateTaskView(MainView mainView)
    {
        return new ConsoleTaskView(mainView);
    }

    public override IEditTaskView CreateEditTaskView(MainView mainView)
    {
        return new ConsoleEditTaskView(mainView);
    }

    public override IInitialView CreateInitialView(MainView mainView)
    {
        return new ConsoleInitialView(mainView);
    }

    public override ISearchView CreateSearchView(MainView mainView)
    {
        return new ConsoleSearchView(mainView);
    }

    public override ISearchResultsView CreateSearchResultsView(MainView mainView)
    {
        return new ConsoleSearchResultsView(mainView);
    }
}