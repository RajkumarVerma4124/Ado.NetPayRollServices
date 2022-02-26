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
    /// Created The Employee Repository Class To Check The DB Connectivity And Fetch All Records(UC1&UC2)
    /// </summary>
    public class EmployeeRepository
    {
        //Declaring Connection String
        public static string ConnectionString = @"Data Source=RAJ-VERMA;Initial Catalog=PayRoll_Service;Integrated Security=True;";
        //SqlConnection
        public static SqlConnection sqlConnection = null;

        //Method to established sql connection and fetch all employee records(UC1&UC2)
        public static void GetAllEmployees()
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    //Created the object of employee model class
                    EmployeeModel model = new EmployeeModel();
                    //Qurey to retreive data
                    string query = "SELECT * FROM Employee_Payroll";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    //Open Sql Connection
                    sqlConnection.Open();
                    //Returns object of result set(UC2)
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //Will Loop until rows are null
                        while (reader.Read())
                        {
                            model.EmployeeId = Convert.ToInt32(reader["Id"] == DBNull.Value ? default : reader["Id"]);
                            model.EmployeeName = reader["Name"] == DBNull.Value ? default : reader["Name"].ToString();
                            model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"] == DBNull.Value ? default : reader["PhoneNumber"]);
                            model.StartDate = (DateTime)(reader["StartDate"] == DBNull.Value ? default(DateTime) : reader["StartDate"]);
                            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
                            model.Address = reader["Address"] == DBNull.Value ? default : reader["Address"].ToString();
                            model.Department = reader["Department"] == DBNull.Value ? default : reader["Department"].ToString();
                            model.BasicPay = Convert.ToDouble(reader["BasicPay"] == DBNull.Value ? default : reader["BasicPay"]);
                            model.TaxablePay = Convert.ToDouble(reader["TaxablePay"] == DBNull.Value ? default : reader["TaxablePay"]);
                            model.IncomeTax = Convert.ToDouble(reader["IncomeTax"] == DBNull.Value ? default : reader["IncomeTax"]);
                            model.Deductions = Convert.ToDouble(reader["Deductions"] == DBNull.Value ? default : reader["Deductions"]);
                            model.NetPay = Convert.ToDouble(reader["NetPay"] == DBNull.Value ? default : reader["NetPay"]);
                            Console.WriteLine(model);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no records in the db table");
                    }
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

        //Method to fetch all records using sqlDataAdapter(UC2)
        public static void GetAllEmployeesUsingDataAdapter()
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    //Created the object of dataset class
                    DataSet dataSet = new DataSet();
                    //Using Stored procedure query to retreive data
                    SqlDataAdapter adapter = new SqlDataAdapter("dbo.ViewAllEmployee", sqlConnection);
                    //Open Sql Connection
                    sqlConnection.Open();
                    adapter.Fill(dataSet);
                    foreach (DataRow data in dataSet.Tables[0].Rows)
                    {
                        Console.WriteLine($"Id : {data["id"]} || Name : {data["Name"]} || PhoneNo : {data["PhoneNumber"]} || Address : {data["Address"]}" +
                            $" || StartDate : {data["StartDate"]} || Gender : {data["Gender"]} || Department : {data["Department"]} || Basic Pay : {data["BasicPay"]}" +
                            $" || Deductions : {data["Deductions"]} || Taxable Pay : {data["TaxablePay"]} || Income Tax : {data["IncomeTax"]} || Net Pay : {data["NetPay"]}\n");
                    }
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

        //Method to add employee details into the database(UC3)
        public static void AddEmployeeIntoDB(EmployeeModel employee)
        {
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("dbo.spAddEmployee", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", employee.EmployeeName);
                sqlCommand.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                sqlCommand.Parameters.AddWithValue("@StartDate", employee.StartDate);
                sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                sqlCommand.Parameters.AddWithValue("@Department", employee.Department);
                sqlCommand.Parameters.AddWithValue("@PhoneNum", employee.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Address", employee.Address);
                sqlCommand.Parameters.AddWithValue("@Deductions", employee.Deductions);
                sqlCommand.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                sqlCommand.Parameters.AddWithValue("@IncomeTax", employee.IncomeTax);
                sqlCommand.Parameters.AddWithValue("@NetPay", employee.NetPay);
                sqlConnection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                    Console.WriteLine("Data Is Added Succesfully");
                else
                    Console.WriteLine("Unsuccesfull Operation");
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
