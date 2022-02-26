using ADO.NetEmployeePayRollProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ADo.NetEmpPayRollTesting
{
    [TestClass]
    public class EmployeePayRollTest
    {
        //Testing the update method of employee to check if its updated successfully or not(UC4-TC4.1)
        [TestMethod]
        [DataRow(1, "Raj", 45000,"Data Updated Succesfully")]
        [DataRow(2, "Raj", 42000, "Update Unsuccessfull")]
        public void GivenSPReturnResult(int id, string name, float basicPay, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = id;
            employeeModel.EmployeeName = name;
            employeeModel.BasicPay = basicPay;
            string actual = EmployeeRepository.UpdateEmpSalary(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the get all employee method  to check if its employ data is retrieved or not using name(UC5-TC5.1)
        [TestMethod]
        [DataRow("Raj","Found The Data With Given Name")]
        [DataRow("Abc","No Records Founds")]
        public void GivenSPReturnEmpDataResultUsingName(string name, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeName = name;
            string actual = EmployeeRepository.GetEmployeesUsingName(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the get all employee method  to check if its employ data is retrieved or not using date(UC5-TC5.2)
        [TestMethod]
        [DataRow("2019-01-01", "Found The Data With Given Date")]
        [DataRow("2023-01-01", "No Records Found")]
        public void GivenSPReturnEmpDataResultUsingDate(string date, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.StartDate = Convert.ToDateTime(date);
            string actual = EmployeeRepository.GetEmployeesUsingDateRange(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        //Testing the get all employee method  to check if its employ data is retrieved or not using income(UC5-TC5.3)
        [TestMethod]
        [DataRow(25000, 50000,"Found The Data With Given Income Range")]
        [DataRow(3000000,600000, "No Records Found")]
        public void GivenSPReturnEmpDataResultUsingIncomeRange(double startRangeInc, double endRangeInc, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            string actual = EmployeeRepository.GetEmployeesUsingIncomeRange(employeeModel, startRangeInc, endRangeInc);
            Assert.AreEqual(actual, expected);
        }

        //Testing the aggregate method to check if data is recieved or not(UC6-TC6.1)
        [TestMethod]
        [DataRow('M', "Found The Data Successfully")]
        [DataRow('F', "Found The Data Successfully")]
        [DataRow('A', "No Records Found")]
        public void GivenSPReturnAggregateFunctionsResult(char gender, string expected)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            string actual = EmployeeRepository.AggregateFunctionsByGender(employeeModel,gender);
            Assert.AreEqual(actual, expected);
        }
    }
}
