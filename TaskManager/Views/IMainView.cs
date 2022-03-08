namespace TaskManager.Views;

public interface IMainView : IView
{
    public void ShowInitialView();

    public void ShowTasksView();

    public void ShowTaskView(Task task);

    public void ShowEditTaskView(Task task);
}