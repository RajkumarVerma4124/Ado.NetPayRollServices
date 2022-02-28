using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayRollProgram
{
    public class EmployeeER
    {
        //Creating a static variable
        public static string result;

        //Entry of the main program
        public static void EntityRelationEmployee()
        {
            //Displaying the welcome message
            Console.WriteLine("\nWelcome TO The ADO.Net ER Employee Payroll Service Program");
            //Creating the object for employee model
            EmployeeModel model = new EmployeeModel();
            try
            {
                while (true)
                {
                    Console.WriteLine("1: Get All Employee \n2: Update Salary Of Employee \n3: Find Employee Using Name \n4: Find Employee Using Start Date"+
                                      "\n5: Find Employee Using Income Range \n6: Aggregate Functions  \n7: Add Data Into Multiple Tables \n8: Go Back");
                    Console.Write("Enter a choice from above : ");
                    bool flag = int.TryParse(Console.ReadLine(), out int choice);
                    if (flag)
                    {
                        switch (choice)
                        {
                            case 1:
                                //Calling the employee method to print all the record from the database(UC1&UC2)
                                EmployeeERRepository.GetAllEREmployees();
                                break;
                            case 2:
                                //Adding the new salary for employee to update in db(UC4)
                                Console.Write("Enter The Id Of the Employee To Update Salary : ");
                                model.EmployeeId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter The Name Of The Employee : ");
                                model.EmployeeName = Console.ReadLine();
                                Console.Write("Enter The Updated Salary Of The Employee : ");
                                model.BasicPay = Convert.ToDouble(Console.ReadLine());
                                result = EmployeeERRepository.UpdateEREmpSalary(model);
                                Console.WriteLine(result);
                                break;
                            case 3:
                                //Retrieving the employee record by using name from db(UC5)
                                Console.Write("Enter The Name Of The Employee : ");
                                model.EmployeeName = Console.ReadLine();
                                result = EmployeeERRepository.GetEREmployeesUsingName(model);
                                Console.WriteLine(result);
                                break;
                            case 4:
                                //Retrieving the employee record by using date range from db(UC5)
                                Console.Write("Enter The Starting Date Of Joining For Employee In yyyy-mm-dd Format: ");
                                model.StartDate = Convert.ToDateTime(Console.ReadLine());
                                result = EmployeeERRepository.GetEREmployeesUsingDateRange(model);
                                Console.WriteLine(result);
                                break;
                            case 5:
                                //Retrieving the employee record by using income range from db(UC5)
                                Console.Write("Enter The Start Range Salary Of The Employee : ");
                                double startIncome = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Enter The End Range Salary Of The Employee : ");
                                double endIncome = Convert.ToDouble(Console.ReadLine());
                                result = EmployeeERRepository.GetEREmployeesUsingIncomeRange(model, startIncome, endIncome);
                                Console.WriteLine(result);
                                break;
                            case 6:
                                //Retrieving the aggregate function values from db(UC6)
                                Console.Write("Enter The Gender Of The Employee : ");
                                char gender = Convert.ToChar(Console.ReadLine());
                                result = EmployeeERRepository.AggregateErFunctionsByGender(model, gender);
                                Console.WriteLine(result);
                                break;
                            case 7:
                                //Inserting the data into multiple table using transaction(UC10)
                                Console.Write("Enter The Company Id From 1 To 3 : ");
                                model.CompanyId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter The Name Of The Employee : ");
                                model.EmployeeName = Console.ReadLine();
                                Console.Write("Enter The Starting Date Of Joining For Employee In yyyy-mm-dd Format: ");
                                model.StartDate = Convert.ToDateTime(Console.ReadLine());
                                Console.Write("Enter The Gender Of The Employee : ");
                                model.Gender = Convert.ToChar(Console.ReadLine());
                                Console.Write("Enter The Phone Number Of The Employee : ");
                                model.PhoneNumber = Convert.ToInt64(Console.ReadLine());
                                Console.Write("Enter The Address Of The Employee : ");
                                model.Address = Console.ReadLine();
                                Console.Write("Enter The Salary Of The Employee : ");
                                model.BasicPay = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Enter The Deduction Amount Of The Employee : ");
                                model.Deductions = Convert.ToDouble(Console.ReadLine());
                                model.TaxablePay = model.BasicPay - model.Deductions;
                                Console.Write("Enter The IncomeTax Amount Of The Employee : ");
                                model.IncomeTax = Convert.ToDouble(Console.ReadLine());
                                model.NetPay = model.TaxablePay - model.IncomeTax;
                                result = PayRollTransactions.InsertDataIntoMulTableUsingTransaction(model);
                                Console.WriteLine(result);
                                break;
                            case 8:
                                //For going back to the main menu
                                Program.Main(null);
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

