// using System;
// using System.ComponentModel;
// using System.Data;
// using System.IO;
// using Microsoft.Data.Sqlite;
// using System.Data.SQLite;
// namespace TaskManager.Models;
//
//
// public class TaskManagerFilesStorageM : ITaskManagerFilesStorage
// {
//     private SQLiteConnection mySQLiteConnection;
//     // private TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
//     // private TaskManagerFilesStorageM()
//     // {
//     //     mySQLiteConnection = new SQLiteConnection("Data Source=FilesStorage.sqlite3");
//     //     if (!File.Exists("./FilesStorage.sqlite3"))
//     //     {
//     //     
//     //         SQLiteConnection.CreateFile("FilesStorage.sqlite3");
//     //         Console.WriteLine("FilesStorage Database file created");
//     //     }
//     // }
//     // public static TaskManagerFilesStorageM GetMyDataBase()
//     // {
//     //     if (myDatabase == null)
//     //     {
//     //         myDatabase = new TaskManagerFilesStorageM();
//     //     }
//     //
//     //     return myDatabase;
//     // }
//     public TaskManagerFilesStorageM()
//     {
//         mySQLiteConnection = new SQLiteConnection("Data Source=FilesStorage.sqlite3");
//         if (!File.Exists("./FilesStorage.sqlite3"))
//         {
//         
//             SQLiteConnection.CreateFile("FilesStorage.sqlite3");
//             Console.WriteLine("FilesStorage Database file created");
//         }
//     }
//     public void InsertNewTaskM(Task task)
//     {
//         TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
//        
//         string insertQuery = "INSERT INTO Tasks (`Title`,`Priority`, `Description`, `CreationTime`, `IsDone`, `DueTime`) VALUES (@title,@priority,@description,@creationtime,@isdone,@duetime)";
//         string insertQuerySub = "INSERT INTO Sub_Tasks (`PrimaryTaskId`,`Title`, `Description`, `CreationTime`, `IsDone`, `DueTime`) VALUES (@primaryTaskId,@title,@priority,@description,@creationtime,@isdone,@duetime)";
//         //SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, myDatabase.mySQLiteConnection);
//         using (SQLiteConnection c = new SQLiteConnection(myDatabase.mySQLiteConnection))
//         {
//             c.Open();
//             using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(insertQuery, c))
//             {
//                 //cmd.ExecuteNonQuery();
//                 //mySQLiteCommand.Connection.Open();
//                 mySQLiteCommand.Parameters.AddWithValue("@title",task.Title);
//                 mySQLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
//                 mySQLiteCommand.Parameters.AddWithValue("@description",task.Description);
//                 mySQLiteCommand.Parameters.AddWithValue("@creationtime",task.CreationTime);
//                 mySQLiteCommand.Parameters.AddWithValue("@isdone",task.IsDone);
//                 mySQLiteCommand.Parameters.AddWithValue("@duetime", task.DueTime);
//                 mySQLiteCommand.ExecuteNonQuery();
//             }
//             
//             using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(insertQuerySub, c))
//             {
//                 //cmd.ExecuteNonQuery();
//                 //mySQLiteCommand.Connection.Open();
//                 foreach (Task t in task.SubTasks)
//                 {
//                     mySQLiteCommandSub.Parameters.AddWithValue("@primarytaskid",task.PrimaryId);
//                     mySQLiteCommandSub.Parameters.AddWithValue("@title",task.Title);
//                     mySQLiteCommandSub.Parameters.AddWithValue("@priority",task.Priority);
//                     mySQLiteCommandSub.Parameters.AddWithValue("@description",task.Description);
//                     mySQLiteCommandSub.Parameters.AddWithValue("@creationtime",task.CreationTime);
//                     mySQLiteCommandSub.Parameters.AddWithValue("@isdone",task.IsDone);
//                     mySQLiteCommandSub.Parameters.AddWithValue("@duetime", task.DueTime);
//                     mySQLiteCommandSub.ExecuteNonQuery();
//   
//                 }
//             }
//
//
//             // using (SQLiteCommand selectCMD = c.CreateCommand())
//             // {
//             //     selectCMD.CommandText = "select max(id) new_id from Tasks";
//             //     selectCMD.CommandType = CommandType.Text;
//             //     SQLiteDataReader myReader = selectCMD.ExecuteReader();
//             //     string s = myReader["Id"].ToString();
//             //     Console.WriteLine(s);
//             // }
//
//         }
//         
//         // selectCMD.CommandText = "SELECT * FROM Tasks";
//         // selectCMD.CommandType = CommandType.Text;
//         // SQLiteDataReader myReader = selectCMD.ExecuteReader();
//         //Console.WriteLine("abc");
//         // mySQLiteCommand.Connection.Open();
//         // mySQLiteCommand.Parameters.AddWithValue("@title",task.Title);
//         // mySQLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
//         // mySQLiteCommand.Parameters.AddWithValue("@description",task.Description);
//         // mySQLiteCommand.Parameters.AddWithValue("@duetime", task.DueTime);
//         // mySQLiteCommand.ExecuteNonQuery();
//         // string insertId = "select max(id) new_id from Tasks";
//         // mySQLiteCommand.CommandText = "SELECT max(id) FROM Tasks";
//         // SQLiteDataReader myReader = mySQLiteCommand.ExecuteReader();
//         
//         //int.Parse(task.Id = myReader());
//         //task.SetID(id);
//         //Console.WriteLine(fruitInsertResult);
//         //mySQLiteCommand.Connection.Close();
//     }
//
//     public void DeleteTaskFromDbM(Task t)
//     {
//         TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
//         int taskId = t.Id;
//         string deleteQuery = "DELETE FROM Tasks WHERE id ='" + taskId + "';";
//         string deleteQuerySub = "DELETE FROM Sub_Tasks WHERE id ='" + taskId + "';";
//         using (SQLiteConnection c = new SQLiteConnection(myDatabase.mySQLiteConnection))
//         {
//             c.Open();
//             using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, c))
//             {
//                 mySQLiteCommand.ExecuteNonQuery();        
//             }
//             using (SQLiteCommand mySQLiteCommandSub = new SQLiteCommand(deleteQuerySub, c))
//             {
//                 mySQLiteCommandSub.ExecuteNonQuery();        
//             }
//         }
//         // SQLiteCommand mySQLiteCommand = new SQLiteCommand(deleteQuery, myDatabase.mySQLiteConnection);
//         // mySQLiteCommand.Connection.Open();
//         // mySQLiteCommand.ExecuteNonQuery();
//         // mySQLiteCommand.Connection.Close();
//     }
//
//     public void UpdateTaskInDbM(Task task)
//     {
//         TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
//         string updateQuery = "UPDATE Tasks SET Title = @title, Priority = @priority, Description = @description, DueTime = @duetime, IsDone = @isdone, CreationTime = @creationtime  where id = '" + task.Id + "';";
//         //SQLiteCommand mySqLiteCommand = new SQLiteCommand(updateQuery, myDatabase.mySQLiteConnection);
//         //Console.WriteLine("abc");
//         using (SQLiteConnection c = new SQLiteConnection(myDatabase.mySQLiteConnection))
//         {
//             c.Open();
//             using (SQLiteCommand mySQLiteCommand = new SQLiteCommand(updateQuery, c))
//             {
//                 mySQLiteCommand.Parameters.AddWithValue("@title",task.Title);
//                 mySQLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
//                 mySQLiteCommand.Parameters.AddWithValue("@description",task.Description);
//                 mySQLiteCommand.Parameters.AddWithValue("@duetime",task.DueTime);
//                 mySQLiteCommand.Parameters.AddWithValue("@isdone",task.IsDone);
//                 mySQLiteCommand.Parameters.AddWithValue("@creationtime",task.CreationTime);
//                 mySQLiteCommand.ExecuteNonQuery();        
//             }
//         }
//         //mySqLiteCommand.Connection.Open();
//         // mySqLiteCommand.Parameters.AddWithValue("@title",task.Title);
//         // mySqLiteCommand.Parameters.AddWithValue("@priority",task.Priority);
//         // mySqLiteCommand.Parameters.AddWithValue("@description",task.Description);
//         // mySqLiteCommand.Connection.Close();
//     }
//     
//     public List<Task> GetTasksFromDbM()
//     {
//         TaskManagerFilesStorageM myDatabase = new TaskManagerFilesStorageM();
//         List<Task> ret = new List<Task>();
//         using (SQLiteConnection connection = new SQLiteConnection(myDatabase.mySQLiteConnection))
//         {
//             connection.Open();
//             using (SQLiteCommand selectCMD = connection.CreateCommand())
//             {
//                 selectCMD.CommandText = "SELECT * FROM Tasks";
//                 selectCMD.CommandType = CommandType.Text;
//                 SQLiteDataReader myReader = selectCMD.ExecuteReader();
//                 // while (myReader.Read())
//                 // {
//                 //     Task t = new Task(myReader["Title"].ToString(), myReader["Priority"], myReader["Description"].ToString());
//                 //     ret.Add(t);
//                 // }
//             }
//     
//         }
//     
//         return ret;
//     }
//     
//     // static void Main(string[] args)
//     // {
//     //     Task t = new Task("yit", Task.TaskPriority.High, "fffffff", null, null, null);
//     //     Task t2 = new Task("yit", Task.TaskPriority.High, "bbb", null, null, null);
//     //     Task t3 = new Task("yit", Task.TaskPriority.High, "cccc", null, null, null);
//     //     Task t4 = new Task("ron", Task.TaskPriority.Medium, "ddddd", null, null, null);
//     //     TaskManagerFilesStorageM tmfs = new TaskManagerFilesStorageM();
//     //     //TaskManagerFilesStorageM tm = GetMyDataBase();
//     //     tmfs.InsertNewTaskM(t);
//     //     // tmfs.InsertNewTask(t2);
//     //     // tmfs.InsertNewTask(t3);
//     //     // Console.WriteLine(t.Id);
//     //     // Console.WriteLine(t2.Id);
//     //     // Console.WriteLine(t3.Id);
//     //     //tmfs.DeleteTaskFromDb(t2);
//     //     //tmfs.UpdateTaskInDbM(t4);
//     //
//     //     //Console.ReadLine();
//     //     /*
//     //     
//     //     */
//     // }
// }