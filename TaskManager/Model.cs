using System.IO.Compression;
using TaskManager.Searchers;
using TaskManager.Sorters;
using TaskManager.TaskCollections;
//using TaskManager.ListTaskCollection;

namespace TaskManager.Models;

public class Model
{
    private ITaskManagerStorage mydatabase;
    private ITaskCollection _taskCollection;
    
    public Model()
    {
        mydatabase = new SqliteStorage();
        _taskCollection = GetTasksFromDbM();
    }
    
    public ITaskCollection SearchTasks(ITaskSearcher searcherType, object param)
    {
        try
        {
            return searcherType.Search(_taskCollection, param);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not search for tasks. {e.Message}");
        }
    }
    
    public ITaskCollection SortTasks(ITaskSorter sorterType)
    {
        try
        {
            return sorterType.Sort(_taskCollection);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not search for tasks. {e.Message}");
        }
    }

    public void InsertTaskM(Task t)
    {
        try
        {
            _taskCollection.Add(t);
            mydatabase.InsertNewTask(t);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not add this task. {e.Message}");
        }
        
    }

    public void DeleteTaskFromDbM(Task t)
    {
        try
        {
            _taskCollection.Remove(t);
            mydatabase.DeleteTaskFromDb(t);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not delete this task. {e.Message}");
        }
        
    }

    public void UpdateTaskInDbM(Task oldT, Task newT)
    {
        try
        {
            // _taskCollection.Update(oldT, newT);
            mydatabase.UpdateTaskInDb(oldT, newT);
            if (mydatabase.GetLabelsFromDb(oldT.Title).Count == 0)
            {
                foreach (string label in newT.Labels)
                {
                    mydatabase.AddLabel(newT, label);    
                }
            }
            
            _taskCollection = GetTasksFromDbM();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not update this task. {e.Message}");
        }
        
    }
    
    public void InsertSubTaskM(Task t, Task sub)
    {
        mydatabase.InsertNewSubTask(t, sub);
    }

    public void DeleteSubTaskFromDbM(Task t)
    {
        
        mydatabase.DeleteSubTaskFromDb(t);
    }

    public void UpdateSubTaskInDbM(Task oldT, Task newT)
    {
        mydatabase.UpdateSubTaskInDb(oldT, newT);
    }

    public ITaskCollection GetTasksFromDbM()
    {
        return mydatabase.GetTasksFromDb();
    }
}