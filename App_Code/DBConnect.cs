using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DBConnect
/// </summary>
public class DBConnect
{
    public MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    private MainController oMainCon;
    private String sErrorLog = "";
    //Constructor
    public DBConnect(String _sErrorLog)
    {
        Initialize();
        sErrorLog = _sErrorLog;
        oMainCon = new MainController(sErrorLog);
    }

    //Initialize values
    private void Initialize()
    {
        connection = new MySqlConnection(ConfigurationSettings.AppSettings["MyConnection"]);
    }

    //open connection to database
    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            switch (ex.Number)
            {
                case 0:
                    int number;
                    if (ex.InnerException != null && ex.InnerException is MySqlException)
                    {
                        number = ((MySqlException)ex.InnerException).Number;
                    }
                    else
                    {
                        number = ex.Number;
                    }
                    oMainCon.WriteToLogFile("Cannot connect to server.  Contact administrator - errorno: " + number);
                    break;

                case 1045:
                    int number2;
                    if (ex.InnerException != null && ex.InnerException is MySqlException)
                    {
                        number2 = ((MySqlException)ex.InnerException).Number;
                    }
                    else
                    {
                        number2 = ex.Number;
                    }
                    oMainCon.WriteToLogFile("Invalid username/password, please try again - errorno: " + number2);
                    break;

                default:
                    oMainCon.WriteToLogFile(ex.Message);
                    break;
            }
            return false;
        }
    }

    //Close connection
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            oMainCon.WriteToLogFile(ex.Message);
            return false;
        }
    }

    //Insert statement
    public void Insert()
    {
        string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Update statement
    public void Update()
    {
        string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Delete statement
    public void Delete()
    {
        string query = "DELETE FROM tableinfo WHERE name='John Smith'";

        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    //Select statement
    public List<string>[] Select()
    {
        string query = "SELECT * FROM tableinfo";

        //Create a list to store the result
        List<string>[] list = new List<string>[3];
        list[0] = new List<string>();
        list[1] = new List<string>();
        list[2] = new List<string>();

        //Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["id"] + "");
                list[1].Add(dataReader["name"] + "");
                list[2].Add(dataReader["age"] + "");
            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();

            //return list to be displayed
            return list;
        }
        else
        {
            return list;
        }
    }

    //Count statement
    public int Count()
    {
        string query = "SELECT Count(*) FROM tableinfo";
        int Count = -1;

        //Open Connection
        if (this.OpenConnection() == true)
        {
            //Create Mysql Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar() + "");

            //close Connection
            this.CloseConnection();

            return Count;
        }
        else
        {
            return Count;
        }
    }

    //Backup
    public void Backup()
    {
        try
        {
            DateTime Time = DateTime.Now;
            int year = Time.Year;
            int month = Time.Month;
            int day = Time.Day;
            int hour = Time.Hour;
            int minute = Time.Minute;
            int second = Time.Second;
            int millisecond = Time.Millisecond;

            //Save file to C:\ with the current date as a filename
            string path;
            path = "C:\\MySqlBackup" + year + "-" + month + "-" + day +
        "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
            StreamWriter file = new StreamWriter(path);


            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "mysqldump";
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = true;
            psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                uid, password, server, database);
            psi.UseShellExecute = false;

            Process process = Process.Start(psi);

            string output;
            output = process.StandardOutput.ReadToEnd();
            file.WriteLine(output);
            process.WaitForExit();
            file.Close();
            process.Close();
        }
        catch (IOException ex)
        {
            oMainCon.WriteToLogFile("Error , unable to backup!" + ex.ToString());
        }
    }

    //Restore
    public void Restore()
    {
        try
        {
            //Read file from C:\
            string path;
            path = "C:\\MySqlBackup.sql";
            StreamReader file = new StreamReader(path);
            string input = file.ReadToEnd();
            file.Close();

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "mysql";
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = false;
            psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                uid, password, server, database);
            psi.UseShellExecute = false;


            Process process = Process.Start(psi);
            process.StandardInput.WriteLine(input);
            process.StandardInput.Close();
            process.WaitForExit();
            process.Close();
        }
        catch (IOException ex)
        {
            oMainCon.WriteToLogFile("Error , unable to Restore!" + ex.ToString());
        }
    }
}

public class DBConnect2
{
    public SqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    private MainController oMainCon;
    private String sErrorLog = "";
    //Constructor
    public DBConnect2(String _sErrorLog)
    {

        Initialize();
        sErrorLog = _sErrorLog;
        oMainCon = new MainController(sErrorLog);
    }

    //Initialize values
    private void Initialize()
    {
        connection = new SqlConnection(ConfigurationManager.AppSettings["MSSQLConnection"]);
    }

    //open connection to database
    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (SqlException ex)
        {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            switch (ex.Number)
            {
                case 0:
                    int number;
                    if (ex.InnerException != null && ex.InnerException is MySqlException)
                    {
                        number = ((MySqlException)ex.InnerException).Number;
                    }
                    else
                    {
                        number = ex.Number;
                    }
                    oMainCon.WriteToLogFile("Cannot connect to server.  Contact administrator - errorno: " + number);
                    break;

                case 1045:
                    int number2;
                    if (ex.InnerException != null && ex.InnerException is MySqlException)
                    {
                        number2 = ((MySqlException)ex.InnerException).Number;
                    }
                    else
                    {
                        number2 = ex.Number;
                    }
                    oMainCon.WriteToLogFile("Invalid username/password, please try again - errorno: " + number2);
                    break;

                default:
                    oMainCon.WriteToLogFile(ex.Message);
                    break;

            }
            return false;
        }
    }

    //Close connection
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (SqlException ex)
        {
            oMainCon.WriteToLogFile(ex.Message);
            return false;
        }
    }

    //Insert statement
    public void Insert()
    {
        string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            SqlCommand cmd = new SqlCommand(query, connection);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Update statement
    public void Update()
    {
        string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            SqlCommand cmd = new SqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Delete statement
    public void Delete()
    {
        string query = "DELETE FROM tableinfo WHERE name='John Smith'";

        if (this.OpenConnection() == true)
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    //Select statement
    public List<string>[] Select()
    {
        string query = "SELECT * FROM tableinfo";

        //Create a list to store the result
        List<string>[] list = new List<string>[3];
        list[0] = new List<string>();
        list[1] = new List<string>();
        list[2] = new List<string>();

        //Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            SqlCommand cmd = new SqlCommand(query, connection);
            //Create a data reader and Execute the command
            SqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["id"] + "");
                list[1].Add(dataReader["name"] + "");
                list[2].Add(dataReader["age"] + "");
            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();

            //return list to be displayed
            return list;
        }
        else
        {
            return list;
        }
    }

    //Count statement
    public int Count()
    {
        string query = "SELECT Count(*) FROM tableinfo";
        int Count = -1;

        //Open Connection
        if (this.OpenConnection() == true)
        {
            //Create Mysql Command
            SqlCommand cmd = new SqlCommand(query, connection);

            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar() + "");

            //close Connection
            this.CloseConnection();

            return Count;
        }
        else
        {
            return Count;
        }
    }

    //Backup
    public void Backup()
    {
        try
        {
            DateTime Time = DateTime.Now;
            int year = Time.Year;
            int month = Time.Month;
            int day = Time.Day;
            int hour = Time.Hour;
            int minute = Time.Minute;
            int second = Time.Second;
            int millisecond = Time.Millisecond;

            //Save file to C:\ with the current date as a filename
            string path;
            path = "C:\\MsSqlBackup" + year + "-" + month + "-" + day +
        "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
            StreamWriter file = new StreamWriter(path);


            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "mssqldump";
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = true;
            psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                uid, password, server, database);
            psi.UseShellExecute = false;

            Process process = Process.Start(psi);

            string output;
            output = process.StandardOutput.ReadToEnd();
            file.WriteLine(output);
            process.WaitForExit();
            file.Close();
            process.Close();
        }
        catch (IOException ex)
        {
            oMainCon.WriteToLogFile("Error , unable to backup!" + ex.ToString());
        }
    }

    //Restore
    public void Restore()
    {
        try
        {
            //Read file from C:\
            string path;
            path = "C:\\MsSqlBackup.sql";
            StreamReader file = new StreamReader(path);
            string input = file.ReadToEnd();
            file.Close();

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "mmssql";
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = false;
            psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                uid, password, server, database);
            psi.UseShellExecute = false;


            Process process = Process.Start(psi);
            process.StandardInput.WriteLine(input);
            process.StandardInput.Close();
            process.WaitForExit();
            process.Close();
        }
        catch (IOException ex)
        {
            oMainCon.WriteToLogFile("Error , unable to Restore!" + ex.ToString());
        }
    }
}