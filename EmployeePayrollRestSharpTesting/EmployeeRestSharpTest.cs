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

        //Getting all the employee details from json server(UC18-TC18.1)
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
    }
}
