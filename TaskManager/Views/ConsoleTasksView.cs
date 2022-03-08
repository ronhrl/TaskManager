using System.Data;

namespace TaskManager.Views;

public class ConsoleTasksView : ConsoleView, ITasksView
{
    // private static readonly int SHOW_TASK_OPTION = 0;

    private static readonly string IS_DONE_SYMBOL = "*";
    private static readonly string PROMPT = "Take a look at your tasks!";
    
    private readonly TestController _controller;
    private ITaskCollection _taskCollection;
    private readonly IMainView _mainView;
    // private readonly IView _callerView;
    // private ITaskView _taskView;
    private string[] _options;

    public ConsoleTasksView(IMainView mainView, TestController controller)
    {
        _controller = controller;
        _mainView = mainView;
        _taskCollection = _controller.GetTasks();
        // _callerView = callerView;
        // _taskView = taskView;
        _options = CreateOptions();
    }

    public override void Start()
    {
        _taskCollection = _controller.GetTasks();
        _options = CreateOptions();
        ConsoleMenu tasksMenu = new ConsoleMenu(PROMPT, _options);
        int selectedIndex = tasksMenu.Run();
        ApplyAction(selectedIndex, tasksMenu);
    }

    private void ApplyAction(int selectedIndex, ConsoleMenu menu)
    {
        int backOption = menu.GetNumOfOptions() - 1;
        int addTaskOption = menu.GetNumOfOptions() - 2;
        int dummyOption = menu.GetNumOfOptions() - 3;
        
        if (selectedIndex == backOption)
        {
            _mainView.ShowInitialView();
        }
        else if (selectedIndex == addTaskOption)
        {
            AddTaskOption();
        }
        else if (selectedIndex == dummyOption)
        {
            Start();
        }
        else
        {
            ShowTaskOption(selectedIndex);
        }
    }

    private void ShowTaskOption(int selectedIndex)
    {
        Task selectedTask = _taskCollection.GetTaskAtIndex(selectedIndex);
        try
        {
            _mainView.ShowTaskView(selectedTask);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
        }
    }

    private void AddTaskOption()
    {
        try
        {
            _controller.AddTask(CreateTask());
            Start();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
        }
    }

    private string[] CreateOptions()
    {
        string[] options = new string[_taskCollection.Count + 3];
        int count = 0;
        foreach (Task task in _taskCollection)
        {
            if (task.IsDone)
            {
                options[count++] = $"[{IS_DONE_SYMBOL}] " + task.Title;
            }
            else
            {
                options[count++] = "[ ] " + task.Title;
            }
        }
        options[count++] = "";
        options[count++] = "Add Task";
        options[count] = "Back";

        return options;
    }
}