// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Interfaces;
using FlightQualityAnalyzer.Service;
using FlightQualityAnalyzer.Tests.Helper;
using Microsoft.Extensions.Options;
using Moq;

namespace FlightQualityAnalyzer.Tests;

public class FlightTests
{
    private readonly Mock<IOptions<FlightFileSetting>> _mockOptions;
    private readonly Mock<IFlightRepository> _mockFlightRepository;
    private readonly FlightService _flightSerive;

    public static IEnumerable<object[]> FlightTestData =>
        new List<object[]>
        {
            new object[] { new List<Flight> {
                new()
                {
                    Id = 1,
                    FlightNumber = "M645",
                    DepartureAirport = "HEL",
                    ArrivalAirport = "DXB",
                    AircraftRegistrationNumber = "123",
                    AircraftType = "232",
                    DepartureDatetime = new DateTime(2024, 1, 1, 10, 0, 0),
                    ArrivalDatetime = new DateTime(2024, 1, 1, 12, 0, 0)
                },
                new()
                {
                    Id = 2,
                    FlightNumber = "W679",
                    DepartureAirport = "HND",
                    ArrivalAirport = "CDG",
                    AircraftRegistrationNumber = "124",
                    AircraftType = "232",
                    DepartureDatetime = new DateTime(2024, 1, 2, 15, 30, 0),
                    ArrivalDatetime = new DateTime(2024, 1, 2, 18, 0, 0)
                },
                new()
                {
                    Id = 3,
                    FlightNumber = "D677",
                    DepartureAirport = "LHR",
                    ArrivalAirport = "JFK",
                    AircraftRegistrationNumber = "125",
                    AircraftType = "232",
                    DepartureDatetime = new DateTime(2024, 1, 3, 9, 15, 0),
                    ArrivalDatetime = new DateTime(2024, 1, 3, 11, 45, 0)
                },
                new()
                {
                    Id = 4,
                    FlightNumber = "U482",
                    DepartureAirport = "HEL",
                    ArrivalAirport = "FRA",
                    AircraftRegistrationNumber = "126",
                    AircraftType = "232",
                    DepartureDatetime = new DateTime(2024, 1, 4, 14, 0, 0),
                    ArrivalDatetime = new DateTime(2024, 1, 4, 17, 0, 0)
                },
            }, 4 },
        };

    public FlightTests()
    {
        // Set up the mock to return Flight-File-Setting
        var appSettings = new FlightFileSetting
        {
            FilePath = "DATA\\flights.csv",
            CsvDelimiter = ","
        };

        _mockOptions = new Mock<IOptions<FlightFileSetting>>();     // simulates the behavior of option (appsettings)
        _mockOptions.Setup(x => x.Value).Returns(appSettings);

        _mockFlightRepository = new Mock<IFlightRepository>();      // simulates the behavior of flight repository
        _flightSerive = new FlightService(_mockFlightRepository.Object, _mockOptions.Object);
    }

    /// <summary>
    /// Tests GetAllAsync, checking that it returns an empty list when there are no flights.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetAllAsync_EmptyList()
    {
        // Arrange
        var resultData = Result<IEnumerable<Flight>>.Success([]);
        _mockFlightRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(resultData);

        // Act
        var result = await _flightSerive.GetAllAsync().ConfigureAwait(true);

        // Assert
        Assert.Empty(result.Value);
    }

    /// <summary>
    /// Tests GetAllAsync, checking that flight list should have same record count as expected.
    /// </summary>
    /// <param name="flights">flight list</param>
    /// <param name="expected">expected flight count</param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(FlightTestData))]
    public async Task GetAllAsync_MustHaveSameFlightCount(List<Flight> flights, int expected)
    {
        // Arrange 
        var flightsInResultObj = Result<IEnumerable<Flight>>.Success(flights);
        _mockFlightRepository.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(flightsInResultObj));

        // Act
        var result = await _flightSerive.GetAllAsync().ConfigureAwait(true);

        // Assert
        Assert.Equal(expected, result.Value.Count());
    }

    /// <summary>
    /// Tests GetInconsistentFlightChainsAsync, should return no-inconsistencies if there is no flights data.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetInconsistentFlightChainsAsync_NoFlightData_NoInconsistencies()
    {
        // Arrange
        var resultData = Result<IEnumerable<Flight>>.Success([]);
        _mockFlightRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(resultData);

        // Act
        var result = await _flightSerive.GetInconsistentFlightChainsAsync().ConfigureAwait(true);

        // Assert
        Assert.Empty(result.Value);
    }

    /// <summary>
    /// Tests GetInconsistentFlightChainsAsync, provided flawed flight data (from csv file) so as expeccted there must be inconsistencies
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetInconsistentFlightChainsAsync_FlightData_WithInconsistencies()
    {
        // arrange
        var flights = Utilities.LoadTestData(_mockOptions.Object.Value.FilePath); // sample data from csv
        var resultData = Result<IEnumerable<Flight>>.Success(flights);
        _mockFlightRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(resultData);

        // Act
        var result = await _flightSerive.GetInconsistentFlightChainsAsync().ConfigureAwait(true);

        // Assert
        Assert.Equal(50, result.Value.Count());
    }

    /// <summary>
    /// Tests GetInconsistentFlightChainsAsync, provided ideal flight data so as expeccted there much be no-inconsistencies
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetInconsistentFlightChainsAsync_FlightData_WithoutInconsistencies()
    {
        // Arrange
        var flights = new List<Flight> {
                new() { Id = 1, FlightNumber = "M645", DepartureAirport = "HEL", ArrivalAirport = "DXB", AircraftRegistrationNumber="123", AircraftType="232", ArrivalDatetime= new DateTime(2023, 11, 1, 10, 0, 0), DepartureDatetime=new DateTime(2023, 11, 1, 12, 20, 0) },
                new() { Id = 3, FlightNumber = "D677", DepartureAirport = "DXB", ArrivalAirport = "HEL", AircraftRegistrationNumber="123", AircraftType="232", ArrivalDatetime= new DateTime(2024, 1, 2, 15, 30, 0), DepartureDatetime=new DateTime(2024, 1, 2, 19, 30, 0) },
                new() { Id = 4, FlightNumber = "U482", DepartureAirport = "HEL", ArrivalAirport = "DXB", AircraftRegistrationNumber="123", AircraftType="232", ArrivalDatetime= new DateTime(2024, 1, 8, 9, 15, 0), DepartureDatetime=new DateTime(2024, 1, 9, 11, 25, 0) },
            };
        var resultData = Result<IEnumerable<Flight>>.Success(flights);
        _mockFlightRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(resultData);

        // Act
        var result = await _flightSerive.GetInconsistentFlightChainsAsync().ConfigureAwait(true);

        // Assert
        Assert.Empty(result.Value);
    }
}
