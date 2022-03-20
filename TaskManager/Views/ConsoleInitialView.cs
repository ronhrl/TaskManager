namespace TaskManager.Views;

public class ConsoleInitialView : InitialView
{
    private static readonly string Prompt = "Welcome to Task Manager!";
    private static readonly string[] Options = new[] { "Show My Tasks", "Exit" };
    // private readonly string[] _options;
    // private readonly MainView _mainView;

    // public ConsoleInitialView(MainView mainView)
    // {
    //     _mainView = mainView;
    //     _options = CreateOptions();
    // }

    public ConsoleInitialView(MainView mainView) : base(mainView)
    {
    }

    public override void Start()
    {
        ConsoleMenu mainMenu = new ConsoleMenu(Prompt, Options);
        int selectedIndex = mainMenu.Run();
        ApplyAction(selectedIndex);
    }

    private void ApplyAction(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                MainView.ShowTasksView();
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

    // private string[] CreateOptions()
    // {
    //     return new[] { "Show My Tasks", "Exit" };
    // }
    
    private void ExitApp()
    {
        Console.WriteLine("Quitting...");
        Environment.Exit(0);
    }
}