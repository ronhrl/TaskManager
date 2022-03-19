namespace TaskManager.Views;

public class ConsoleInitialView : ConsoleView, IInitialView
{
    private static readonly string _prompt = "Welcome to Task Manager!";
    private readonly string[] _options;
    private readonly MainView _mainView;

    public ConsoleInitialView(MainView mainView)
    {
        _mainView = mainView;
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
                _mainView.ShowTasksView();
                break;
            case 1:
                ExitApp();
                break;
        }
    }

    // private void ShowTasks()
    // {
    //     try
    //     {
    //         IEditTaskView editTaskView = new ConsoleEditTaskView(_controller, this);
    //         ITaskView taskView = new ConsoleTaskView(_controller, this, editTaskView);
    //         ITasksView tasksView = new ConsoleTasksView(_controller, this, taskView);
    //         tasksView.Start();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine($"Error! {e.Message}");
    //         Start();
    //     }
    // }

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