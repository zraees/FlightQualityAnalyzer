// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightQualityAnalyzer.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly ILogger<FlightsController> _logger;

    public FlightsController(IFlightService flightService, ILogger<FlightsController> logger)
    {
        _flightService = flightService;
        _logger = logger;
    }

    /// <summary>
    /// first endpoint: to bring all flights data from csv and return to client in json format
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllFlights")]
    public async Task<IActionResult> Get()
    {
        // get all flights through injected-service
        var flights = await _flightService.GetAllAsync().ConfigureAwait(false);

        // if return with sucess, show flight data to client other wise badreqeust with error message.
        if (flights.IsSuccess)
        {
            return Ok(flights.Value);
        }
        else
        {
            _logger.LogError(flights.Error);
            return BadRequest(flights.Error);
        }
    }

    /// <summary>
    /// second endpoint: to find inconsistencies after analyzing flight chains.
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetInconsistentFlights")]
    public async Task<IActionResult> GetInconsistentFlights()
    {
        // get all inconsistencies through injected-service
        var inconsistentFlights = await _flightService.GetInconsistentFlightChainsAsync().ConfigureAwait(false);

        // if return with sucess, show inconsistencies data to client other wise badreqeust with error message.
        if (inconsistentFlights.IsSuccess)
        {
            return Ok(inconsistentFlights.Value);
        }
        else
        {
            _logger.LogError(inconsistentFlights.Error);
            return BadRequest(inconsistentFlights.Error);
        }
    }
}
