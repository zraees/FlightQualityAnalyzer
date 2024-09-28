// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Entities;

namespace FlightQualityAnalyzer.Domain.Interfaces;
public interface IFlightService
{
    Task<Result<IEnumerable<Flight>>> GetAllAsync();

    Task<Result<IEnumerable<FlightChainAnalysis>>> ApplyFlightChainAnalysis();
}
