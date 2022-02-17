namespace TaskManager;

public class TaskManagerConsoleView : TaskManagerView
{
    private string ADD_TASK_SHORTCUT = "a";
    private string EDIT_TASK_SHORTCUT = "e";
    private string DELETE_TASK_SHORTCUT = "d";
    private string VIEW_TASK_SHORTCUT = "v";
    public TaskManagerConsoleView(TestController controller) : base(controller) {}

    public override void StartMenu()
    {
        Console.WriteLine("Welcome to Task Manager!\n\n");
        PrintTasks();
    }

    private void PrintTasks()
    {
        ITaskCollection taskCollection = base.Controller.GetTasks();
        int count = 1;
        foreach (Task task in taskCollection)
        {
            Console.WriteLine(count + ". " + task.Title);
            count++;
        }
    }

    private void PrintTask(Task task)
    {
        Console.WriteLine("Title: " + task.Title);
        Console.WriteLine("Creation Time: " + task.CreationTime);
        Console.WriteLine("Is Done: " + task.IsDone);
        Console.WriteLine("Priority: " + task.GetPriorityAsString());

        if (task.Description != null)
        {
            Console.WriteLine("Description: " + task.Description);
        }

        if (task.Labels.Count > 0)
        {
            Console.Write("Labels: ");
            foreach (string label in task.Labels)
            {
                Console.Write("\t" + label);
            }
            Console.WriteLine();
        }

        if (task.DueTime != null)
        {
            Console.WriteLine("Due Time: " + task.DueTime);
        }

        if (task.SubTasks.Count > 0)
        {
            Console.WriteLine("Sub Tasks:");
            int i = 1;
            foreach (Task subTask in task.SubTasks)
            {
                Console.WriteLine(i + ".");
                PrintTask(subTask);
                Console.WriteLine();
            }
        }
    }
}