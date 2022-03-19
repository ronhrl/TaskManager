using System.Data;

namespace TaskManager.Views;

public class ConsoleEditTaskView : ConsoleView, IEditTaskView
{
    // private readonly IView _callerView;
    private readonly MainView _mainView;
    private readonly string[] _options;
    private Task? _selectedTask;
    private Task? _updatedTask;

    public ConsoleEditTaskView(MainView mainView, Task? selectedTask = null)
    {
        // _callerView = callerView;
        _mainView = mainView;
        _selectedTask = selectedTask;
        if (_selectedTask != null)
        {
            _updatedTask = new Task(_selectedTask.Title);
            _updatedTask.CopyTaskValues(_selectedTask);
        }
        else
        {
            _updatedTask = null;
        }

        _options = CreateOptions();
    }

    public override void Start()
    {
        if (_updatedTask == null || _selectedTask == null)
        {
            throw new InvalidOperationException("No task selected!");
        }
        ConsoleMenu editTaskMenu = new ConsoleMenu(_updatedTask.ToString(), _options);
        int selectedIndex = editTaskMenu.Run();
        ApplyAction(selectedIndex);
    }

    public void SetSelectedTask(Task task)
    {
        _selectedTask = task;
        SetUpdatedTask(_selectedTask);
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
                _mainView.ShowTaskView(_selectedTask!);
                break;
        }
    }

    private void SetUpdatedTask(Task task)
    {
        _updatedTask ??= new Task(task.Title);
        _updatedTask.CopyTaskValues(task);
    }

    private string[] CreateOptions()
    {
        return new []{ "Title", "Priority", "Is Done", "Description", 
            "Due Time", "Labels", "Sub Tasks", "", "Save Changes", "Cancel" };
    }
    
    private void SaveChangesToTask()
    {
        TestController.Instance.UpdateTask(_selectedTask!, _updatedTask!);
        _mainView.ShowTasksView();
    }
    
    private void EditTitleOption()
    {
        string newTitle;
        try
        {
            newTitle = GetTitleFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
            return;
        }

        _updatedTask!.Title = newTitle;
        Start();
    }
    
    private void EditPriorityOption()
    {
        Task.TaskPriority newPriority;
        try
        {
            newPriority = GetPriorityFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
            return;
        }

        _updatedTask!.Priority = newPriority;
        Start();
    }
    
    private void ChangeIsDone()
    {
        if (_selectedTask!.IsDone)
        {
            _updatedTask!.IsDone = false;
        }
        else
        {
            _updatedTask!.IsDone = true;
        }
        Start();
    }
    
    private void EditDescriptionOption()
    {
        string? newDescription = null;
        try
        {
            newDescription = GetDescriptionFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
            return;
        }

        _updatedTask!.Description = newDescription;
        Start();
    }
    
    private void EditDueTimeOption()
    {
        DateTime? newDueTime = null;
        try
        {
            newDueTime = GetDueDateFromUser();
        }
        catch (InvalidExpressionException e)
        {
            Console.WriteLine($"Error! {e.Message}");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
            return;
        }

        _updatedTask!.DueTime = newDueTime;
        Start();
    }
    
    private void EditLabelsOption()
    {
        Console.WriteLine("Do you want to add a label or remove one (a / r)?");
        string? addOrRemove = Console.ReadLine();
        if (addOrRemove == null)
        {
            Console.WriteLine("Invalid option!");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
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
                Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
                Start();
            }
            else
            {
                _updatedTask!.AddLabel(label);
                Start();
            }
        }
        else if (addOrRemove.Equals("r"))
        {
            Console.WriteLine("Enter a label to remove:");
            string? label = Console.ReadLine();
            if (label == null || !_updatedTask!.IsContainLabel(label))
            {
                Console.WriteLine("Invalid label!");
                Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
                Start();
            }
            else
            {
                _updatedTask!.RemoveLabel(label);
                Start();
            }
        }
        else
        {
            Console.WriteLine("Invalid option!");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
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
                _updatedTask!.AddSubTask(CreateTask());
                Start();
            }
            catch (InvalidExpressionException e)
            {
                Console.WriteLine($"Error! {e.Message}");
                Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
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
            foreach (Task subTask in _updatedTask!.SubTasks)
            {
                if (subTaskTitle != subTask.Title)
                {
                    continue;
                }

                taskToRemove = subTask;
                _updatedTask!.RemoveSubTask(taskToRemove);
                Start();
                return;
            }

            Console.WriteLine("Sub Task with same title not found!");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            Start();
        }
        else
        {
            throw new InvalidExpressionException("Invalid! Please enter (y / n).");
        }
    }
}