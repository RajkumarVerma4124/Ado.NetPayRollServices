using System;
using System.Collections.Generic;
using System.Text;
namespace ADO.NetEmployeePayRollProgram
{
    /// <summary>
    /// Created Class For Getting And Setting Database Values
    /// </summary>
    public class EmployeeModel
    {
        //Declaring properties to get the database values
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public double BasicPay { get; set; }
        public double TaxablePay { get; set; }
        public double IncomeTax { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }
    }
}