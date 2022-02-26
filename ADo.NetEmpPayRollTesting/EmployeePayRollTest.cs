using ADO.NetEmployeePayRollProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
