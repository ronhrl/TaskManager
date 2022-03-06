namespace TaskManager.Views;

public class ConsoleTaskView : ConsoleView, ITaskView
{
    // private static readonly int EDIT_TASK_OPTION = 0;
    // private static readonly int DELETE_TASK_OPTION = 1;
    // private static readonly int BACK_OPTION = 2;
    
    private readonly TestController _controller;
    private Task? _selectedTask;
    private IView _callerView;
    private string[] _options;

    public ConsoleTaskView(TestController controller, IView callerView, Task? selectedTask = null)
    {
        _controller = controller;
        _selectedTask = selectedTask;
        _callerView = callerView;
        _options = CreateOptions();
    }

    public override void Start()
    {
        if (_selectedTask == null)
        {
            throw new InvalidOperationException("No task selected!");
        }
        ConsoleMenu taskMenu = new ConsoleMenu(_selectedTask.ToString(), _options);
        int selectedIndex = taskMenu.Run();
        ApplyAction(selectedIndex, taskMenu);
    }

    public void SetSelectedTask(Task task)
    {
        _selectedTask = task;
    }

    private void ApplyAction(int selectedIndex, ConsoleMenu menu)
    {
        switch (selectedIndex)
        {
            case 0:
                // todo edit task view
                break;
            case 1:
                DeleteTask(_selectedTask);
                _callerView.Start();
                break;
            case 2:
                _callerView.Start();
                break;
        }
    }

    private string[] CreateOptions()
    {
        return new []{ "Edit Task", "Delete Task", "Back to all Tasks" };
    }

    private void DeleteTask(Task? task)
    {
        if (task == null)
        {
            Console.WriteLine("Error! No task selected!");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
        }
        try
        {
            _controller.DeleteTask(task);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Error! Could not delete task: {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
        }
    }
}