using System.IO.Compression;
using TaskManager.Searchers;
using TaskManager.Sorters;
using TaskManager.TaskCollections;
//using TaskManager.ListTaskCollection;

namespace TaskManager.Models;

public class Model
{
    private SqliteStorage mydatabase;
    //List<Task>
    private readonly ITaskCollection _taskCollection;
    // private readonly SearcherFactory _searcherFactory;
    // private readonly SorterFactory _sorterFactory;
    public Model()
    {
        mydatabase = new SqliteStorage();
        _taskCollection = new ListTaskCollection();
        // _searcherFactory = new SearcherFactory();
        // _sorterFactory = new SorterFactory();
    }
    
    public ITaskCollection SearchTasks(ITaskSearcher searcherType, object param)
    {
        try
        {
            //ITaskSearcher searcher = _searcherFactory.CreateSearcher(searcherType);
            return searcherType.Search(_taskCollection, param);
            //searcher.Search()
            //return _model.SearchTasks(searcher, param);
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
            //ITaskSorter sorter = _sorterFactory.CreateSorter(sorterType);
            return sorterType.Sort(_taskCollection);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not search for tasks. {e.Message}");
        }
    }
    //public ITaskCollection so
    // public List<Task> SortTask(ITaskCollection collection, object param)
    // {
    //     //ITaskSorter s;
    //     if (param.ToString().Equals("priority"))
    //     {
    //         TaskPrioritySorterM s = new TaskPrioritySorterM(); 
    //         return s.Sort(collection); 
    //     }
    //     
    //     else if (param.ToString().Equals("dueTime"))
    //     {
    //         DueTimeSorterM s = new DueTimeSorterM(); 
    //         return s.Sort(collection); 
    //     }
    //     
    //     else if (param.ToString().Equals("creationTime"))
    //     {
    //         CreationTimeSorterM s = new CreationTimeSorterM(); 
    //         return s.Sort(collection); 
    //     }
    //     throw new NotImplementedException();
    //     //return new List<Task>;
    // }

    // public List<Task> SearchTasks(ITaskCollection collection, object param, object p)
    // {
    //     //ITaskSorter s;
    //     if (param.ToString().Equals("label"))
    //     {
    //         TaskLabelSearcher s = new TaskLabelSearcher();
    //         return s.Search(collection, p);
    //     }
    //
    //     else if (param.ToString().Equals("title"))
    //     {
    //         TaskTitleSearcher s = new TaskTitleSearcher();
    //         return s.Search(collection, p);
    //     }
    //     throw new NotImplementedException();
    // }

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
            _taskCollection.Update(oldT, newT);
            mydatabase.UpdateTaskInDb(oldT, newT);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error! Could not update this task. {e.Message}");
        }
        
    }
    
    public void InsertSubTaskM(Task t, Task sub)
    {
        //_taskCollection.AddSubTask(t, sub);
        mydatabase.InsertNewSubTask(t, sub);
    }

    public void DeleteSubTaskFromDbM(Task t)
    {
        
        mydatabase.DeleteSubTaskFromDb(t);
    }

    public void UpdateSubTaskInDbM(Task oldTask, Task newTask)
    {
        
        mydatabase.UpdateSubTaskInDb(oldTask, newTask);
    }

    public ITaskCollection GetTasksFromDbM()
    {
        return mydatabase.GetTasksFromDb();
    }

    public static void Main(string[] args)
    {
        Task t = new Task("llllllll", Task.TaskPriority.High, "fffffff", DateTime.Today, null, null);
        Task t2 = new Task("aaaa", Task.TaskPriority.High, "bbb", DateTime.Today, null, null);
        Task t3 = new Task("yit", Task.TaskPriority.High, "cccc", DateTime.Today, null, null);
        Task t4 = new Task("ron", Task.TaskPriority.Medium, "ddddd", DateTime.Today, null, null);
        //TaskManagerFilesStorageM tmfs = new TaskManagerFilesStorageM();
        List<Task> list = new List<Task>();
        list.Add(t);
        list.Add(t2);
        list.Add(t3);
        list.Add(t4);
        Model m = new Model();
        List<string> labels = new List<string>();
        labels.Add("A");
        labels.Add("B");
        labels.Add("C");
        Task t8 = new Task("wwwww", Task.TaskPriority.High, "fffffff", DateTime.Today, labels, list);
        //TaskManagerFilesStorageM tm = GetMyDataBase();
        m.InsertTaskM(t8);
        ITaskCollection ls = new ListTaskCollection();
        ls = m.GetTasksFromDbM();
    
        foreach (Task t6 in ls)
        {
            Console.WriteLine(t6);
        }
        // m.InsertTaskM(t2);
        // m.InsertSubTaskM(t, t3);
        // List<Task> l = m.GetTasksFromDbM();
        // foreach (Task task in l)
        // {
        //     Console.WriteLine(task);
        // }
        //m.DeleteTaskFromDbM(t);
    }
}