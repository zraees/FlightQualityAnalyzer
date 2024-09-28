// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightQualityAnalyzer.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var flights = await _flightService.GetAllAsync().ConfigureAwait(false);

        return Ok(flights.Value);
    }
}
