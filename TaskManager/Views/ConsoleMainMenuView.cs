namespace TaskManager.Views;

public class ConsoleMainMenuView : ConsoleView, IMainMenuView
{
    private static readonly string _prompt = "Welcome to Task Manager!";
    private readonly string[] _options;
    private readonly TestController _controller;

    public ConsoleMainMenuView(TestController controller)
    {
        _controller = controller;
        _options = CreateOptions();
    }

    public override void Start()
    {
        ConsoleMenu mainMenu = new ConsoleMenu(_prompt, _options);
        int selectedIndex = mainMenu.Run();
        ApplyAction(selectedIndex);
    }

    private void ApplyAction(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                ShowTasks();
                break;
            case 1:
                ExitApp();
                break;
        }
    }

    private void ShowTasks()
    {
        try
        {
            IEditTaskView editTaskView = new ConsoleEditTaskView(_controller, this);
            ITaskView taskView = new ConsoleTaskView(_controller, this, editTaskView);
            ITasksView tasksView = new ConsoleTasksView(_controller, this, taskView);
            tasksView.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Start();
        }
    }

    private string[] CreateOptions()
    {
        return new[] { "Show My Tasks", "Exit" };
    }
    
    private void ExitApp()
    {
        Console.WriteLine("Quitting...");
        Environment.Exit(0);
    }
}