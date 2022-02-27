namespace TaskManager;

public class TaskManagerConsoleView : TaskManagerView
{
    // private string ADD_TASK_SHORTCUT = "a";
    // private string EDIT_TASK_SHORTCUT = "e";
    // private string DELETE_TASK_SHORTCUT = "d";
    // private string VIEW_TASK_SHORTCUT = "v";
    public TaskManagerConsoleView(TestController controller) : base(controller) {}

    public override void StartMainMenu()
    {
        ConsoleMenu mainMenu = CreateMainMenu();
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {   
            case 0:
                ShowTasksOption();
                break;
            case 1:
                ExitMenu();
                break;
        }
    }

    private ConsoleMenu CreateMainMenu()
    {
        string mainMenuPrompt = "Welcome to Task Manager!";
        string[] options = { "Show My Tasks", "Add Task", "Exit" };
        return new ConsoleMenu(mainMenuPrompt, options);
    }

    private ConsoleMenu CreateTasksMenu()
    {
        string tasksMenuPrompt = "Here are your tasks!";

        ITaskCollection taskCollection = Controller.GetTasks();
        string[] options = new string[taskCollection.Count + 1];
        int count = 0;
        foreach (Task task in taskCollection)
        {
            options[count++] = task.Title;
        }

        options[count] = "Back to Main Menu";

        return new ConsoleMenu(tasksMenuPrompt, options);
    }

    private void ShowTasksOption()
    {
        ConsoleMenu tasksMenu = CreateTasksMenu();
        int selectedIndex = tasksMenu.Run();

        if (selectedIndex == tasksMenu.GetNumOfOptions() - 1)
        {
            StartMainMenu();
        }
        else
        {
            Console.Clear();
            ITaskCollection taskCollection = Controller.GetTasks();
            Console.WriteLine(taskCollection.GetTaskAtIndex(selectedIndex));
        }
    }

    // private void PrintTasks()
    // {
    //     ITaskCollection taskCollection = base.Controller.GetTasks();
    //     int count = 1;
    //     foreach (Task task in taskCollection)
    //     {
    //         Console.WriteLine(count + ". " + task.Title);
    //         count++;
    //     }
    // }

    

    private void ExitMenu()
    {
        Console.WriteLine("Quitting...");
        Environment.Exit(0);
    }
}