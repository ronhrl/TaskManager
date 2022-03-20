using System.IO.Compression;

namespace TaskManager.Models;

public class Model
{
    private TaskManagerFilesStorageM mydatabase;
    //List<Task>
    private ITaskCollection _taskCollection;
    public Model()
    {
        mydatabase = new TaskManagerFilesStorageM();
        _taskCollection = new ListTaskCollection();
    }

    public List<Task> SortTask(ITaskCollection collection, object param)
    {
        //ITaskSorter s;
        if (param.ToString().Equals("priority"))
        {
            TaskPrioritySorterM s = new TaskPrioritySorterM(); 
            return s.Sort(collection); 
        }
        
        else if (param.ToString().Equals("dueTime"))
        {
            DueTimeSorterM s = new DueTimeSorterM(); 
            return s.Sort(collection); 
        }
        
        else if (param.ToString().Equals("creationTime"))
        {
            CreationTimeSorterM s = new CreationTimeSorterM(); 
            return s.Sort(collection); 
        }
        throw new NotImplementedException();
        //return new List<Task>;
    }

    public List<Task> SearchTasks(ITaskCollection collection, object param, object p)
    {
        //ITaskSorter s;
        if (param.ToString().Equals("label"))
        {
            TaskLabelSearcher s = new TaskLabelSearcher();
            return s.Search(collection, p);
        }

        else if (param.ToString().Equals("title"))
        {
            TaskTitleSearcher s = new TaskTitleSearcher();
            return s.Search(collection, p);
        }
        throw new NotImplementedException();
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
            Console.WriteLine(e);
            throw;
        }
        
    }

    public void DeleteTaskFromDbM(Task t)
    {
        mydatabase.DeleteTaskFromDb(t);
        _taskCollection.Remove(t);
    }

    public void UpdateTaskInDbM(Task oldT, Task newT)
    {
        mydatabase.UpdateTaskInDb(newT);
        _taskCollection.Update(oldT, newT);
    }
    
    public void InsertSubTaskM(Task t, Task sub)
    {
        mydatabase.InsertNewSubTask(t, sub);
        
    }

    public void DeleteSubTaskFromDbM(Task t)
    {
        mydatabase.DeleteSubTaskFromDb(t);
    }

    public void UpdateSubTaskInDbM(Task t)
    {
        mydatabase.UpdateSubTaskInDb(t);
    }

    public List<Task> GetTasksFromDbM()
    {
        return mydatabase.GetTasksFromDb();
    }

    public static void Main(string[] args)
    {
        Task t = new Task("ppppppppppppppp", Task.TaskPriority.High, "fffffff", null, null, null);
        Task t2 = new Task("aaaa", Task.TaskPriority.High, "bbb", null, null, null);
        Task t3 = new Task("yit", Task.TaskPriority.High, "cccc", null, null, null);
        Task t4 = new Task("ron", Task.TaskPriority.Medium, "ddddd", null, null, null);
        //TaskManagerFilesStorageM tmfs = new TaskManagerFilesStorageM();
        Model m = new Model();
        //TaskManagerFilesStorageM tm = GetMyDataBase();
        m.InsertTaskM(t);
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