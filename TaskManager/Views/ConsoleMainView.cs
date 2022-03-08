namespace TaskManager.Views;

public class ConsoleMainView : ConsoleView, IMainView
{
    private IInitialView _initialView;
    private ITasksView _tasksView;
    private ITaskView _taskView;
    private IEditTaskView _editTaskView;

    public ConsoleMainView()
    {
        _initialView = CreateInitialView();
        _tasksView = CreateTasksView();
        _taskView = CreateTaskView();
        _editTaskView = CreateEditTaskView();
    }

    public override void Start()
    {
        ShowInitialView();
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

    private IInitialView CreateInitialView()
    {
        return new ConsoleInitialView(this, TestController.Instance);
    }

    private ITasksView CreateTasksView()
    {
        return new ConsoleTasksView(this, TestController.Instance);
    }

    private ITaskView CreateTaskView()
    {
        return new ConsoleTaskView(this, TestController.Instance);
    }

    private IEditTaskView CreateEditTaskView()
    {
        return new ConsoleEditTaskView(this, TestController.Instance);
    }
}