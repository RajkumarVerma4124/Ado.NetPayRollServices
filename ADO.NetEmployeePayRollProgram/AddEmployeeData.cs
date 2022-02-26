using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayRollProgram
{
    /// <summary>
    /// Created The Class For Adding The Employee Data(UC3)
    /// </summary>
    public class AddEmployeeData
    {
        //Method for adding data
        public static EmployeeModel AddData(EmployeeModel employee)
        {
            Console.Write("Enter The Name Of The Employee : ");
            employee.EmployeeName = Console.ReadLine();
            Console.Write("Enter The Salary Of The Employee : ");
            employee.BasicPay = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter The Starting Date Of Joining For Employee In yyyy-mm-dd Format: ");
            employee.StartDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter The Gender Of The Employee : ");
            employee.Gender = Convert.ToChar(Console.ReadLine());
            Console.Write("Enter The Department Of The Employee : ");
            employee.Department = Console.ReadLine();
            Console.Write("Enter The Phone Number Of The Employee : ");
            employee.PhoneNumber = Convert.ToInt64(Console.ReadLine());
            Console.Write("Enter The Address Of The Employee : ");
            employee.Address = Console.ReadLine();
            Console.Write("Enter The Deduction Amount Of The Employee : ");
            employee.Deductions = Convert.ToDouble(Console.ReadLine());           
            employee.TaxablePay = employee.BasicPay-employee.Deductions;
            Console.Write("Enter The IncomeTax Amount Of The Employee : ");
            employee.IncomeTax = Convert.ToDouble(Console.ReadLine());
            employee.NetPay = employee.TaxablePay - employee.IncomeTax;
            return employee;
        }
    }
}
