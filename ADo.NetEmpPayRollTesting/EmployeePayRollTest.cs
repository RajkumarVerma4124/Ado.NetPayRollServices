﻿using ADO.NetEmployeePayRollProgram;
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
        public void GivenSPReturnUpdatedResult(int id, string name, float basicPay, string expected)
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
        public void GivenSPReturnUpdatedErResult(int id, string name, float basicPay, string expected)
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
    }
}