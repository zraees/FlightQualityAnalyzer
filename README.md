# Flight Quality Analyzer

## Overview
The Flight Quality Analyzer is a .NET 8 application (developed using Visual Studio 2022) designed to automate the analysis of flight data quality. It provides RESTful APIs to retrieve flight records from a CSV file and analyze flight chains for inconsistencies. 

### Key Enhancements (based on feedback)
1. **CSV Parsing**: Utilized the `CsvParser` library for efficient CSV parsing.
2. **Business Logic Revamped**: I have revised the logic for identifying inconsistencies in flight chains, and I hope it works as expected.
3. **Integration Testing**: Added a new project, `FlightQualityAnalyzer.IntegrationTests`, and implemented integration tests using `xUnit`, `Microsoft.AspNetCore.Mvc.Testing`, and `FluentAssertions`. FluentAssertions is a .NET library that makes it easier to write clear and expressive assertions in tests.
4. **Unit Tests**: I see that there’s still room to improve, especially in testing different CSV reading cases. I’ve added CSV files to the unit test `GetInconsistentFlightChainsAsync_FlightData_WithInconsistencies` to make our testing more thorough. This shows how I can use CSV data effectively. I’m open to any suggestions you have, as I’m always eager to learn. I’ve also included different arrival and departure times.

## Room for Enhancement
There are several areas in the project that can be improved:
1. Implement JWT authentication and introduce API versioning.
2. Enhance code coverage by adding additional unit and integration tests. Also, incorporate a wider variety of datasets to test different scenarios.
3. **Rate Limiting**: Implement rate limiting to protect the API from excessive requests.
4. **Documentation**: Enhance API documentation using Swagger comments for better developer experience.
5. **Caching**: Implement caching strategies for frequently accessed data to improve performance.
6. **Monitoring and Metrics**: Integrate monitoring tools (e.g., Application Insights) to track performance and usage metrics.

### Some best practices used:
1. Utilize Onion Architecture to create an application that is loosely coupled, easy to maintain, and highly testable.
2. Asynchronous data processing using `async` and `await`.  
4. Implement design patterns, such as the Repository, Option, and Result patterns.
5. Repository Pattern for data access and encapsulation and Result Pattern for structured success and error handling.
6. Unit tests with xUnit to ensure code quality.
3. Use EditorConfig to enforce coding styles and standards.
7. Utilize Serilog for logging and save logs to the file system. (Note: This feature is included to demonstrate logging, but is not fully implemented in the application.) 
8. Implement global exception handling introduced in .NET Core 8.
     
## Technologies used:
- .NET 8.0
- C#
- xUnit for unit testing & integration test
- Moq for mocking dependencies
- ASP.NET Core for building the API
  
## How to run code locally
1. Clone the repository or download zip.
2. Navigate to the project directory.
3. Run `dotnet restore` to restore dependencies, or just run application with visual studio 2022 
4. Run `dotnet run` to start the application.

## Two API Endpoints available:
### 1- Get all flights from csv to json
- **GET /api/Flights/GetAllFlights**
- Returns all flight data in JSON format.

### 2- Find inconsistencies in flight chains
- **GET /api/Flights/GetInconsistentFlights**
- Analyzes flight chains and returns inconsistencies with note/remarks in JSON format.

## How to access Endpoints
Swagger is added to access endpoint or use curl or Postman to access the endpoints:
