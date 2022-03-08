namespace TaskManager.Views;

public interface IEditTaskView : IView
{
    public void SetSelectedTask(Task task);
}