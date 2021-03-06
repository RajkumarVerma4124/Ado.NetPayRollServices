using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayRollProgram
{
    public class Program
    {
        //Creating a static variable
        public static string result;

        //Entry of the main program
        public static void Main(string[] args)
        {
            //Displaying the welcome message
            Console.WriteLine("Welcome TO The ADO.Net Employee Payroll Service Program");
            //Creating the object for employee model
            EmployeeModel model = new EmployeeModel();
            try
            {
                while (true)
                {
                    Console.WriteLine("1: Get All Employee \n2: Get All Employee Using Sql Adapter \n3: Add Employee Details Into DB \n4: Update Salary Of Employee \n5: Find Employee Using Name"+
                                    "\n6: Find Employee Using Start Date \n7: Find Employee Using Income Range \n8: Aggregate Functions \n9: Delete Single Record \n10: Employee Table Using Er Diagram \n11: Exit");
                    Console.Write("Enter a choice from above : ");
                    bool flag = int.TryParse(Console.ReadLine(), out int choice);
                    if (flag)
                    {
                        switch (choice)
                        {
                            case 1:
                                //Calling the employee method to print all the record from the database(UC1&UC2)
                                EmployeeRepository.GetAllEmployees();
                                break;
                            case 2:
                                //Calling the employee using adapter method to print all the record from the database(UC1&UC2)
                                EmployeeRepository.GetAllEmployeesUsingDataAdapter();
                                break;
                            case 3:
                                //Using the object of employee model to add value into db(UC3)
                                var empModel = AddEmployeeData.AddData(model);
                                EmployeeRepository.AddEmployeeIntoDB(empModel);
                                break;
                            case 4:
                                //Adding the new salary for employee to update in db(UC4)
                                Console.Write("Enter The Id Of the Employee To Update Salary : ");
                                model.EmployeeId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter The Name Of The Employee : ");
                                model.EmployeeName = Console.ReadLine();
                                Console.Write("Enter The Updated Salary Of The Employee : ");
                                model.BasicPay = Convert.ToDouble(Console.ReadLine());
                                result = EmployeeRepository.UpdateEmpSalary(model);
                                Console.WriteLine(result);
                                break;
                            case 5:
                                //Retrieving the employee record by using name from db(UC5)
                                Console.Write("Enter The Name Of The Employee : ");
                                model.EmployeeName = Console.ReadLine();
                                result = EmployeeRepository.GetEmployeesUsingName(model);
                                Console.WriteLine(result);
                                break;
                            case 6:
                                //Retrieving the employee record by using date range from db(UC5)
                                Console.Write("Enter The Starting Date Of Joining For Employee In yyyy-mm-dd Format: ");
                                model.StartDate = Convert.ToDateTime(Console.ReadLine());
                                result = EmployeeRepository.GetEmployeesUsingDateRange(model);
                                Console.WriteLine(result);
                                break;
                            case 7:
                                //Retrieving the employee record by using income range from db(UC5)
                                Console.Write("Enter The Start Range Salary Of The Employee : ");
                                double startIncome = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Enter The End Range Salary Of The Employee : ");
                                double endIncome = Convert.ToDouble(Console.ReadLine());
                                result = EmployeeRepository.GetEmployeesUsingIncomeRange(model, startIncome, endIncome);
                                Console.WriteLine(result);
                                break;
                            case 8:
                                //Retrieving the aggregate function values from db(UC6)
                                Console.Write("Enter The Gender Of The Employee : ");
                                char gender  = Convert.ToChar(Console.ReadLine());                                
                                result = EmployeeRepository.AggregateFunctionsByGender(model, gender);
                                Console.WriteLine(result);
                                break;
                            case 9:
                                //Deleting a single recorde from db table(UC6)
                                Console.Write("Enter The Id Of The Employee : ");
                                model.EmployeeId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter The Name Of The Employee : ");
                                model.EmployeeName = Console.ReadLine();
                                result = EmployeeRepository.DeleteSingleRecord(model);
                                Console.WriteLine(result);
                                break;
                            case 10:
                                //Implmenting the er diagram table
                                EmployeeER.EntityRelationEmployee();
                                break;
                            case 11:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Enter right choice");
                                continue;
                        }
                    }
                    else
                        Console.WriteLine("Enter some choice");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
