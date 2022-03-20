using System.Data;

namespace TaskManager.Views;

public class ConsoleEditTaskView : EditTaskView
{
    public ConsoleEditTaskView(MainView mainView, Task? selectedTask = null) : base(mainView, selectedTask)
    {
    }

    public override void Start()
    {
        if (UpdatedTask == null || SelectedTask == null)
        {
            throw new InvalidOperationException("No task selected!");
        }

        string[] options = CreateOptions();
        ConsoleMenu editTaskMenu = new ConsoleMenu(UpdatedTask.ToString(), options);
        int selectedIndex = editTaskMenu.Run();
        ApplyAction(selectedIndex);
    }

    private void ApplyAction(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                EditTitleOption();
                break;
            case 1:
                EditPriorityOption();
                break;
            case 2:
                ChangeIsDone();
                break;
            case 3:
                EditDescriptionOption();
                break;
            case 4:
                EditDueTimeOption();
                break;
            case 5:
                EditLabelsOption();
                break;
            case 6:
                EditSubTasks();
                break;
            case 7:
                Start();
                break;
            case 8:
                SaveChangesToTask();
                break;
            case 9:
                MainView.ShowTaskView(SelectedTask!);
                break;
        }
    }

    // private void SetUpdatedTask(Task task)
    // {
    //     _updatedTask ??= new Task(task.Title);
    //     _updatedTask.CopyTaskValues(task);
    // }

    private string[] CreateOptions()
    {
        return new []{ "Title", "Priority", "Is Done", "Description", 
            "Due Time", "Labels", "Sub Tasks", "", "Save Changes", "Cancel" };
    }
    
    private void SaveChangesToTask()
    {
        TestController.Instance.UpdateTask(SelectedTask!, UpdatedTask!);
        MainView.ShowTasksView();
    }
    
    private void EditTitleOption()
    {
        string newTitle;
        try
        {
            newTitle = ConsoleViewUtils.GetTitleFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
            return;
        }

        UpdatedTask!.Title = newTitle;
        Start();
    }
    
    private void EditPriorityOption()
    {
        Task.TaskPriority newPriority;
        try
        {
            newPriority = ConsoleViewUtils.GetPriorityFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
            return;
        }

        UpdatedTask!.Priority = newPriority;
        Start();
    }
    
    private void ChangeIsDone()
    {
        if (SelectedTask!.IsDone)
        {
            UpdatedTask!.IsDone = false;
        }
        else
        {
            UpdatedTask!.IsDone = true;
        }
        Start();
    }
    
    private void EditDescriptionOption()
    {
        string? newDescription = null;
        try
        {
            newDescription = ConsoleViewUtils.GetDescriptionFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
            return;
        }

        UpdatedTask!.Description = newDescription;
        Start();
    }
    
    private void EditDueTimeOption()
    {
        DateTime? newDueTime = null;
        try
        {
            newDueTime = ConsoleViewUtils.GetDueDateFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
            return;
        }

        UpdatedTask!.DueTime = newDueTime;
        Start();
    }
    
    private void EditLabelsOption()
    {
        Console.WriteLine("Do you want to add a label or remove one (a / r)?");
        string? addOrRemove = Console.ReadLine();
        if (addOrRemove == null)
        {
            Console.WriteLine("Invalid option!");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
            return;
        }

        if (addOrRemove.Equals("a"))
        {
            Console.WriteLine("Enter a label to add:");
            string? label = Console.ReadLine();
            if (label == null || label.Equals(""))
            {
                Console.WriteLine("Invalid label!");
                Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
                Start();
            }
            else
            {
                UpdatedTask!.AddLabel(label);
                Start();
            }
        }
        else if (addOrRemove.Equals("r"))
        {
            Console.WriteLine("Enter a label to remove:");
            string? label = Console.ReadLine();
            if (label == null || !UpdatedTask!.IsContainLabel(label))
            {
                Console.WriteLine("Invalid label!");
                Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
                Start();
            }
            else
            {
                UpdatedTask!.RemoveLabel(label);
                Start();
            }
        }
        else
        {
            Console.WriteLine("Invalid option!");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
        }
    }
    
    private void EditSubTasks()
    {
        Console.WriteLine("Do you want to add a sub task or remove one? (a / r):");
        string res = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid answer to question!");
        if (res.Equals("a"))
        {
            try
            {
                UpdatedTask!.AddSubTask(ConsoleViewUtils.CreateTask());
                Start();
            }
            catch (InvalidExpressionException e)
            {
                Console.WriteLine($"Error! {e.Message}");
                Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
                Start();
            }
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
            foreach (Task subTask in UpdatedTask!.SubTasks)
            {
                if (subTaskTitle != subTask.Title)
                {
                    continue;
                }

                taskToRemove = subTask;
                UpdatedTask!.RemoveSubTask(taskToRemove);
                Start();
                return;
            }

            Console.WriteLine("Sub Task with same title not found!");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            Start();
        }
        else
        {
            throw new InvalidExpressionException("Invalid! Please enter (y / n).");
        }
    }
}