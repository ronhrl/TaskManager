using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
namespace TaskManager.Models;


public class TaskManagerFilesStorageM : ITaskManagerFilesStorage
{
    private SQLiteConnection mySQLiteConnection;
    // private TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
    // private TaskManagerFilesStorageM()
    // {
    //     mySQLiteConnection = new SQLiteConnection("Data Source=FilesStorage.sqlite3");
    //     if (!File.Exists("./FilesStorage.sqlite3"))
    //     {
    //     
    //         SQLiteConnection.CreateFile("FilesStorage.sqlite3");
    //         Console.WriteLine("FilesStorage Database file created");
    //     }
    // }
    // public static TaskManagerFilesStorageM GetMyDataBase()
    // {
    //     if (myDatabase == null)
    //     {
    //         myDatabase = new TaskManagerFilesStorageM();
    //     }
    //
    //     return myDatabase;
    // }
    public TaskManagerFilesStorageM()
    {
        SQLiteConnection con;
        mySQLiteConnection = new SQLiteConnection("Data Source=FilesStorage.sqlite3");
        if (!File.Exists("FilesStorage.sqlite3"))
        {
            SQLiteConnection.CreateFile("FilesStorage.sqlite3");
            Console.WriteLine("FilesStorage Database file created");
            string createTableQuery = @"CREATE TABLE Tasks(
                          Title TEXT NOT NULL PRIMARY KEY,
                          Priority       TEXT        NULL,
                          Description    TEXT        NULL,
                          CreationTime   TEXT        NULL,
                          IsDone         TEXT        NULL,
                          DueDate        TEXT        NULL
                          );";
           
            string createSubTableQuery = @"CREATE TABLE Sub_Tasks(
                          PrimaryTaskTitle TEXT  NOT NULL,
                          SubTaskTitle     TEXT  NOT NULL,
                          Priority       TEXT        NULL,
                          Description    TEXT        NULL,
                          CreationTime   TEXT        NULL,
                          IsDone         TEXT        NULL,
                          DueDate        TEXT        NULL
                          );";

            string createLabelsTableQuery = @"CREATE TABLE Labels(
                          PrimaryTaskTitle TEXT  NOT NULL PRIMARY KEY,
                          LabelTitle     TEXT  NOT NULL,
                          );";
            
            
            using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
            {
                c.Open();
                using (SQLiteCommand com = new SQLiteCommand(createTableQuery, c))
                {
                     // Open the connection to the database

                    com.CommandText = createTableQuery; // Set CommandText to our query that will create the table
                    com.ExecuteNonQuery(); // Execute the query
                }
                
                using (SQLiteCommand com = new SQLiteCommand(createSubTableQuery, c))
                {
                    // Open the connection to the database

                    com.CommandText = createSubTableQuery; // Set CommandText to our query that will create the table
                    com.ExecuteNonQuery(); // Execute the query
                }
                
                using (SQLiteCommand com = new SQLiteCommand(createSubTableQuery, c))
                {
                    // Open the connection to the database

                    com.CommandText = createSubTableQuery; // Set CommandText to our query that will create the table
                    com.ExecuteNonQuery(); // Execute the query
                }
                
                using (SQLiteCommand com = new SQLiteCommand(createLabelsTableQuery, c))
                {
                    // Open the connection to the database

                    com.CommandText = createLabelsTableQuery; // Set CommandText to our query that will create the table
                    com.ExecuteNonQuery(); // Execute the query
                }

            }
        }
    }


    public void AddLabel(Task t, string label)
    {
        string insertQuery =
            "INSERT INTO Labels (`PrimaryTaskTitle`,`LabelTitle`) VALUES (@primarytasktitle, @labeltitle)";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, c))
            {
                //cmd.ExecuteNonQuery();
                //mySQLiteCommand.Connection.Open();
                mySQLiteCommand.Parameters.AddWithValue("@primarytasktitle", t.Title);
                mySQLiteCommand.Parameters.AddWithValue("@labeltitle", label);
                mySQLiteCommand.ExecuteNonQuery();
            }
        }
    }
    
    public void InsertNewTask(Task task)
    {
        //TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        List<string> l = task.Labels;
        string insertQuery =
            "INSERT INTO Tasks (`Title`,`Priority`, `Description`, `CreationTime`, `IsDone`, `DueDate`) VALUES (@title,@priority,@description,@creationtime,@isdone,@duedate)";
        string insertQuerySub =
            "INSERT INTO Sub_Tasks (`PrimaryTaskTitle`,`Title`, `Description`, `CreationTime`, `IsDone`, `DueDate`) VALUES (@primaryTaskTitle,@title,@priority,@description,@creationtime,@isdone,@duedate)";
        string insertLabelQuery =
            "INSERT INTO Labels (`PrimaryTaskTitle`,`LabelTitle`) VALUES (@primarytasktitle, @labeltitle)";
        //SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, myDatabase.mySQLiteConnection);
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, c))
            {
                //cmd.ExecuteNonQuery();
                //mySQLiteCommand.Connection.Open();
                mySQLiteCommand.Parameters.AddWithValue("@title", task.Title);
                mySQLiteCommand.Parameters.AddWithValue("@priority", task.Priority);
                mySQLiteCommand.Parameters.AddWithValue("@description", task.Description);
                mySQLiteCommand.Parameters.AddWithValue("@creationtime", task.CreationTime);
                mySQLiteCommand.Parameters.AddWithValue("@isdone", task.IsDone);
                mySQLiteCommand.Parameters.AddWithValue("@dueDate", task.DueTime);
                mySQLiteCommand.ExecuteNonQuery();
            }

            using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(insertQuerySub, c))
            {
                //cmd.ExecuteNonQuery();
                //mySQLiteCommand.Connection.Open();
                foreach (Task t in task.SubTasks)
                {
                    mySQLiteCommandSub.Parameters.AddWithValue("@primarytasktitle", task.Title);
                    mySQLiteCommandSub.Parameters.AddWithValue("@subtasktitle", t.Title);
                    mySQLiteCommandSub.Parameters.AddWithValue("@priority", t.Priority);
                    mySQLiteCommandSub.Parameters.AddWithValue("@description", t.Description);
                    mySQLiteCommandSub.Parameters.AddWithValue("@creationtime", t.CreationTime);
                    mySQLiteCommandSub.Parameters.AddWithValue("@isdone", t.IsDone);
                    mySQLiteCommandSub.Parameters.AddWithValue("@duedate", t.DueTime);
                    mySQLiteCommandSub.ExecuteNonQuery();
            
                }
            }
            
            using (SQLiteCommand mySQLiteCommandLabel = new SQLiteCommand(insertLabelQuery, c))
            {
                //cmd.ExecuteNonQuery();
                //mySQLiteCommand.Connection.Open();
                foreach (string label in task.Labels)
                {
                    mySQLiteCommandLabel.Parameters.AddWithValue("@primarytasktitle", task.Title);
                    mySQLiteCommandLabel.Parameters.AddWithValue("@labeltitle", label);
                    mySQLiteCommandLabel.ExecuteNonQuery();
            
                }
            }


            // using (SQLiteCommand selectCMD = c.CreateCommand())
            // {
            //     selectCMD.CommandText = "select max(id) new_id from Tasks";
            //     selectCMD.CommandType = CommandType.Text;
            //     SQLiteDataReader myReader = selectCMD.ExecuteReader();
            //     string s = myReader["Id"].ToString();
            //     Console.WriteLine(s);
            // }

        }
    }

    public void InsertNewSubTask(Task t, Task subTask)
    {
        //TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
       
        //string insertQuery = "INSERT INTO Tasks (`Title`,`Priority`, `Description`, `CreationTime`, `IsDone`, `DueTime`) VALUES (@title,@priority,@description,@creationtime,@isdone,@duetime)";
        string insertQuerySub = "INSERT INTO Sub_Tasks (`PrimaryTaskTitle`,`SubTaskTitle`, `Description`, `CreationTime`, `IsDone`, `DueTime`, `Priority`) VALUES (@primaryTaskTitle,@subtasktitle,@priority,@description,@creationtime,@isdone,@duetime)";
        //SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, myDatabase.mySQLiteConnection);
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(insertQuerySub, c))
            {
                //cmd.ExecuteNonQuery();
                //mySQLiteCommand.Connection.Open();
                mySQLiteCommandSub.Parameters.AddWithValue("@primarytasktitle",t.Title);
                mySQLiteCommandSub.Parameters.AddWithValue("@subtasktitle",subTask.Title);
                mySQLiteCommandSub.Parameters.AddWithValue("@priority",subTask.Priority);
                mySQLiteCommandSub.Parameters.AddWithValue("@description",subTask.Description);
                mySQLiteCommandSub.Parameters.AddWithValue("@creationtime",subTask.CreationTime);
                mySQLiteCommandSub.Parameters.AddWithValue("@isdone",subTask.IsDone);
                mySQLiteCommandSub.Parameters.AddWithValue("@duetime", subTask.DueTime);
                mySQLiteCommandSub.ExecuteNonQuery();
            }

        }
        
        // selectCMD.CommandText = "SELECT * FROM Tasks";
        // selectCMD.CommandType = CommandType.Text;
        // SQLiteDataReader myReader = selectCMD.ExecuteReader();
        //Console.WriteLine("abc");
        // mySQLiteCommand.Connection.Open();
        // mySQLiteCommand.Parameters.AddWithValue("@title",task.Title);
        // mySQLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
        // mySQLiteCommand.Parameters.AddWithValue("@description",task.Description);
        // mySQLiteCommand.Parameters.AddWithValue("@duetime", task.DueTime);
        // mySQLiteCommand.ExecuteNonQuery();
        // string insertId = "select max(id) new_id from Tasks";
        // mySQLiteCommand.CommandText = "SELECT max(id) FROM Tasks";
        // SQLiteDataReader myReader = mySQLiteCommand.ExecuteReader();
        
        //int.Parse(task.Id = myReader());
        //task.SetID(id);
        //Console.WriteLine(fruitInsertResult);
        //mySQLiteCommand.Connection.Close();
    }

    public void DeleteLabelFromDb(string label)
    {
        // TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        // int taskId = t.Id;
        string deleteQuery = "DELETE FROM Labels WHERE labeltitle ='" + label + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, c))
            {
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
        // SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, myDatabase.mySQLiteConnection);
        // mySQLiteCommand.Connection.Open();
        // mySQLiteCommand.ExecuteNonQuery();
        // mySQLiteCommand.Connection.Close();
    }

    
    public void DeleteTaskFromDb(Task t)
    {
        // TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        // int taskId = t.Id;
        string deleteQuery = "DELETE FROM Tasks WHERE title ='" + t.Title + "';";
        string deleteQuerySub = "DELETE FROM Sub_Tasks WHERE primarytasktitle ='" + t.Title + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, c))
            {
                mySQLiteCommand.ExecuteNonQuery();        
            }
            using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(deleteQuerySub, c))
            {
                mySQLiteCommandSub.ExecuteNonQuery();        
            }
        }
        // SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, myDatabase.mySQLiteConnection);
        // mySQLiteCommand.Connection.Open();
        // mySQLiteCommand.ExecuteNonQuery();
        // mySQLiteCommand.Connection.Close();
    }
    
    public void DeleteSubTaskFromDb(Task subTask)
    {
        // TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        // int taskId = t.Id;
        string deleteQuerySub = "DELETE FROM Sub_Tasks WHERE Title ='" + subTask.Title + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(deleteQuerySub, c))
            {
                mySQLiteCommandSub.ExecuteNonQuery();        
            }
        }
        // SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, myDatabase.mySQLiteConnection);
        // mySQLiteCommand.Connection.Open();
        // mySQLiteCommand.ExecuteNonQuery();
        // mySQLiteCommand.Connection.Close();
    }

    public void UpdateLabel(string label)
    {
        // TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        string updateQuery = "UPDATE Labels SET LabelTitle = @labeltitle  where labeltitle = '" + label + "';";
        //SQLiteCommand mySqLiteCommand = new SQLiteCommand(updateQuery, myDatabase.mySQLiteConnection);
        //Console.WriteLine("abc");
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
            {
                mySQLiteCommand.Parameters.AddWithValue("@labeltitle",label);
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
        //mySqLiteCommand.Connection.Open();
        // mySqLiteCommand.Parameters.AddWithValue("@title",task.Title);
        // mySqLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
        // mySqLiteCommand.Parameters.AddWithValue("@description",task.Description);
        // mySqLiteCommand.Connection.Close();
    }

    public void UpdateTaskInDb(Task task)
    {
        // TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        string updateQuery = "UPDATE Tasks SET Title = @title, Priority = @priority, Description = @description, DueTime = @duetime, IsDone = @isdone, CreationTime = @creationtime  where title = '" + task.Title + "';";
        //SQLiteCommand mySqLiteCommand = new SQLiteCommand(updateQuery, myDatabase.mySQLiteConnection);
        //Console.WriteLine("abc");
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
            {
                mySQLiteCommand.Parameters.AddWithValue("@title",task.Title);
                mySQLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
                mySQLiteCommand.Parameters.AddWithValue("@description",task.Description);
                mySQLiteCommand.Parameters.AddWithValue("@duetime",task.DueTime);
                mySQLiteCommand.Parameters.AddWithValue("@isdone",task.IsDone);
                mySQLiteCommand.Parameters.AddWithValue("@creationtime",task.CreationTime);
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
        //mySqLiteCommand.Connection.Open();
        // mySqLiteCommand.Parameters.AddWithValue("@title",task.Title);
        // mySqLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
        // mySqLiteCommand.Parameters.AddWithValue("@description",task.Description);
        // mySqLiteCommand.Connection.Close();
    }
    
    public void UpdateSubTaskInDb(Task task)
    {
        // TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        string updateQuery = "UPDATE Sub_Tasks SET Title = @title, Priority = @priority, Description = @description, DueTime = @duetime, IsDone = @isdone, CreationTime = @creationtime  where title = '" + task.Title + "';";
        //SQLiteCommand mySqLiteCommand = new SQLiteCommand(updateQuery, myDatabase.mySQLiteConnection);
        //Console.WriteLine("abc");
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
            {
                mySQLiteCommand.Parameters.AddWithValue("@title",task.Title);
                mySQLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
                mySQLiteCommand.Parameters.AddWithValue("@description",task.Description);
                mySQLiteCommand.Parameters.AddWithValue("@duetime",task.DueTime);
                mySQLiteCommand.Parameters.AddWithValue("@isdone",task.IsDone);
                mySQLiteCommand.Parameters.AddWithValue("@creationtime",task.CreationTime);
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
        //mySqLiteCommand.Connection.Open();
        // mySqLiteCommand.Parameters.AddWithValue("@title",task.Title);
        // mySqLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
        // mySqLiteCommand.Parameters.AddWithValue("@description",task.Description);
        // mySqLiteCommand.Connection.Close();
    }

    public ITaskCollection GetSubTasksFromDb(Task t)
    {
        ITaskCollection ret = new ListTaskCollection();
        List<Task> ls;
        using (SQLiteConnection connection = new SQLiteConnection(mySQLiteConnection))
        {
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT * FROM Sub_Tasks WHERE Primar";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                while (myReader.Read())
                {
                    // string primaryTaskTitle = myReader.GetValue(0).ToString();
                    // string subTaskTitle = myReader.GetValue(1).ToString();
                    // int priority = myReader.GetInt32(2);
                    // string description = myReader.GetValue(3).ToString();
                    // string creationTime = myReader.GetValue(4).ToString();
                    // string isDone = myReader.GetValue(5).ToString();
                    // string dueDate = myReader.GetValue(6).ToString();
                    // Task t = new Task(subTaskTitle, priority, description, creationTime, isDone, dueDate);
                    // ls.Add(t);
                }
            }
    
        }
    
        return ret;
    }
    
    
    public ITaskCollection GetTasksFromDb()
    {
        //TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
        ITaskCollection ret = new ListTaskCollection();
        
        using (SQLiteConnection connection = new SQLiteConnection(mySQLiteConnection))
        {
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                Task.TaskPriority priority;
                string done;
                
                selectCMD.CommandText = "SELECT * FROM Tasks";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                while (myReader.Read())
                {
                    string title = myReader.GetValue(0).ToString();
                    string p = myReader.GetValue(1).ToString();
                    if (p.Equals("High"))
                    {
                        priority = Task.TaskPriority.High;
                    }
                    else if (p.Equals("Medium"))
                    {
                        priority = Task.TaskPriority.Medium;
                    }
                    
                    else
                    {
                        priority = Task.TaskPriority.Low;
                    }
                    
                    //Task.TaskPriority priority = Task.TaskPriority.TryParse(myReader.GetValue(1));
                    string description = myReader.GetValue(2).ToString();
                    DateTime creationTime = DateTime.Parse(myReader.GetValue(3).ToString());
                    done = myReader.GetValue(4).ToString();
                    DateTime dueTime = DateTime.Parse(myReader.GetValue(5).ToString());
                    bool isDone;
                    if (done.Equals("true"))
                    {
                        isDone = true;
                    }
                    else
                    {
                        isDone = false;
                    }

                    //bool isDone = myReader.GetValue(4).ToString();
                    //string dueDate = myReader.GetValue(5).ToString();
                    Task t = new Task(title, priority, description, dueTime);
                    t.IsDone = isDone;
                    //ls.Add(t);
                    //Task t = new Task(myReader["Title"], Int(myReader["Priority"]), myReader["Description"], myReader["DueTime"], myReader["Labels"], myReader["SubTasks"]);
                    ret.Add(t);
                }
            }
    
        }
    
        return ret;
    }
    
    public List<Task> GetSubTasksFromDb(Task t)
    {
        List<Task> ret = new List<Task>();
        using (SQLiteConnection connection = new SQLiteConnection(this.mySQLiteConnection))
        {
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT * FROM Sub_Tasks WHERE PrimaryTaskTitle ='" + t.Title + "';";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                // while (myReader.Read())
                // {
                //     Task t2 = new Task(myReader["Title"].ToString(), myReader["Priority"].ToString(), myReader["Description"].ToString(), myReader["DueTime"].ToString(), myReader["Labels"].ToString(), myReader["SubTasks"].ToString());
                //     ret.Add(t2);
                // }
            }
    
        }
    
        return ret;
    }

    // static void Main(string[] args)
    // {
    //     Task t = new Task("yit", Task.TaskPriority.High, "fffffff", null, null, null);
    //     Task t2 = new Task("yit", Task.TaskPriority.High, "bbb", null, null, null);
    //     Task t3 = new Task("yit", Task.TaskPriority.High, "cccc", null, null, null);
    //     Task t4 = new Task("ron", Task.TaskPriority.Medium, "ddddd", null, null, null);
    //     TaskManagerFilesStorageM tmfs = new TaskManagerFilesStorageM();
    //     //TaskManagerFilesStorageM tm = GetMyDataBase();
    //     tmfs.InsertNewTask(t);
    // }
    // tmfs.InsertNewTask(t2);
    //     // tmfs.InsertNewTask(t3);
    //     // Console.WriteLine(t.Id);
    //     // Console.WriteLine(t2.Id);
    //     // Console.WriteLine(t3.Id);
    //     //tmfs.DeleteTaskFromDb(t2);
    //     //tmfs.UpdateTaskInDbM(t4);
    //
    //     //Console.ReadLine();
    //     /*
    //     
    //     */
    // }
}