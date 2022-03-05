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

        //Test method to return the response got from the request(UC18) 
        [TestMethod]
        public IRestResponse GetAllEmployees()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees", Method.GET);
            //Act
            IRestResponse response = client.Execute(request);
            //Return the response
            return response;
        }

        //Testing method to get all the employee details from json server(UC18-TC18.1)
        [TestMethod]
        public void CallingGetAPIToReturnEmployees()
        {
            IRestResponse response = GetAllEmployees();
            //Deserialize json object to list
            var jsonObject = JsonConvert.DeserializeObject<List<EmployeeModel>>(response.Content);
            Assert.AreEqual(8, jsonObject.Count);
            foreach (var employee in jsonObject)
            {
                Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            }
            //Checking the status code 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //Testing method to add an employee to Json server using json object(UC19-TC19.1)
        [TestMethod]
        public void GivenEmployeeOnPostAPIReturnEmployees()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JsonObject jsonObject = new JsonObject();
            jsonObject.Add("name", "Ajay");
            jsonObject.Add("salary", 35000);

            //Adding a parameter to request 
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            //Act
            IRestResponse response = client.Execute(request);
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            //Assert
            Assert.AreEqual("Ajay", employee.Name);
            Assert.AreEqual(35000, employee.Salary);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
