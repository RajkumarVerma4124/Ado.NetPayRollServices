/*using ADO.NetEmployeePayRollProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ADo.NetEmpPayRollTesting
{
    [TestClass]
    public class EmployeePayRollTest
    {
        //Testing the update method of employee to check if its updated successfully or not(UC4-TC4.1)
        [TestMethod]
        [DataRow(1, "Raj", 45000,"Data Updated Succesfully")]
        [DataRow(2, "Raj", 42000, "Update Unsuccessfull")]
        public void GivenSPUpdateEmpRecords(int id, string name, float basicPay, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = id;
            employeeModel.EmployeeName = name;
            employeeModel.BasicPay = basicPay;
            string actual = EmployeeRepository.UpdateEmpSalary(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the update method of er employee to check if its updated successfully or not(UC7ToUC9)
        [TestMethod]
        [DataRow(1, "Rajkumar", 42000, "Data Updated Succesfully")]
        [DataRow(2, "Raj", 42000, "Update Unsuccessfull")]
        public void GivenSPUpdateEREmpRecords(int id, string name, float basicPay, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = id;
            employeeModel.EmployeeName = name;
            employeeModel.BasicPay = basicPay;
            string actual = EmployeeERRepository.UpdateEREmpSalary(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the Delete method of employee to check if its delted successfully or not(UC4-TC4.2)
        [TestMethod]
        [DataRow(10, "Sachin", "Data Deleted Succesfully")]
        [DataRow(14, "Kumar", "Unsuccessfull")]
        public void GivenSPReturnDeletedResult(int id, string name, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = id;
            employeeModel.EmployeeName = name;
            string actual = EmployeeRepository.DeleteSingleRecord(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the get all employee and er employee method to check if its employ data is retrieved or not using name(UC5-TC5.1 && UC7ToUC9)
        [TestMethod]
        [DataRow("Omkar","Found The Data With Given Name")]
        [DataRow("Abc","No Records Founds")]
        public void GivenSPReturnEmpDataResultUsingName(string name, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeName = name;
            string actualEmployee = EmployeeRepository.GetEmployeesUsingName(employeeModel);
            string actualErEmployee = EmployeeERRepository.GetEREmployeesUsingName(employeeModel);
            Assert.AreEqual(actualEmployee, expected);
            Assert.AreEqual(actualErEmployee, expected);
        }

        //Testing the get all employee and er employee method  to check if its employ data is retrieved or not using date(UC5-TC5.2 && UC7ToUC9)
        [TestMethod]
        [DataRow("2019-01-01", "Found The Data With Given Date")]
        [DataRow("2023-01-01", "No Records Found")]
        public void GivenSPReturnEmpDataResultUsingDate(string date, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.StartDate = Convert.ToDateTime(date);
            string actualEmployee = EmployeeRepository.GetEmployeesUsingDateRange(employeeModel);
            string actualErEmployee = EmployeeERRepository.GetEREmployeesUsingDateRange(employeeModel);
            Assert.AreEqual(actualEmployee, expected);
            Assert.AreEqual(actualErEmployee, expected);
        }

        //Testing the get all employee and er employee method  to check if its employ data is retrieved or not using income(UC5-TC5.3 && UC7ToUC9)
        [TestMethod]
        [DataRow(25000, 50000,"Found The Data With Given Income Range")]
        [DataRow(3000000,600000, "No Records Found")]
        public void GivenSPReturnEmpDataResultUsingIncomeRange(double startRangeInc, double endRangeInc, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            string actualEmployee = EmployeeRepository.GetEmployeesUsingIncomeRange(employeeModel, startRangeInc, endRangeInc);
            string actualErEmployee = EmployeeERRepository.GetEREmployeesUsingIncomeRange(employeeModel, startRangeInc, endRangeInc);
            Assert.AreEqual(actualEmployee, expected);
            Assert.AreEqual(actualErEmployee, expected);
        }

        //Testing the aggregate and er aggregate method to check if data is recieved or not(UC6-TC6.1 && UC7ToUC9)
        [TestMethod]
        [DataRow('M', "Found The Data Successfully")]
        [DataRow('F', "Found The Data Successfully")]
        [DataRow('A', "No Records Found")]
        public void GivenSPReturnAggregateFunctionsResult(char gender, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            string actualEmployee = EmployeeRepository.AggregateFunctionsByGender(employeeModel,gender);
            string actualErEmployee = EmployeeERRepository.AggregateErFunctionsByGender(employeeModel, gender);
            Assert.AreEqual(actualEmployee, expected);
            Assert.AreEqual(actualErEmployee, expected);
        }

        //Testing the insert data into mul tables method to check if it is added successfully or not(UC10-TC10.1)
        [TestMethod]
        [DataRow(2, "Yash", "2020-10-21", 'M', 9658740123, "Govandi", 43500, 999.99, 4999.99, "Inserted The Data Successfully")]
        public void GivenSPReturnAddedResult(int companyId, string name, string date, char gender, long phoneNum, string addr, double basicPay, double dedcution, double incTax, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.CompanyId = companyId;
            employeeModel.EmployeeName = name;
            employeeModel.StartDate = Convert.ToDateTime(date);
            employeeModel.Gender = gender;
            employeeModel.PhoneNumber = phoneNum;
            employeeModel.Address = addr;
            employeeModel.BasicPay = basicPay;
            employeeModel.Deductions = dedcution;
            employeeModel.TaxablePay = employeeModel.BasicPay - employeeModel.Deductions;
            employeeModel.IncomeTax = incTax;
            employeeModel.NetPay = employeeModel.TaxablePay - employeeModel.IncomeTax;
            string actual = PayRollTransactions.InsertDataIntoMulTableUsingTransaction(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the delete data from mul tables method to check if it is deleted successfully or not(UC11-TC11.1)
        [TestMethod]
        [DataRow(78, "Raj", "Deleted The Data Successfully")]
        [DataRow(79, "Amit", "Deleted The Data Successfully")]
        [DataRow(80, "Jerin", "Deleted The Data Successfully")]
        [DataRow(81, "Abhishek", "Deleted The Data Successfully")]
        [DataRow(82, "Mahipal", "Deleted The Data Successfully")]
        [DataRow(82, "Mahipal", "Unsucesfull")]
        public void GivenSPReturnCascadeDeletedResult(int employeeId, string name, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = employeeId;
            employeeModel.EmployeeName = name;
            string actual = PayRollTransactions.DeleteDataFromMulTableUsingCascade(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the auditlist method to check if it is updated successfully or not(UC11-TC11.1)
        [TestMethod]
        [DataRow(25, "Delete And Update Operation performed successfully")]
        [DataRow(25, "Unsucessfull")]
        public void GivenSPReturnAuditListResult(int employeeId, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = employeeId;
            string actual = PayRollTransactions.AuditList(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the insert method without using thread(UC13-TC13.1)
        [TestMethod]
        [DataRow("Successfull")]
        public void GivenMulEmployeeAddToDbWithoutThread(string expected)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>()
             {
                 new EmployeeModel { CompanyId = 1, EmployeeName = "Raj", StartDate = Convert.ToDateTime("2020-10-21"), Gender = 'M', PhoneNumber = 9658740123, Address = "Vashi", BasicPay = 41500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 39500.02},
                 new EmployeeModel { CompanyId = 2, EmployeeName = "Amit", StartDate = Convert.ToDateTime("2019-12-25"), Gender = 'M', PhoneNumber = 9654780123, Address = "Govandi", BasicPay = 42500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 40500.01},
                 new EmployeeModel { CompanyId = 3, EmployeeName = "Jerin", StartDate = Convert.ToDateTime("2018-10-11"), Gender = 'M', PhoneNumber = 9658741233, Address = "Airoli", BasicPay = 44500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 42500.01},
                 new EmployeeModel { CompanyId = 1, EmployeeName = "Raj", StartDate = Convert.ToDateTime("2020-10-21"), Gender = 'M', PhoneNumber = 9658470123, Address = "Powai", BasicPay = 47500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 45500.01},
                 new EmployeeModel { CompanyId = 2, EmployeeName = "Mahipal", StartDate = Convert.ToDateTime("2019-05-15"), Gender = 'M', PhoneNumber = 9678540123, Address = "Nerul", BasicPay = 49500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 47500.01},
             };
            var actual = PayRollTransactions.AddMulEmployeeToPayrollWithoutThread(employees);
            Assert.AreEqual(expected, actual);
        }

        //Testing the insert method with using thread and synchronization(UC14-TC14.1&&UC15-TC15.1)
        [TestMethod]
        [DataRow("Successfull")]
        public void GivenMulEmpAddUsingThreadAndSynchronization(string expected)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>()
             {
                 new EmployeeModel { CompanyId = 1, EmployeeName = "Raj", StartDate = Convert.ToDateTime("2020-10-21"), Gender = 'M', PhoneNumber = 9658740123, Address = "Vashi", BasicPay = 41500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 39500.02},
                 new EmployeeModel { CompanyId = 2, EmployeeName = "Amit", StartDate = Convert.ToDateTime("2019-12-25"), Gender = 'M', PhoneNumber = 9654780123, Address = "Govandi", BasicPay = 42500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 40500.01},
                 new EmployeeModel { CompanyId = 3, EmployeeName = "Jerin", StartDate = Convert.ToDateTime("2018-10-11"), Gender = 'M', PhoneNumber = 9658741233, Address = "Airoli", BasicPay = 44500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 42500.01},
                 new EmployeeModel { CompanyId = 1, EmployeeName = "Abhishek", StartDate = Convert.ToDateTime("2020-10-21"), Gender = 'M', PhoneNumber = 9658470123, Address = "Powai", BasicPay = 47500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 45500.01},
                 new EmployeeModel { CompanyId = 2, EmployeeName = "Mahipal", StartDate = Convert.ToDateTime("2019-05-15"), Gender = 'M', PhoneNumber = 9678540123, Address = "Nerul", BasicPay = 49500, Deductions = 999.99,IncomeTax = 4999.99, TaxablePay = 41500-999.99, NetPay = 47500.01},
             };
            var actual = PayRollTransactions.AddMulEmployeeToPayrollUsingThread(employees);
            Assert.AreEqual(expected, actual);
        }

        //Testing the update method of mul employee to check if its updated successfully or not(UC17-TC17.1)
        [TestMethod]
        [DataRow("Successfull")]
        public void GivenSPUpdateMulEREmpRecords(string expected)
        {
             List<EmployeeModel> employees = new List<EmployeeModel>()
             {
                 new EmployeeModel { EmployeeId = 84, EmployeeName = "Raj", BasicPay = 49500},
                 new EmployeeModel { EmployeeId = 85, EmployeeName = "Amit", BasicPay = 44500},
                 new EmployeeModel { EmployeeId = 86, EmployeeName = "Jerin", BasicPay = 43500},
                 new EmployeeModel { EmployeeId = 87, EmployeeName = "Abhishek", BasicPay = 42500},
                 new EmployeeModel { EmployeeId = 88, EmployeeName = "Mahipal", BasicPay = 41500},
             };
            string actual = PayRollTransactions.UpdateMulEmpSalaryUsingThread(employees);
            Assert.AreEqual(actual, expected);
        }
    }
}
*/