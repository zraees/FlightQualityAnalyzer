// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FlightQualityAnalyzer.Domain.Entities;

public class Flight
{
    public int Id { get; set; }

    public required string AircraftRegistrationNumber { get; set; }

    public required string AircraftType { get; set; }

    public required string FlightNumber { get; set; }

    public required string DepartureAirport { get; set; }

    public required DateTime DepartureDatetime { get; set; }

    public required string ArrivalAirport { get; set; }

    public required DateTime ArrivalDatetime { get; set; }
}
