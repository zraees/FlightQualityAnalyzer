# Flight Quality Analyzer

## Overview
The Flight Quality Analyzer is a .NET 8 application (developed using Visual Studio 2022) designed to automate the analysis of flight data quality. It provides RESTful APIs to retrieve flight records from a CSV file and analyze flight chains for inconsistencies. 

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
- xUnit for unit testing
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
