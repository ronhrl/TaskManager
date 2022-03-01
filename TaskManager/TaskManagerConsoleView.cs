using System.Data;
using System.Text.RegularExpressions;

namespace TaskManager;

public class TaskManagerConsoleView : TaskManagerView
{
    private const int ErrorMessageWaitTime = 2000;
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
        string[] options = { "Edit Task", "Delete Task", "Back to all Tasks" };
        return new ConsoleMenu(taskPrompt, options);
    }

    private ConsoleMenu CreateEditTaskMenu(Task task)
    {
        string editTaskPrompt = task + "\nChoose a property to change:";
        string[] options =
            { "Title", "Priority", "Is Done", "Description", 
                "Due Time", "Labels", "Sub Tasks", "", "Save Changes", "Cancel" };
        return new ConsoleMenu(editTaskPrompt, options);
    }

    private void ShowTaskOption(Task task)
    {
        ConsoleMenu taskMenu = CreateTaskMenu(task);
        int selectedIndex = taskMenu.Run();
        
        switch (selectedIndex)
        {
            case 0:
                EditTaskOption(task);
                break;
            case 1:
                DeleteTask(task);
                break;
            case 2:
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

    private void EditTaskOption(Task task)
    {
        ConsoleMenu editTaskMenu = CreateEditTaskMenu(task);
        int selectedIndex = editTaskMenu.Run();

        if (selectedIndex == editTaskMenu.GetNumOfOptions() - 1)
        {
            ShowTaskOption(task);
        }
        else
        {
            Task updatedTask = new Task(task.Title);
            updatedTask.CopyTaskValues(task);

            switch (selectedIndex)
            {
                case 0:
                    EditTitleOption(updatedTask);
                    break;
                case 1:
                    EditPriorityOption(updatedTask);
                    break;
                case 2:
                    ChangeIsDone(task, updatedTask);
                    break;
                case 3:
                    EditDescriptionOption(updatedTask);
                    break;
                case 4:
                    EditDueTimeOption(updatedTask);
                    break;
                case 5:
                    EditLabelsOption(updatedTask);
                    break;
                case 6:
                    EditSubTasks(updatedTask);
                    break;
                case 7:
                    EditTaskOption(updatedTask);
                    break;
                case 8:
                    SaveChangesToTask(task, updatedTask);
                    break;
            }
        }
    }

    private void SaveChangesToTask(Task oldTask, Task newTask)
    {
        Controller.UpdateTask(oldTask, newTask);
        ShowTasksOption();
    }

    private void EditSubTasks(Task updatedTask)
    {
        Console.WriteLine("Do you want to add a sub task or remove one? (a / r):");
        string res = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid answer to question!");
        if (res.Equals("a"))
        {
            updatedTask.AddSubTask(CreateTask());
            EditTaskOption(updatedTask);
        }
        else if (res.Equals("r"))
        {
            Console.WriteLine("Enter title of sub task to remove:");
            string? subTaskTitle = Console.ReadLine();
            if (subTaskTitle == null)
            {
                throw new InvalidExpressionException("Invalid title!");
            }

            Task taskToRemove;
            foreach (Task subTask in updatedTask.SubTasks)
            {
                if (subTaskTitle != subTask.Title)
                {
                    continue;
                }

                taskToRemove = subTask;
                updatedTask.RemoveSubTask(taskToRemove);
                EditTaskOption(updatedTask);
            }

            Console.WriteLine("Sub Task with same title not found!");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
        }
        else
        {
            throw new InvalidExpressionException("Invalid! Please enter (y / n).");
        }
    }

    private Task CreateTask()
    {
        var title = GetTitleFromUser();
        var priority = GetPriorityFromUser();
        var description = GetDescriptionFromUser();
        var dueDate = GetDueDateFromUser();
        var labels = GetLabelsFromUser();
        var subTasks = GetSubTasksFromUser();
        return new Task(title, priority, description, dueDate, labels, subTasks);
        }

    private List<Task> GetSubTasksFromUser()
    {
        bool addSubTask = true;
        List<Task> subTasks = new List<Task>();
        while (addSubTask)
        {
            Console.WriteLine("Do you want to add another sub task? (y / n):");
            string res = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid answer to question!");
            if (res.Equals("y"))
            {
                subTasks.Add(CreateTask());
            }
            else if (res.Equals("n"))
            {
                addSubTask = false;
            }
            else
            {
                throw new InvalidExpressionException("Invalid! Please enter (y / n).");
            }
        }

        return subTasks;
    }

    private static List<string> GetLabelsFromUser()
    {
        Console.WriteLine("Please enter labels (format - <label1> <label2> <label3>. leave empty if wanted");
        string labelsString = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid labels!");
        List<string> labels = new List<string>();
        if (!labelsString.Equals(""))
        {
            string[] labelsArr = labelsString.Split(" ");
            foreach (string label in labelsArr)
            {
                labels.Add(label);
            }
        }

        return labels;
    }

    private static DateTime? GetDueDateFromUser()
    {
        Console.WriteLine("Please enter a due date (format - dd/mm/yyyy. leave empty if wanted):");
        string dueDateString = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid due date!");
        DateTime? dueDate = null;
        if (!dueDateString.Equals(""))
        {
            Regex dateRegex = new Regex(
                "(^(((0[1-9]|1[0-9]|2[0-8])[/](0[1-9]|1[012]))|((29|30|31)[/](0[13578]|1[02]))|((29|30)[/](0[4,6,9]|11)))[/](19|[2-9][0-9])[0-9][0-9]$)|(^29[/]02[/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)");
            if (!dateRegex.IsMatch(dueDateString))
            {
                throw new InvalidExpressionException("Invalid due time!");
            }

            string[] dateParts = dueDateString.Split("/");
            dueDate = new DateTime(
                Int32.Parse(dateParts[2]), Int32.Parse(dateParts[1]), Int32.Parse(dateParts[0]));
        }

        return dueDate;
    }

    private static string? GetDescriptionFromUser()
    {
        Console.WriteLine("Please enter a description (leave empty if wanted):");
        string? description = Console.ReadLine();
        if (description != null && description.Equals(""))
        {
            description = null;
        }

        return description;
    }

    private static Task.TaskPriority GetPriorityFromUser()
    {
        Console.WriteLine("Please enter a priority (format - L / M / H):");
        string priorityChar = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid priority!");
        Task.TaskPriority priority = Task.TaskPriority.Medium;
        if (priorityChar.Equals("L"))
        {
            priority = Task.TaskPriority.Low;
        }
        else if (priorityChar.Equals("M"))
        {
            priority = Task.TaskPriority.Medium;
        }
        else if (priorityChar.Equals("H"))
        {
            priority = Task.TaskPriority.High;
        }
        else
        {
            throw new InvalidExpressionException("Invalid priority!");
        }

        return priority;
    }

    private static string GetTitleFromUser()
    {
        Console.WriteLine("Please enter a title:");
        string title = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid title!");
        if (title.Equals(""))
        {
            throw new InvalidExpressionException("Invalid title!");
        }

        return title;
    }

    private void EditLabelsOption(Task updatedTask)
    {
        Console.WriteLine("Do you want to add a label or remove one (a / r)?");
        string? addOrRemove = Console.ReadLine();
        if (addOrRemove == null)
        {
            Console.WriteLine("Invalid option!");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
        }

        if (addOrRemove.Equals("a"))
        {
            Console.WriteLine("Enter a label to add:");
            string? label = Console.ReadLine();
            if (label == null || label.Equals(""))
            {
                Console.WriteLine("Invalid label!");
                Thread.Sleep(ErrorMessageWaitTime);
                EditTaskOption(updatedTask);
            }
            else
            {
                updatedTask.AddLabel(label);
                EditTaskOption(updatedTask);
            }
        }
        else if (addOrRemove.Equals("r"))
        {
            Console.WriteLine("Enter a label to remove:");
            string? label = Console.ReadLine();
            if (label == null || !updatedTask.IsContainLabel(label))
            {
                Console.WriteLine("Invalid label!");
                Thread.Sleep(ErrorMessageWaitTime);
                EditTaskOption(updatedTask);
            }
            else
            {
                updatedTask.RemoveLabel(label);
                EditTaskOption(updatedTask);
            }
        }
        else
        {
            Console.WriteLine("Invalid option!");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
        }
    }

    private void EditDueTimeOption(Task updatedTask)
    {
        DateTime? newDueTime = null;
        try
        {
            newDueTime = GetDueDateFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
        }

        updatedTask.DueTime = newDueTime;
        EditTaskOption(updatedTask);
    }

    private void EditDescriptionOption(Task updatedTask)
    {
        string? newDescription = null;
        try
        {
            newDescription = GetDescriptionFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
        }

        updatedTask.Description = newDescription;
        Console.Clear();
        EditTaskOption(updatedTask);
    }

    private void ChangeIsDone(Task task, Task updatedTask)
    {
        if (task.IsDone)
        {
            updatedTask.IsDone = false;
        }
        else
        {
            updatedTask.IsDone = true;
        }

        Console.Clear();
        EditTaskOption(updatedTask);
    }

    private void EditPriorityOption(Task updatedTask)
    {
        Task.TaskPriority newPriority;
        try
        {
            newPriority = GetPriorityFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
            return;
        }

        updatedTask.Priority = newPriority;
        Console.Clear();
        EditTaskOption(updatedTask);
    }

    private void EditTitleOption(Task updatedTask)
    {
        string newTitle;
        try
        {
            newTitle = GetTitleFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ErrorMessageWaitTime);
            EditTaskOption(updatedTask);
            return;
        }

        updatedTask.Title = newTitle;
        Console.Clear();
        EditTaskOption(updatedTask);
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