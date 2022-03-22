namespace TaskManager.Views;

public class ConsoleInitialView : InitialView
{
    private static readonly string Prompt = "Welcome to Task Manager!";
    private static readonly string[] Options = new[] { "Show My Tasks", "Exit" };
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

    private void ExitApp()
    {
        Console.WriteLine("Quitting...");
        Environment.Exit(0);
    }
}