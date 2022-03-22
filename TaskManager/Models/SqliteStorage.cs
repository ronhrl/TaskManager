using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using TaskManager.Models;
using TaskManager.TaskCollections;

namespace TaskManager.Models;


public class SqliteStorage : ITaskManagerStorage
{
    private SQLiteConnection mySQLiteConnection;
    public SqliteStorage()
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
                          PrimaryTaskTitle TEXT  NOT NULL,
                          LabelTitle     TEXT  NOT NULL
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
                
                using (SQLiteCommand com = new SQLiteCommand(createLabelsTableQuery, c))
                {
                    // Open the connection to the database

                    com.CommandText = createLabelsTableQuery; // Set CommandText to our query that will create the table
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
                mySQLiteCommand.Parameters.AddWithValue("@primarytasktitle", t.Title);
                mySQLiteCommand.Parameters.AddWithValue("@labeltitle", label);
                mySQLiteCommand.ExecuteNonQuery();
            }
        }
    }
    
    public void InsertNewTask(Task task)
    {
        List<string> l = task.Labels;
        string insertQuery =
            "INSERT INTO Tasks (`Title`,`Priority`, `Description`, `CreationTime`, `IsDone`, `DueTime`) VALUES (@title,@priority,@description,@creationtime,@isdone,@duedate)";
        string insertQuerySub =
            "INSERT INTO Sub_Tasks (`PrimaryTaskTitle`,`SubTaskTitle`,`Priority`, `Description`, `CreationTime`, `IsDone`, `DueTime`) VALUES (@primaryTaskTitle,@SubTasktitle,@priority,@description,@creationtime,@isdone,@duedate)";
        string insertLabelQuery =
            "INSERT INTO Labels (`PrimaryTaskTitle`,`LabelTitle`) VALUES (@primarytasktitle, @labeltitle)";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, c))
            {
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
                foreach (string label in task.Labels)
                {
                    mySQLiteCommandLabel.Parameters.AddWithValue("@primarytasktitle", task.Title);
                    mySQLiteCommandLabel.Parameters.AddWithValue("@labeltitle", label);
                    mySQLiteCommandLabel.ExecuteNonQuery();
            
                }
            }
        }
    }

    public void InsertNewSubTask(Task t, Task subTask)
    {
        string insertQuerySub = "INSERT INTO Sub_Tasks (`PrimaryTaskTitle`,`SubTaskTitle`, `Description`, `CreationTime`, `IsDone`, `DueTime`, `Priority`) VALUES (@primaryTaskTitle,@subtasktitle,@priority,@description,@creationtime,@isdone,@duetime)";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(insertQuerySub, c))
            {
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
    }

    public void DeleteLabelFromDb(string label)
    {
        string deleteQuery = "DELETE FROM Labels WHERE labeltitle ='" + label + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, c))
            {
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
    }

    
    public void DeleteTaskFromDb(Task t)
    {
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
    }
    
    public void DeleteSubTaskFromDb(Task subTask)
    {
        string deleteQuerySub = "DELETE FROM Sub_Tasks WHERE Title ='" + subTask.Title + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(deleteQuerySub, c))
            {
                mySQLiteCommandSub.ExecuteNonQuery();        
            }
        }
    }

    public void UpdateLabel(string oldLabel, string newLabel)
    {
        string updateQuery = "UPDATE Labels SET LabelTitle = @labeltitle  where labeltitle = '" + oldLabel + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
            {
                mySQLiteCommand.Parameters.AddWithValue("@labeltitle",newLabel);
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
    }

    public void UpdateTaskInDb(Task oldTask, Task newTask)
    {
        string updateQuery = "UPDATE Tasks SET Title = @title, Priority = @priority, Description = @description, CreationTime = @CreationTime, IsDone = @isdone, DueTime = @duetime  WHERE Title = '" + oldTask.Title + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
            {
                mySQLiteCommand.Parameters.AddWithValue("@title",newTask.Title);
                mySQLiteCommand.Parameters.AddWithValue("@priority",newTask.Priority);
                mySQLiteCommand.Parameters.AddWithValue("@description",newTask.Description);
                mySQLiteCommand.Parameters.AddWithValue("@duetime",newTask.DueTime);
                mySQLiteCommand.Parameters.AddWithValue("@isdone",newTask.IsDone);
                mySQLiteCommand.Parameters.AddWithValue("@creationtime",newTask.CreationTime);
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
    }
    
    public void UpdateSubTaskInDb(Task oldTask, Task newTask)
    {
        string updateQuery = "UPDATE Sub_Tasks SET SubTaskTitle = @subtasktitle, Priority = @priority, Description = @description, CreationTime = @creationtime, IsDone = @isdone, DueTime = @DueDate  WHERE SubTaskTitle = '" + oldTask.Title + "';";
        using (SQLiteConnection c = new SQLiteConnection(this.mySQLiteConnection))
        {
            c.Open();
            using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
            {
                mySQLiteCommand.Parameters.AddWithValue("@title",newTask.Title);
                mySQLiteCommand.Parameters.AddWithValue("@priority",newTask.Priority);
                mySQLiteCommand.Parameters.AddWithValue("@description",newTask.Description);
                mySQLiteCommand.Parameters.AddWithValue("@creationtime",newTask.CreationTime);
                mySQLiteCommand.Parameters.AddWithValue("@isdone",newTask.IsDone);
                mySQLiteCommand.Parameters.AddWithValue("@duetime",newTask.DueTime);
                mySQLiteCommand.ExecuteNonQuery();        
            }
        }
    }

    public ITaskCollection GetTasksFromDb()
    {
        ITaskCollection ret = new ListTaskCollection();
        List<Task> subTasks;
        List<string> labels;
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
                    
                    string description = myReader.GetValue(2).ToString() != "" ? myReader.GetValue(2).ToString() : null;
                    DateTime creationTime = DateTime.Parse(myReader.GetValue(3).ToString());
                    done = myReader.GetValue(4).ToString();
                    DateTime? dueTime = myReader.GetValue(5).ToString() != "" ? DateTime.Parse(myReader.GetValue(5).ToString()) : null;
                    bool isDone;
                    if (done.Equals("1"))
                    {
                        isDone = true;
                    }
                    else
                    {
                        isDone = false;
                    }
                    
                    subTasks = GetSubTasksFromDb(title);
                    labels = GetLabelsFromDb(title);
                    Task t = new Task(title, priority, description, dueTime, labels, subTasks);
                    t.IsDone = isDone;
                    ret.Add(t);
                }
            }
    
        }
    
        return ret;
    }
    
    private List<Task> GetSubTasksFromDb(string taskTitle)
    {
        List<Task> ret = new List<Task>();
        Task.TaskPriority priority;
        string done;
        using (SQLiteConnection connection = new SQLiteConnection(this.mySQLiteConnection))
        {
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT * FROM Sub_Tasks WHERE PrimaryTaskTitle ='" + taskTitle + "';";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                while (myReader.Read()){
                    string title = myReader.GetValue(1).ToString();
                    string p = myReader.GetValue(2).ToString();
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
                    
                    string description = myReader.GetValue(3).ToString() != "" ? myReader.GetValue(3).ToString() : null;
                    DateTime creationTime = DateTime.Parse(myReader.GetValue(4).ToString());
                    done = myReader.GetValue(5).ToString();
                    DateTime? dueTime = myReader.GetValue(6).ToString() != "" ? DateTime.Parse(myReader.GetValue(6).ToString()) : null;
                    bool isDone;
                    if (done.Equals("true"))
                    {
                        isDone = true;
                    }
                    else
                    {
                        isDone = false;
                    }

                    Task t = new Task(title, priority, description, dueTime);
                    t.IsDone = isDone;
                    ret.Add(t);
                }
            }
        }
        return ret;
    }

    private List<string> GetLabelsFromDb(string taskTitle)
    {
        List<string> ret = new List<string>();
        using (SQLiteConnection connection = new SQLiteConnection(this.mySQLiteConnection))
        {
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT * FROM Labels WHERE PrimaryTaskTitle ='" + taskTitle + "';";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                while (myReader.Read()){
                    string title = myReader.GetValue(1).ToString();
                    ret.Add(title);
                }
            }
        }
        return ret;
    }
}