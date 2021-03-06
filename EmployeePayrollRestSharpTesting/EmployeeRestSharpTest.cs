using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeePayrollRestSharpTesting
{
    [TestClass]
    public class EmployeeRestSharpTest
    {
        //Initializing the restclient object as null
        RestClient client = null;

        //Initializing  the client object to the base url
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:4000");
        }

        //Method to return the response got from the request(UC18) 
        public RestResponse GetAllEmployees()
        {
            RestResponse response = default;
            try
            {
                //Arrange
                RestRequest request = new RestRequest("/employees", Method.Get);
                //Act
                response = client.ExecuteAsync(request).Result;
                //Return the response
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        //Testing method to get all the employee details from json server(UC18-TC18.1)
        [TestMethod]
        public void CallingGetAPIToReturnEmployees()
        {
            RestResponse response = GetAllEmployees();
            //Deserialize json object to list
            var jsonObject = JsonConvert.DeserializeObject<List<EmployeeModel>>(response.Content);
            Assert.AreEqual(4, jsonObject.Count);
            foreach (var employee in jsonObject)
            {
                Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            }
            //Checking the status code 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //Method to add an employee to Json server using(UC19-TC19.1)
        public RestResponse AddToJsonServer(EmployeeModel emp)
        {
            RestResponse response = default;
            try
            {
                RestRequest request = new RestRequest("/employees", Method.Post);
                //Adding jsonbody in request and passing the employee list as body of request
                request.AddHeader("Content-type", "application/json");
                request.AddJsonBody(new { Name = emp.Name, Salary = emp.Salary });
                //Executing the request
                response = client.ExecuteAsync(request).Result;      
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        //Testing method to add an employee to Json server using json object(UC19-TC19.1)
        [TestMethod]
        public void GivenEmployeeOnPostAPIReturnEmployee()
        {
            //Employee object add employee data 
            EmployeeModel employee = new EmployeeModel();
            employee.Name = "Ajay"; employee.Salary = 35000;
            //Calling the addtojsonserver and passing the employee data
            RestResponse response = AddToJsonServer(employee);
            //Converting the json object to employee object
            var resEmployee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            //Assert
            Assert.AreEqual("Ajay", resEmployee.Name);
            Assert.AreEqual(35000, resEmployee.Salary);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        //Testing method to add mul employee to Json server using json object(UC19-TC19.1)
        [TestMethod]
        public void GivenMulEmployeeOnPostAPIReturnMulEmployees()
        {
            //List for storing multiple employee data 
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            employeeList.Add(new EmployeeModel { Name = "Mahipal", Salary = 25000 });
            employeeList.Add(new EmployeeModel { Name = "Rahul", Salary = 25000 });
            employeeList.Add(new EmployeeModel { Name = "Ankit", Salary = 35000 });
            employeeList.ForEach((employee) =>
            {
                //Calling method to add the employee to json server
                AddToJsonServer(employee);
            });
            //Calling the get all emp to check all employee details
            RestResponse response = GetAllEmployees();
            //Deserializing json object to list of employee class object
            var resEmployeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(response.Content);
            resEmployeeList.ForEach((employee) =>
            {
                Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            });
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //Test method to update details of json server using put(UC21-TC21.1)
        [TestMethod]
        public void TestMethodToUpdateDetails()
        {
            //Setting rest request to url and setting method to put to update details
            RestRequest request = new RestRequest("/employees/8", Method.Put);
            //Adding jsonbody in request and passing the employee list as body of request
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new { Name = "Ankit", Salary = 25000 }); ;
            //Executing the request
            RestResponse response = client.ExecuteAsync(request).Result;
            //Deserializing the json object to employee class object
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            //Checking the response statuscode
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Printing details
            Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
        }

        //Test method to delete an employee based on id of employee from json server(UC22-TC22.1)
        [TestMethod]
        public void TestMethodToDeleteDetails()
        {
            //Setting rest request to url and setting method to delete to delete particular id
            RestRequest request = new RestRequest("/employees/5", Method.Delete);
            //Executing the request
            RestResponse response = client.ExecuteAsync(request).Result;
            //Calling the get all emp to check all employee details if given id is deleted or not 
            RestResponse restResponse = GetAllEmployees();
            //Converting json object to list of employee object
            var resEmployeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(restResponse.Content);
            resEmployeeList.ForEach((employee) =>
            {
                Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            });
            //Checking the response statuscode
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode); 
        }
    }
}
