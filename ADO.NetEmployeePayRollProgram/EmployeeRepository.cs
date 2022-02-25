using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayRollProgram
{
    /// <summary>
    /// Created The Employee Repository Class To Check The DB Connectivity(UC1)
    /// </summary>
    public class EmployeeRepository
    {
        //Declaring Connection String
        public static string ConnectionString = @"Data Source=RAJ-VERMA;Initial Catalog=PayRoll_Service;Integrated Security=True;";
        //SqlConnection
        public static SqlConnection sqlConnection = null;

        //Method to check the sql connection is established(UC1)
        public static void GetAllEmployees()
        {
            try
            { 
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    //Object for employee class
                    EmployeeModel model = new EmployeeModel();
                    //Qurey to retreive data
                    string query = "Select * From Employee_Payroll";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    //Open Sql Connection
                    sqlConnection.Open();
                    //Returns object of result set
                    SqlDataReader result = command.ExecuteReader();
                    Console.WriteLine("Connection is properly established");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
