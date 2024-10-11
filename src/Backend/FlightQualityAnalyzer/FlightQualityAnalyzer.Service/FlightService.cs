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
    /// <returns>flight collection from csv, in result object</returns>
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
    /// considering that
    /// 1- flights are grouped by aircraft registration number and 
    /// 2- sort in asc these grouped flights by departure datetime
    /// 3- check in sequential manner for inconsistency in a flight by checking 
    /// 3.1- if the upcoming flight's departure airport does not match the active flight's arrival airport
    /// </summary>
    /// <returns>discrepencies in flight chains with note, in result object</returns>
    public async Task<Result<IEnumerable<FlightChainAnalysis>>> GetInconsistentFlightChainsAsync()
    {
        var flightResult = await GetAllAsync().ConfigureAwait(false);

        // early return: no flights mean no inconsistencies, so return empty with notes
        if (flightResult.IsSuccess == false)
        {
            return Result<IEnumerable<FlightChainAnalysis>>.Success([]);
        }

        // Group flights by aircraft registration number
        var flightsGroupedByRegNo = flightResult.Value.GroupBy(f => f.AircraftRegistrationNumber);
        List<FlightChainAnalysis> inconsistentFlights = [];

        // iterate each group of flights per AircraftRegNo
        foreach (var group in flightsGroupedByRegNo)
        {
            // Ascending flights by departure_datetime for the selected/choosen aircraft 
            var flightsOrderByDepartureTime = group.OrderBy(f => f.DepartureDatetime).ToList();

            // loop on the sorted flights and trying to find the inconsistent flight chains
            for (int i = 0; i < flightsOrderByDepartureTime.Count - 1; i++)
            {
                var activeFlight = flightsOrderByDepartureTime[i];
                var upcomingFlight = flightsOrderByDepartureTime[i + 1];

                // checking if 'arrival airport' of the 'active flight' not matches the 'departure airport' of the 'upcoming flight'
                if (activeFlight.ArrivalAirport != upcomingFlight.DepartureAirport)
                {
                    inconsistentFlights.Add(new FlightChainAnalysis
                    {
                        Flight = activeFlight,
                        Notes = string.Format(CultureInfo.InvariantCulture, Messages.InconsistentFlightChainFound,
                                                             activeFlight.ArrivalAirport, activeFlight.DepartureAirport, activeFlight.ArrivalDatetime)
                    });
                }
            }
        }

        return Result<IEnumerable<FlightChainAnalysis>>.Success(inconsistentFlights);
    }
}
