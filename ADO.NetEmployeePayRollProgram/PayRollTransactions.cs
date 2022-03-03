using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayRollProgram
{
    /// <summary>
    /// Created The PayRollTransaction Class To Add Data Into Multiple Tables(UC10)
    /// </summary>
    public class PayRollTransactions
    {
        //Declaring Connection String
        public static string ConnectionString = @"Data Source=RAJ-VERMA;Initial Catalog=PayRoll_Service;Integrated Security=True;";
        //SqlConnection
        public static SqlConnection sqlConnection = null;
        //Declaring list object
        public static List<EmployeeModel> employeePayrollDetailsList = new List<EmployeeModel>();

        //Method to insert data into multiple tables(UC10)
        public static string InsertDataIntoMulTableUsingTransaction(EmployeeModel model)
        {
            using (sqlConnection = new SqlConnection(ConnectionString))
            {
                //Open the connection
                sqlConnection.Open();
                //Start a local transactions
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                //Enlist a command int the current transaction
                SqlCommand command = sqlConnection.CreateCommand();
                //Setting the command to transaction
                command.Transaction = sqlTransaction;
                try
                {
                    //Executing different commands objects
                    command = new SqlCommand("dbo.spInsertDataIntoEmployee", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@CompanyId", model.CompanyId);
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@EmployeeAddress", model.Address);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Transaction = sqlTransaction;
                    var result = command.ExecuteScalar();   
                    int newId = Convert.ToInt32(command.Parameters["@id"].Value.ToString());

                    command = new SqlCommand("dbo.spInsertDataIntoPayroll", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", newId);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", model.IncomeTax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Transaction = sqlTransaction;
                    var newResult = command.ExecuteNonQuery();
                    if (newResult != 0)
                    {
                        //if all executes are success commit the transaction
                        sqlTransaction.Commit();
                        return $"Inserted The Data Successfully";
                    }
                    else
                        return $"Unsucesfull";
                }
                catch (Exception ex)
                {
                    //Handle the eception if the transaction fails to commit
                    Console.WriteLine(ex.Message);
                    try
                    {
                        //Attempt to rollback the transaction
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollBack)
                    {
                        return exRollBack.Message;
                    }
                    return default;
                }
            }
        }

        //Method to delte data from multiple tables(UC11)
        public static string DeleteDataFromMulTableUsingCascade(EmployeeModel model)
        {
            using (sqlConnection = new SqlConnection(ConnectionString))
            {
                //Open the connection
                sqlConnection.Open();
                //Start a local transactions
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                //Enlist a command int the current transaction
                SqlCommand command = sqlConnection.CreateCommand();
                //Setting the command to transaction
                command.Transaction = sqlTransaction;
                try
                {
                    //Executing different commands objects
                    command = new SqlCommand("dbo.spDeleteDataFromEmployee", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Transaction = sqlTransaction;
                    var result = command.ExecuteNonQuery();

                    //Deleting payroll table
                    string query = $"DELETE FROM Payroll WHERE EmployeeId='{model.EmployeeId}'";
                    command = new SqlCommand(query, sqlConnection);
                    command.Transaction = sqlTransaction;
                    var newResult = command.ExecuteNonQuery();
                    if (newResult != 0 && result != 0)
                    {
                        //if all executes are success commit the transaction
                        sqlTransaction.Commit();
                        return $"Deleted The Data Successfully";
                    }
                    else
                        return $"Unsucesfull";
                }
                catch (Exception ex)
                {
                    //Handle the eception if the transaction fails to commit
                    Console.WriteLine(ex.Message);
                    try
                    {
                        //Attempt to rollback the transaction
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollBack)
                    {
                        return exRollBack.Message;
                    }
                    return default;
                }
            }
        }

        //Method to add a column in the employee table(UC12)
        public static string AddIsActiveColumn()
        {
            using (sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                //Begins sql transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Add column IsActive in Employee
                    string query = $"Alter table Employee add IsActive varchar(10) NOT NULL default 'True';";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    var result = sqlCommand.ExecuteNonQuery();
                    if(result != 0)
                    {
                        sqlTransaction.Commit();
                        return "Added the coulumn successfully";
                    }
                    else
                    {
                        return "Unsuccessfull";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        //Rollback to the point before exception
                        sqlTransaction.Rollback();
                    }
                    catch(Exception exRollBack)
                    {
                        return exRollBack.Message;
                    }
                }
            }
            return default;
        }

        //Method to create audit list(UC12)
        public static string AuditList(EmployeeModel model)
        {
            using (sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                //Begins sql transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand command = sqlConnection.CreateCommand();
                command.Transaction = sqlTransaction;
                try
                {
                    //Deleting payroll table employee
                    string delQuery = $"DELETE FROM Payroll WHERE EmployeeId='{model.EmployeeId}'";
                    command = new SqlCommand(delQuery, sqlConnection);
                    command.Transaction = sqlTransaction;
                    var result = command.ExecuteNonQuery();

                    //Setting The IsActive = False after deleting the employee payroll object
                    string udQuery = $"UPDATE Employee SET IsActive = 'False' WHERE EmployeeId={model.EmployeeId};";
                    command = new SqlCommand(udQuery, sqlConnection);
                    command.Transaction = sqlTransaction;
                    var newResult = command.ExecuteNonQuery();
                    if (newResult !=0 && result != 0 )
                    {
                        sqlTransaction.Commit();
                        return "Delete And Update Operation performed successfully";
                    }
                    else
                    {
                        return "Unsucessfull";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        //Rollback to the point before exception
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollBack)
                    {
                        return exRollBack.Message;
                    }
                }
            }
            return default;
        }

        //Method to add mul employee into db table without using thread(UC13)
        public static string AddMulEmployeeToPayrollWithoutThread(List<EmployeeModel> employeePayroll)
        {
            try
            {
                employeePayroll.ForEach(employeeData =>
                {
                    //object for stopwatch
                    Stopwatch stopWatch = new Stopwatch();
                    //start the stopwatch
                    stopWatch.Start();
                    Console.WriteLine("Employee Being Added" + employeeData.EmployeeName);
                    InsertDataIntoMulTableUsingTransaction(employeeData);
                    Console.WriteLine("Employee Added Into Db" + employeeData.EmployeeName);
                    //stop stopwatch
                    stopWatch.Stop();
                    double elapsedTime = Math.Round((double)stopWatch.ElapsedMilliseconds / 1000, 2);
                    Console.WriteLine($"Duration Without Thread : {elapsedTime} milliseconds");
                });
                return $"Successfull";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }         
        }
    }
}
