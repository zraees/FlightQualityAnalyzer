// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net.Http.Json;
using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FlightQualityAnalyzer.IntegrationTests;

public class FlightsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public FlightsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Tests GetAllFlights endpoint, checking Ok-response, and get flight list as expected.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetAllFlights_ReturnsOk_WhenFlightsExist()
    {
        // Arrange
        // if we have something to arrange for our test case

        // Act
        var response = await _client.GetAsync("/api/Flights/GetAllFlights").ConfigureAwait(true);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var flights = await response.Content.ReadFromJsonAsync<List<Flight>>().ConfigureAwait(true);
        flights.Should().NotBeNull();       // used FluentAssertions for more readable assertions
        flights.Should().NotBeEmpty();
    }

    /// <summary>
    /// Tests GetInconsistentFlights endpoint, checking Ok-response, and get inconsistent flights.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetInconsistentFlights_ReturnsOk_WhenInconsistenciesExist()
    {
        // Arrange
        // if we have something to arrange for our test case

        // Act
        var response = await _client.GetAsync("/api/Flights/GetInconsistentFlights").ConfigureAwait(true);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK); // used FluentAssertions for more readable assertions
        var inconsistencies = await response.Content.ReadFromJsonAsync<List<FlightChainAnalysis>>().ConfigureAwait(true);
        inconsistencies.Should().NotBeNull();
        inconsistencies.Should().NotBeEmpty();
    }
}
