// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace FlightQualityAnalyzer.Infrastructure.Repository;

/// <summary>
/// This repository provides abstraction for accessing data from a CSV file.
/// </summary>
public class FlightRepository : IFlightRepository
{
    private readonly IOptions<FlightFileSetting> _options;

    /// <summary>
    /// injecting options to access FileSetting from app.config
    /// </summary>
    /// <param name="options"></param>
    public FlightRepository(IOptions<FlightFileSetting> options)
    {
        _options = options;
    }

    /// <summary>
    /// This function is called asynchronously, parses each row and adds the flight to a collection. Finally, it returns the collection of flights,
    /// throwing an exception if any row encounters a parsing issue.
    /// </summary>
    /// <returns>result object, with success or failure with data.</returns>
    public async Task<Result<IEnumerable<Flight>>> GetAllAsync()
    {
        // read all data rows from csv file.
        var rows = await File.ReadAllLinesAsync(_options.Value.FilePath).ConfigureAwait(false);
        var flights = new List<Flight>();

        foreach (var row in rows.Skip(1)) // skipping header row.
        {
            // split csv row based on delimiter (comma in our case)
            var rowValues = row.Split(_options.Value.CsvDelimiter ?? ",");

            // this try catch to handle any error because of data parsing.
            try
            {
                var flight = new Flight
                {
                    Id = int.Parse(rowValues[0], CultureInfo.InvariantCulture),
                    AircraftRegistrationNumber = rowValues[1],
                    AircraftType = rowValues[2],
                    FlightNumber = rowValues[3],
                    DepartureAirport = rowValues[4],
                    DepartureDatetime = DateTime.Parse(rowValues[5], CultureInfo.InvariantCulture),
                    ArrivalAirport = rowValues[6],
                    ArrivalDatetime = DateTime.Parse(rowValues[7], CultureInfo.InvariantCulture)
                };

                flights.Add(flight);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Flight>>.Failure(string.Format(CultureInfo.InvariantCulture, Messages.ParsingError, ex.Message));
            }
        }

        return Result<IEnumerable<Flight>>.Success(flights);
    }
}
