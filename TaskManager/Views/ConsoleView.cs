using System.Data;
using System.Text.RegularExpressions;

namespace TaskManager.Views;

public abstract class ConsoleView : IView
{
    protected static readonly int ERROR_MESSAGE_WAIT_TIME = 2000;
    
    public abstract void Start();

    protected Task CreateTask()
    {
        var title = GetTitleFromUser();
        var priority = GetPriorityFromUser();
        var description = GetDescriptionFromUser();
        var dueDate = GetDueDateFromUser();
        var labels = GetLabelsFromUser();
        var subTasks = GetSubTasksFromUser();
        return new Task(title, priority, description, dueDate, labels, subTasks);
    }

    protected List<Task> GetSubTasksFromUser()
    {
        bool addSubTask = true;
        List<Task> subTasks = new List<Task>();
        while (addSubTask)
        {
            Console.WriteLine("Do you want to add another sub task? (y / n):");
            string res = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid answer to question!");
            if (res.Equals("y"))
            {
                try
                {
                    subTasks.Add(CreateTask());
                }
                catch (InvalidExpressionException e)
                {
                    Console.WriteLine($"Error! {e.Message}. Sub Task was not added.");
                }
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

    protected static List<string> GetLabelsFromUser()
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

    protected static DateTime? GetDueDateFromUser()
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

    protected static string? GetDescriptionFromUser()
    {
        Console.WriteLine("Please enter a description (leave empty if wanted):");
        string? description = Console.ReadLine();
        if (description != null && description.Equals(""))
        {
            description = null;
        }

        return description;
    }

    protected static Task.TaskPriority GetPriorityFromUser()
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

    protected static string GetTitleFromUser()
    {
        Console.WriteLine("Please enter a title:");
        string title = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid title!");
        if (title.Equals(""))
        {
            throw new InvalidExpressionException("Invalid title!");
        }

        return title;
    }
}