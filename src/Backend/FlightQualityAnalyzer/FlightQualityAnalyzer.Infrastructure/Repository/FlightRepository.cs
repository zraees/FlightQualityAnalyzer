// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using CsvHelper;
using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Helper;
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
    /// This function is called asynchronously, parses csv using "csvParser .net library".
    /// Before parsing we need to map csv cols and entity class properties 
    /// (in our case csv cols and entity properties are not same so we used ClassMap)
    /// throwing an exception if any parser encounters a parsing issue.
    /// </summary>
    /// <returns>result object, with success or failure with data.</returns>
    public async Task<Result<IEnumerable<Flight>>> GetAllAsync()
    {
        // this try catch to handle any error because of data parsing.
        try
        {
            var reader = new StreamReader(_options.Value.FilePath);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            // this mapping is to map csv cols with entity cols
            csv.Context.RegisterClassMap<FlightCsvMap>();

            // read all data rows from csv file.
            var flightsAsync = await csv.GetRecordsAsync<Flight>().ToListAsync().ConfigureAwait(false);

            return Result<IEnumerable<Flight>>.Success(flightsAsync);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Flight>>.Failure(string.Format(CultureInfo.InvariantCulture, Messages.ParsingError, ex.Message));
        }
    }
}
