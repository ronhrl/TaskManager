using System.Data;

namespace TaskManager.Views;

public class ConsoleTasksView : ConsoleView, ITasksView
{
    private static readonly int SHOW_TASK_OPTION = 0;

    private static readonly string IS_DONE_SYMBOL = "*";
    private static readonly string PROMPT = "Take a look at your tasks!";
    
    private readonly TestController _controller;
    private ITaskCollection _taskCollection;
    private IView _callerView;
    private string[] _options;

    public ConsoleTasksView(TestController controller, IView callerView)
    {
        _controller = controller;
        _taskCollection = _controller.GetTasks();
        _callerView = callerView;
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
        int backToAllTasksOption = menu.GetNumOfOptions() - 1;
        int addTaskOption = menu.GetNumOfOptions() - 2;
        int dummyOption = menu.GetNumOfOptions() - 3;
        
        if (selectedIndex == backToAllTasksOption)
        {
            _callerView.Start();
        }
        else if (selectedIndex == addTaskOption)
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
        else if (selectedIndex == dummyOption)
        {
            Start();
        }
        else
        {
            Task selectedTask = _taskCollection.GetTaskAtIndex(selectedIndex);
            // TODO show task
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