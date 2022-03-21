using System.Data;

namespace TaskManager.Views;

public class ConsoleTasksView : TasksView
{
    private static readonly string IS_DONE_SYMBOL = "*";
    private static readonly string PROMPT = "Take a look at your tasks!";

    public ConsoleTasksView(MainView mainView) : base(mainView)
    {
    }
    public override void Start()
    {
        TaskCollection = TaskManagerController.Instance.GetTasks();
        string[] options = CreateOptions();
        ConsoleMenu tasksMenu = new ConsoleMenu(PROMPT, options);
        int selectedIndex = tasksMenu.Run();
        ApplyAction(selectedIndex, tasksMenu);
    }

    private void ApplyAction(int selectedIndex, ConsoleMenu menu)
    {
        int backOption = menu.GetNumOfOptions() - 1;
        int searchOption = menu.GetNumOfOptions() - 2;
        int addTaskOption = menu.GetNumOfOptions() - 3;
        int dummyOption = menu.GetNumOfOptions() - 4;
        
        if (selectedIndex == backOption)
        {
            MainView.ShowInitialView();
        }
        else if (selectedIndex == addTaskOption)
        {
            AddTaskOption();
        }
        else if (selectedIndex == searchOption)
        {
            SearchOption();
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

    private void SearchOption()
    {
        MainView.ShowSearchView();
    }

    private void ShowTaskOption(int selectedIndex)
    {
        Task selectedTask = TaskCollection.GetTaskAtIndex(selectedIndex);
        try
        {
            MainView.ShowTaskView(selectedTask);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
        }
    }

    private void AddTaskOption()
    {
        try
        {
            Task task = ConsoleViewUtils.CreateTask();
            TaskManagerController.Instance.AddTask(task);
            Start();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"{e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
        }
    }

    private string[] CreateOptions()
    {
        string[] options = new string[TaskCollection.Count + 4];
        int count = 0;
        foreach (Task task in TaskCollection)
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
        options[count++] = "Search for tasks";
        options[count] = "Back";

        return options;
    }
}