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
    /// Created The Employee ER Repository Class To Check The DB Connectivity And Fetch & Update Records(UC7ToUC9)
    /// </summary>
    public class EmployeeERRepository
    {
        //Declaring Connection String
        public static string ConnectionString = @"Data Source=RAJ-VERMA;Initial Catalog=PayRoll_Service;Integrated Security=True;";
        //SqlConnection
        public static SqlConnection sqlConnection = null;
        //Creating Object Of Employee Model
        public static EmployeeModel model = new EmployeeModel();

        //Method to fetch all employee records using er diagram(UC7ToUC9)
        public static void GetAllEREmployees()
        {
            try
            { 
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    //Stored Procedure to retreive data
                    SqlCommand command = new SqlCommand("dbo.spGetAllErEmployee", sqlConnection);
                    //Open Sql Connection
                    sqlConnection.Open();
                    //Returns object of result set
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            PrintEmpDetails(reader, model);
                    }
                    else
                        Console.WriteLine("There is no records in the db table");
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

        //Method to update er employee salary from the db(UC7ToUC9)
        public static string UpdateEREmpSalary(EmployeeModel model)
        {
            int result = 0;
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUpdateERSalary", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                    sqlCommand.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    sqlConnection.Open();
                    result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                        return "Data Updated Succesfully";
                    else
                        return "Update Unsuccessfull";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Method to take values from db using sql data reader to model object(UC7ToUC9)
        public static void PrintEmpDetails(SqlDataReader reader, EmployeeModel model)
        {
            model.CompanyId = Convert.ToInt32(reader["CompanyID"] == DBNull.Value ? default : reader["CompanyID"]);
            model.CompanyName = reader["CompanyName"] == DBNull.Value ? default : reader["CompanyName"].ToString();
            model.EmployeeId = Convert.ToInt32(reader["EmployeeId"] == DBNull.Value ? default : reader["EmployeeId"]);
            model.EmployeeName = reader["EmployeeName"] == DBNull.Value ? default : reader["EmployeeName"].ToString();
            model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"] == DBNull.Value ? default : reader["PhoneNumber"]);
            model.StartDate = (DateTime)(reader["StartDate"] == DBNull.Value ? default(DateTime) : reader["StartDate"]);
            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
            model.Address = reader["EmployeeAddress"] == DBNull.Value ? default : reader["EmployeeAddress"].ToString();
            model.DepartmentId = Convert.ToInt32(reader["DepartmentId"] == DBNull.Value ? default : reader["DepartmentId"]);
            model.Department = reader["DepartmentName"] == DBNull.Value ? default : reader["DepartmentName"].ToString();
            model.BasicPay = Convert.ToDouble(reader["BasicPay"] == DBNull.Value ? default : reader["BasicPay"]);
            model.TaxablePay = Convert.ToDouble(reader["TaxablePay"] == DBNull.Value ? default : reader["TaxablePay"]);
            model.IncomeTax = Convert.ToDouble(reader["IncomeTax"] == DBNull.Value ? default : reader["IncomeTax"]);
            model.Deductions = Convert.ToDouble(reader["Deductions"] == DBNull.Value ? default : reader["Deductions"]);
            model.NetPay = Convert.ToDouble(reader["NetPay"] == DBNull.Value ? default : reader["NetPay"]);
            Console.WriteLine(model);
        }

        //Method to take values from er db using sql data reader to model object(UC7ToUC9)
        public static void PrintErEmpDetails(SqlDataReader reader, EmployeeModel model)
        {
            //Printing deatails that are retrived
            model.CompanyId = Convert.ToInt32(reader["CompanyID"] == DBNull.Value ? default : reader["CompanyID"]);
            model.CompanyName = reader["CompanyName"] == DBNull.Value ? default : reader["CompanyName"].ToString();
            model.EmployeeId = Convert.ToInt32(reader["EmployeeId"] == DBNull.Value ? default : reader["EmployeeId"]);
            model.EmployeeName = reader["EmployeeName"] == DBNull.Value ? default : reader["EmployeeName"].ToString();
            model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"] == DBNull.Value ? default : reader["PhoneNumber"]);
            model.StartDate = (DateTime)(reader["StartDate"] == DBNull.Value ? default(DateTime) : reader["StartDate"]);
            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
            model.IsActive = reader["IsActive"] == DBNull.Value ? default : reader["IsActive"].ToString();
            Console.WriteLine($"Employee Details For {model.EmployeeName} Is Listed Below : \nCompany Id : {model.CompanyId} \tCompany Name : {model.CompanyName} \tId : {model.EmployeeId}" +
                $" \tName : {model.EmployeeName} \tPhoneNo : {model.PhoneNumber} \tStartDate : {model.StartDate} \tGender : {model.Gender} \tIsActive : {model.IsActive}");
        }

        //Method to fetch all records from er employee db using the given name(UC7ToUC9)
        public static string GetEREmployeesUsingName(EmployeeModel model)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.spGetEREmpUsingName", sqlConnection);
                    //Setting command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    //Add parameters to stored procedures
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    //Open the connection
                    sqlConnection.Open();
                    //Sql data reader- using execute reader returns object for resultset
                    SqlDataReader reader = command.ExecuteReader();
                    //checking result set has rows are not
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PrintErEmpDetails(reader, model);
                        }
                        return "Found The Data With Given Name";
                    }
                    else
                        return "No Records Founds";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Method to fetch all records from er employee db by checking is active(UC12)
        public static string GetEREmployeesByCheckingIsActive(EmployeeModel model)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.spGetEREmpByCheckingActive", sqlConnection);
                    //Setting command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    //Add parameters to stored procedures
                    command.Parameters.AddWithValue("@IsActive", model.IsActive);
                    //Open the connection
                    sqlConnection.Open();
                    //Sql data reader- using execute reader returns object for resultset
                    SqlDataReader reader = command.ExecuteReader();
                    //checking result set has rows are not
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PrintErEmpDetails(reader, model);
                        }
                        return "Found The Data";
                    }
                    else
                        return "No Records Founds";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Method to fetch all records from er employee db using the given date(UC7ToUC9)
        public static string GetEREmployeesUsingDateRange(EmployeeModel model)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.spGetEREmpUsingDate", sqlConnection);
                    //Setting command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    //Add parameters to stored procedures
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
                    //Open the connection
                    sqlConnection.Open();
                    //Sql data reader- using execute reader returns object for resultset
                    SqlDataReader result = command.ExecuteReader();
                    //checking result set has rows are not
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            PrintErEmpDetails(result, model);
                        }
                        return "Found The Data With Given Date";
                    }
                    else
                    {
                        return "No Records Found";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Method to fetch all records from er employee db using the given income(UC7ToUC9)
        public static string GetEREmployeesUsingIncomeRange(EmployeeModel model, double startIncome, double endIncome)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.spGetEREmpUsingIncome", sqlConnection);
                    //Setting command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    //Add parameters to stored procedures
                    command.Parameters.AddWithValue("@BasicPayStart", startIncome);
                    command.Parameters.AddWithValue("@BasicPayEnd", endIncome);
                    //Open the connection
                    sqlConnection.Open();
                    //Sql data reader- using execute reader returns object for resultset
                    SqlDataReader reader = command.ExecuteReader();
                    //checking result set has rows are not
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Printing deatails that are retrived
                            model.CompanyId = Convert.ToInt32(reader["CompanyID"] == DBNull.Value ? default : reader["CompanyID"]);
                            model.CompanyName = reader["CompanyName"] == DBNull.Value ? default : reader["CompanyName"].ToString();
                            model.EmployeeId = Convert.ToInt32(reader["EmployeeId"] == DBNull.Value ? default : reader["EmployeeId"]);
                            model.EmployeeName = reader["EmployeeName"] == DBNull.Value ? default : reader["EmployeeName"].ToString();
                            model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"] == DBNull.Value ? default : reader["PhoneNumber"]);
                            model.StartDate = (DateTime)(reader["StartDate"] == DBNull.Value ? default(DateTime) : reader["StartDate"]);
                            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
                            model.BasicPay = Convert.ToDouble(reader["BasicPay"] == DBNull.Value ? default : reader["BasicPay"]);
                            Console.WriteLine($"Employee Details For {model.EmployeeName} Is Listed Below : \nCompany Id : {model.CompanyId} \tCompany Name : {model.CompanyName} \tId : {model.EmployeeId}" +
                                $" \tName : {model.EmployeeName} \tPhoneNo : {model.PhoneNumber} \tStartDate : {model.StartDate} \tGender : {model.Gender} \tBasicPay : {model.BasicPay}");
                        }
                        return "Found The Data With Given Income Range";
                    }
                    else
                    {
                        return "No Records Found";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Method to show the aggregate function values from the database
        public static string AggregateErFunctionsByGender(EmployeeModel model, char gender)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.spAggregateErFunction", sqlConnection);
                    //Setting command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    //Add parameters to stored procedures
                    command.Parameters.AddWithValue("@Gender", gender);
                    //Open the connection
                    sqlConnection.Open();
                    //Sql data reader- using execute reader returns object for resultset
                    SqlDataReader result = command.ExecuteReader();
                    //checking result set has rows are not
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            Console.WriteLine($"Total Salary : {result[0]} \nMaximum Salary : {result[1]} \nMinimum Salary : {result[2]} \nAverage Salary = {result[3]}" +
                                $"\nGender : {result[4]} \nCount : {result[5]}");
                        }
                        return "Found The Data Successfully";
                    }
                    else
                    {
                        return "No Records Found";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
