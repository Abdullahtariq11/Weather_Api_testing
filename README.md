API Testing Project Documentation
Abdullah Tariq



 1. Project Overview
Purpose:  The purpose of this project is to perform API testing on the WeatherAPI.com API using RestSharp.
Objectives:
-	Validate the functionality and reliability of the WeatherAPI.com API.
-	Verify the accuracy of weather data retrieved from the API.
-	Identify and report any issues or anomalies in the API responses.
Scope:The scope of this project is limited to testing the current weather endpoint of the WeatherAPI.com API.

2. API Selection
Weather API: WeatherAPI.com has been selected as the API for this project.
 Justification: WeatherAPI.com provides comprehensive weather data and offers a user-friendly interface for accessing weather information.

3. API Documentation Review
The WeatherAPI.com API documentation has been reviewed to understand its functionality and usage.
Endpoints to be tested:
1.	Current Weather: Retrieve current weather conditions for a specific location.


4. Test Strategy
Testing Approach: A combination of functional and integration testing will be employed.
Test Scenarios: The test scenarios will cover typical and edge cases for the current weather endpoint.
 Test Data: Both valid and invalid data inputs will be used to verify the API responses.

5. Test Plan
Test Cases:
1.	Valid location and API key
	Expected Results: Response status code 200 and accurate weather data.
2.	Invalid location and API key
	Expected Results: Response status code 401 (Unauthorized) or 404 (Not Found) depending on the error.
3.	Missing location or API key
	Expected Results: Response status code 400 (Bad Request) or 401 (Unauthorized) depending on the error.

6. Test Execution
RestSharp Setup: RestSharp was installed using NuGet package manager in the project.
Test Case Execution:
-	RestSharp client and request objects were created.
-	Headers, query parameters, and authentication were set based on the test case.
-	‘streamreader’ class was used instead of ‘TextFieldParser’ for reading csv as the date in csv file was not that complex.
-	API responses were captured and validated against the expected results.



7. Error Handling and Reporting
Error Handling: Errors encountered during API testing were captured by inspecting the response status codes and error messages in the API responses.
Reporting: Test results were logged using a custom reporting mechanism, including details on passed and failed test cases.

8. Test Environment Setup
Software and Dependencies: RestSharp and NUnit testing framework were installed as project dependencies.
Configuration: The WeatherAPI.com API key was obtained and configured in the RestSharp requests.

9. Test Data Management
Test Data Sources: Test data for location names was prepared in a CSV file.
Test Data Preparation: The test data was read from the CSV file and used as input for the test cases.
Test Data Cleanup: No specific cleanup was required as the test data was read-only.

10. Test Automation
Automation Framework: The API tests were automated using the NUnit testing framework.
Setup and Execution: Test cases were organized into test fixtures and executed automatically using the NUnit test runner.

11. Risk Assessment
Risks: Potential risks included API rate limits and potential changes to the API endpoints or response structures.
Mitigation Strategies: To mitigate the risks, delays were incorporated between API requests to stay within rate limits, and regular checks were conducted to ensure API changes were addressed promptly.

12. Timeline and Resources
Timeline: The API testing project was conducted over a period of a week.
Resources: The testing was performed using RestSharp and NUnit as the testing tools.
