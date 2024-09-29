# Flight Quality Analyzer

## Overview
The Flight Quality Analyzer is a .NET 8 application designed to automate the analysis of airline flight data quality. It provides RESTful APIs to retrieve flight records from a CSV file and analyze flight chains for inconsistencies.
### some best practices are:
1. Use Onion Architecture, to make application loosly coupled, easy to maintenane and testable.
2. use editorconfig, to implement coding style and standards
3. use some design patterns such as repository, option and result patterns.
   

## Setup Instructions
1. Clone the repository.
2. Navigate to the project directory.
3. Run `dotnet restore` to restore dependencies.
4. Run `dotnet run` to start the application.

## API Endpoints
### Get All Flights
- **GET /flights**
- Returns all flight records in JSON format.

### Analyze Flight Chains
- **GET /flightchains**
- Analyzes flight chains and returns inconsistencies in JSON format.

## Example Usage
Use a tool like `curl` or Postman to access the endpoints:
```bash
curl http://localhost:5000/flights
curl http://localhost:5000/flightchains
