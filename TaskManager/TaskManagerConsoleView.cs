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
                ExitApp();
                break;
        }
    }

    private ConsoleMenu CreateMainMenu()
    {
        string mainMenuPrompt = "Welcome to Task Manager!";
        string[] options = { "Show My Tasks", "Exit" };
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

    private ConsoleMenu CreateTaskMenu(Task task)
    {
        string taskPrompt = task.ToString();
        string[] options = { "Add Task", "Edit Task", "Delete Task", "Back to all Tasks" };
        return new ConsoleMenu(taskPrompt, options);
    }

    private void ShowTaskOption(Task task)
    {
        ConsoleMenu taskMenu = CreateTaskMenu(task);
        int selectedIndex = taskMenu.Run();
        
        switch (selectedIndex)
        {
            case 0:
                // todo
                break;
            case 1:
                // todo
                break;
            case 2:
                DeleteTask(task);
                break;
            case 3:
                ShowTasksOption();
                break;
        }
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
            ITaskCollection taskCollection = Controller.GetTasks();
            ShowTaskOption(taskCollection.GetTaskAtIndex(selectedIndex));
        }
    }
    
    private void ExitApp()
    {
        Console.WriteLine("Quitting...");
        Environment.Exit(0);
    }

    private void DeleteTask(Task task)
    {
        Controller.DeleteTask(task);
        ShowTasksOption();
    }
}