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
        public IRestResponse GetAllEmployees()
        {
            IRestResponse response = default;
            try
            {
                //Arrange
                RestRequest request = new RestRequest("/employees", Method.GET);
                //Act
                response = client.Execute(request);
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
            IRestResponse response = GetAllEmployees();
            //Deserialize json object to list
            var jsonObject = JsonConvert.DeserializeObject<List<EmployeeModel>>(response.Content);
            Assert.AreEqual(7, jsonObject.Count);
            foreach (var employee in jsonObject)
            {
                Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            }
            //Checking the status code 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //Method to add an employee to Json server using(UC19-TC19.1)
        public IRestResponse AddToJsonServer(JsonObject jsonObject)
        {
            IRestResponse response = default;
            try
            {
                RestRequest request = new RestRequest("/employees", Method.POST);
                //Adding type as json in request and passing the json object as a body of request
                request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
                //Executing the request
                response = client.Execute(request);      
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
            //Creating object for json
            JsonObject jsonObject = new JsonObject();
            //Adding new employee details to json object
            jsonObject.Add("name", "Ajay");
            jsonObject.Add("salary", 35000);
            //Calling method to add the employee to json server
            IRestResponse response = AddToJsonServer(jsonObject);
            //Deserializing json object to employee class object
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
            //Assert
            Assert.AreEqual("Ajay", employee.Name);
            Assert.AreEqual(35000, employee.Salary);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        //Testing method to add mul employee to Json server using json object(UC19-TC19.1)
        [TestMethod]
        public void GivenMulEmployeeOnPostAPIReturnMulEmployees()
        {
            //List for storing multiple employeee data json objects
            List<JsonObject> employeeList = new List<JsonObject>();
            //Creating object for json
            JsonObject jsonObjectOne = new JsonObject();
            //Adding new employee details to json object
            jsonObjectOne.Add("name", "Raj");
            jsonObjectOne.Add("salary", 40000);
            employeeList.Add(jsonObjectOne);
            JsonObject jsonObjectTwo = new JsonObject();
            jsonObjectTwo.Add("name", "Mansi");
            jsonObjectTwo.Add("salary", 35000);
            employeeList.Add(jsonObjectTwo);

            employeeList.ForEach((jsonObject) =>
            {
                //Calling method to add the employee to json server
                AddToJsonServer(jsonObject);
            });
            //Check by getting all employee details
            IRestResponse response = GetAllEmployees();
            //Deserializing json object to employee class object
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
            RestRequest request = new RestRequest("/employees/7", Method.PUT);
            //Creating object for json
            JsonObject json = new JsonObject();
            //Adding new employee details to json object for updating
            json.Add("name", "Ankit");
            json.Add("salary", 30000);
            //Adding type as json in request and passing the json object as a body of request
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            //Executing the request
            IRestResponse response = client.Execute(request);
            //Deserializing the json object to employee class object
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            //Checking the response statuscode 200-Ok
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Printing deatils
            Console.WriteLine("Id: {0} || Name: {1} || Salary :{2} ", employee.Id, employee.Name, employee.Salary);
        }
    }
}
