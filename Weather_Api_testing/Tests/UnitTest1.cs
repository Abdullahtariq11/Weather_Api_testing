using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Weather_Api_testing
{
    public class Tests
    {
        private static IRestClient _client;
        private string _url= "http://api.weatherapi.com/v1";
        private string _urlpath = "/current.json";
        private string _ApiKey = "bab25eeea39a482d88f202152232705";
        private string _invalidApiKey = "bab25eeea39a482d88f202157832705";
        private string _filePath = "C:/Users/abdul/Documents/Study/Software tessting/API Testing/New folder/Weather Api testing/Weather_Api_testing/Weather_Api_testing/Test Data/city_data.csv";
        private string _filePathInvalid = "C:/Users/abdul/Documents/Study/Software tessting/API Testing/New folder/Weather Api testing/Weather_Api_testing/Weather_Api_testing/Test Data/city_data-Invalid.csv";
        List<string> csvData = new List<string>();
        List<string> csvDataInvalid = new List<string>();

        private ExtentReports extent;
        private ExtentTest test;



        [OneTimeSetUp] 
        public void intializeCLient ()=> _client = new RestClient (_url);
        [OneTimeSetUp]
        public void getTestData()
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                bool isFirstRowHeader = true;
                if (isFirstRowHeader)
                {
                    reader.ReadLine();
                }

                string line;

               
                while ((line = reader.ReadLine()) != null)
                {
                    
                    string[] parts = line.Split(',');

                    csvData.Add(parts[0]);
                   
                }
            }
        }

        public void GetinvalidData()
        {
            csvDataInvalid.Clear();
            using (StreamReader reader = new StreamReader(_filePathInvalid))
            {
                bool isFirstRowHeader = true;
                if (isFirstRowHeader)
                {
                    reader.ReadLine();
                }

                string line;


                while ((line = reader.ReadLine()) != null)
                {

                    string[] parts = line.Split(',');

                    csvDataInvalid.Add(parts[0]);

                }
            }
        }

        [OneTimeSetUp]
        public void InitializeReport()
        {
            string reportPath = "C:/Users/abdul/Documents/Study/Software tessting/API Testing/New folder/Weather Api testing/Weather_Api_testing/Weather_Api_testing/test_report.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [Test]
        public void CheckGetWeatherData()
        {
            test = extent.CreateTest("checkGerWeatherData");
            try
            {
                var request = new RestRequest(_urlpath).AddQueryParameter("key", _ApiKey).AddQueryParameter("q", "karachi");
                var repsonse = _client.ExecuteGetAsync(request);
                Assert.AreEqual(HttpStatusCode.OK, repsonse.Result.StatusCode);
                Console.WriteLine(JToken.Parse(repsonse.Result.Content).SelectToken("current").SelectToken("temp_c"));
                test.Pass("Test passed");
            }
            catch (Exception ex)
            {
                test.Fail("Test failed");
                test.Error(ex);
            }


        }

        [Test]
        public void CheckWithCorrectData()
        {
           

            foreach (var data in csvData)
            {
                if (data != null)
                {
                    test = extent.CreateTest("CheckWithCorrectData - " + data);
                    try
                    {

                        var request = new RestRequest(_urlpath).AddQueryParameter("key", _ApiKey).AddQueryParameter("q", data);
                        var repsonse = _client.ExecuteGetAsync(request);
                        Assert.AreEqual(HttpStatusCode.OK, repsonse.Result.StatusCode);
                        Console.WriteLine(data);
                        test.Pass("Test passed");
                    }
                    catch (Exception ex)
                    {
                        test.Fail("Test failed");
                        test.Error(ex);
                    }
                }
            }
            
        }

        [Test]

        public void CheckWithInvalidData()
        {
            GetinvalidData();
            foreach (string data in csvDataInvalid)
            {
                test = extent.CreateTest("CheckWithInvalidData - " + data);

                try
                {
                    var request = new RestRequest(_urlpath).AddQueryParameter("key", _ApiKey).AddQueryParameter("q", data);
                    var response = _client.ExecuteGetAsync(request);
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.Result.StatusCode);
                    Console.WriteLine(response.Result.Content);
                    test.Pass("Test passed");
                }
                catch(Exception ex)
                {
                    test.Fail("Test failed");
                    test.Error(ex);
                }

            }
        }

        [Test]

        public void CheckWithInvalidKey()
        {
            test = extent.CreateTest("CheckWithInvalidKey");
            try
            {
                var request = new RestRequest(_urlpath).AddQueryParameter("key", _invalidApiKey);
                var response = _client.ExecuteGetAsync(request);
                Assert.AreEqual(HttpStatusCode.Forbidden, response.Result.StatusCode);
                test.Pass("Test passed");
            }
            catch (Exception ex)
            {
                test.Fail("Test failed");
                test.Error(ex);
            }

        }

        [Test]

        public void CheckWithNoKey()
        {
            test = extent.CreateTest("CheckWithNoKey");
            try
            {
                var request = new RestRequest(_urlpath).AddQueryParameter("q", "Karachi");
                var response = _client.ExecuteGetAsync(request);
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.Result.StatusCode);
                test.Pass("Test passed");
            }
            catch(Exception ex)
            {
                test.Fail("Test failed");
                test.Error(ex);
            }

        }
        [OneTimeTearDown]
        public void CloseReport()
        {
            extent.Flush();
        }



    }
}







