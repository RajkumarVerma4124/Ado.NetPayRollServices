using System;
using System.Collections.Generic;
using System.Text;
namespace ADO.NetEmployeePayRollProgram
{
    /// <summary>
    /// Created Class For Declaring Properties To Get And Set Database Values(UC1 & UC2)
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
        public int DepartmentId { get; set; }
        public double BasicPay { get; set; }
        public double TaxablePay { get; set; }
        public double IncomeTax { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }

        //Override the string method
        public override string ToString()
        {
            return $"Employee Details For {EmployeeName} Is Listed Below : \nCompany Id : {CompanyId} \tCompany Name : {CompanyName} \tId : {EmployeeId} \tName : {EmployeeName} \tPhoneNo : {PhoneNumber} \tStartDate : {StartDate} \tGender : {Gender}"+
                 $"\nAddress : {Address} \tDepartment Id : {DepartmentId} \tDepartment Name : {Department} \tBasic Pay : {BasicPay} \tDeductions : {Deductions} \tTaxable Pay : {TaxablePay} \tIncome Tax : {IncomeTax} \tNet Pay : {NetPay}\n";
        }
    }
}