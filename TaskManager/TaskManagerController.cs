using TaskManager.Models;
using TaskManager.Searchers;
using TaskManager.TaskCollections;
using TaskManager.Views;

namespace TaskManager;

public class TaskManagerController
{
    private static readonly TaskManagerController Instance = new TaskManagerController();
    private readonly Model _model;
    private readonly MainView _mainView;
    private readonly SearcherFactory _searcherFactory;

    private TaskManagerController()
    {
        _model = new Model();
        ViewFactory viewFactory = new ConsoleViewFactory();
        _mainView = viewFactory.CreateMainView();
        _searcherFactory = new SearcherFactory();
    }

    public ITaskCollection GetTasks()
    {
        try
        {
            // todo return _model.GetTasks();
            return new ListTaskCollection();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not get tasks. {e.Message}");
        }
    }

    public void AddTask(Task task)
    {
        try
        {
            _model.InsertTaskM(task);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not insert task. {e.Message}");
        }
    }

    public void DeleteTask(Task task)
    {
        try
        {
            _model.DeleteTaskFromDbM(task);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not delete task. {e.Message}");
        }
    }

    public void UpdateTask(Task oldTask, Task newTask)
    {
        try
        {
            _model.UpdateTaskInDbM(oldTask, newTask);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not update task. {e.Message}");
        }
    }

    public ITaskCollection SearchTasks(string searcherType, object param)
    {
        try
        {
            ITaskSearcher searcher = _searcherFactory.CreateSearcher(searcherType);
            return _model.SearchTasks(searcher, param);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not search for tasks. {e.Message}");
        }
    }
}