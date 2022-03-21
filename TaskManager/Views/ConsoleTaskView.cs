namespace TaskManager.Views;

public class ConsoleTaskView : TaskView
{
    private static readonly string[] Options = new []{ "Edit Task", "Delete Task", "Back to all Tasks" };
    
    public ConsoleTaskView(MainView mainView, Task? selectedTask = null) : base(mainView, selectedTask)
    {
    }

    public override void Start()
    {
        if (SelectedTask == null)
        {
            throw new InvalidOperationException("No task selected!");
        }

        ConsoleMenu taskMenu = new ConsoleMenu(SelectedTask.ToString(), Options);
        int selectedIndex = taskMenu.Run();
        ApplyAction(selectedIndex);
    }

    private void ApplyAction(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                EditTaskOption();
                break;
            case 1:
                DeleteTask(SelectedTask);
                MainView.ShowTasksView();
                break;
            case 2:
                MainView.ShowTasksView();
                break;
        }
    }

    private void EditTaskOption()
    {
        try
        {
            MainView.ShowEditTaskView(SelectedTask!);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
        }
    }

    private void DeleteTask(Task? task)
    {
        if (task == null)
        {
            Console.WriteLine("Error! No task selected!");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
        }
        try
        {
            TaskManagerController.Instance.DeleteTask(task!);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Error! Could not delete task: {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
        }
    }
}