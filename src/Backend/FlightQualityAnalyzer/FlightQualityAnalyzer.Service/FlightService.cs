// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Interfaces;
using FlightQualityAnalyzer.Infrastructure;
using Microsoft.Extensions.Options;

namespace FlightQualityAnalyzer.Service;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IOptions<FlightFileSetting> _options;

    public FlightService(IFlightRepository flightRepository, IOptions<FlightFileSetting> options)
    {
        _flightRepository = flightRepository;
        _options = options;
    }

    /// <summary>
    /// This function is called asynchronously. It first checks whether the CSV file exists on server,
    /// then call repository func to bring all flights from csv file.
    /// </summary>
    /// <returns>flight collection from csv</returns>
    public async Task<Result<IEnumerable<Flight>>> GetAllAsync()
    {
        // early return to enhance code readability
        if (_options.Value == null)
        {
            return Result<IEnumerable<Flight>>.Failure(Messages.FileNotFound);
        }

        var flights = await _flightRepository.GetAllAsync().ConfigureAwait(false);

        if (flights.IsSuccess)
        {
            // here we need to perform any extra data cleanup or validations.
            // like validate flightType or flightNum etc
        }

        return flights;
    }

    /// <summary>
    /// this function to analyse flight chains.
    /// First, the flight list was sorted in ascending order based on departure time.
    /// then, I checked the first condition to ensure that the departure airport of the next flight matches the arrival airport of the current flight.
    /// then, I checked the second condition to ensure that the arrival airport of the next flight matches the departure airport of the current flight.
    /// Finally, I checked the last condition to ensure that the next flight departs after the arrival time of the current flight.
    /// If the next flight does not exist based on these conditions, it indicates an inconsistency.
    /// </summary>
    /// <returns>discrepencies in flight chains with note</returns>
    public async Task<Result<IEnumerable<FlightChainAnalysis>>> GetInconsistentFlightChains()
    {
        var flightResult = await GetAllAsync().ConfigureAwait(false);
        List<FlightChainAnalysis> inconsistentFlights = [];

        // early return: no flights mean no inconsistencies, so return empty with notes
        if (flightResult.IsSuccess == false)
        {
            return Result<IEnumerable<FlightChainAnalysis>>.Success(inconsistentFlights);
        }

        var flights = flightResult.Value.OrderBy(x => x.DepartureDatetime);
        var lastFlightId = flights.LastOrDefault()?.Id ?? 0;

        // now iterating all flights to findout discrepencies
        foreach (var flight in flights)
        {
            // There is no need to check the last flight, so I will skip it.
            if (flight.Id == lastFlightId)
            {
                continue;
            }

            // getting next-departure-airport of the current flight, also added ArrivalDatetime check to move forward no need to search from start.
            var nextDepartureAirport = flights.FirstOrDefault(x => x.DepartureAirport == flight.ArrivalAirport
                                                                && x.ArrivalAirport == flight.DepartureAirport
                                                                && x.DepartureDatetime >= flight.ArrivalDatetime);

            // next-flight not found, so add into inconsistentFlightList
            if (nextDepartureAirport == null)
            {
                inconsistentFlights.Add(new FlightChainAnalysis
                {
                    Flight = flight,
                    Notes = string.Format(CultureInfo.InvariantCulture, Messages.InconsistentFlightChainFound,
                                            flight.ArrivalAirport, flight.DepartureAirport, flight.ArrivalDatetime)
                });
            }
        }

        return Result<IEnumerable<FlightChainAnalysis>>.Success(inconsistentFlights);
    }
}
