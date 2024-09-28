// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Interfaces;
using Microsoft.Extensions.Options;
using FlightQualityAnalyzer.Infrastructure;

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
    /// <returns></returns>
    public async Task<Result<IEnumerable<Flight>>> GetAllAsync()
    {
        // early return to enhance code readability
        if (_options.Value == null)
        {
            return Result<IEnumerable<Flight>>.Failure(Messages.FileNotFound);
        }

        var flights = await _flightRepository.GetAllAsync().ConfigureAwait(false);

        if (flights.IsSucess)
        {
            // here we need to perform any extra data cleanup or validations.
            // like validate flightType or flightNum etc
        }

        return flights;
    }

    public Task<Result<IEnumerable<FlightChainAnalysis>>> ApplyFlightChainAnalysis()
    {
        throw new NotImplementedException();
    }
}
