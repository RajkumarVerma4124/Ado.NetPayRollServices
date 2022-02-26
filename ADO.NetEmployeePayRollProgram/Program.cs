using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayRollProgram
{
    public class Program
    {
        //Entry of the main program
        public static void Main(string[] args)
        {
            //Displaying the welcome message
            Console.WriteLine("Welcome TO The ADO.Net Employee Payroll Sercive Program");
            try 
            {
                while (true)
                {
                    Console.WriteLine("1: Get All Employee \n2: Get All Employee Using Sql Adapter \n3: Add Employee Details Into DB\n4: Exit");
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
                                //Creating the object for employee model to add value into db(UC3)
                                EmployeeModel model = new EmployeeModel();
                                var empModel = AddEmployeeData.AddData(model);
                                EmployeeRepository.AddEmployeeIntoDB(empModel);
                                break;
                            case 4:
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
