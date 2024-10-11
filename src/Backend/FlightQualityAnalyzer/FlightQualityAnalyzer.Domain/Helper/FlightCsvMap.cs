// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CsvHelper.Configuration;
using FlightQualityAnalyzer.Domain.Entities;

namespace FlightQualityAnalyzer.Domain.Helper;

public sealed class FlightCsvMap : ClassMap<Flight>
{
    public FlightCsvMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.AircraftRegistrationNumber).Name("aircraft_registration_number");
        Map(m => m.AircraftType).Name("aircraft_type");
        Map(m => m.FlightNumber).Name("flight_number");
        Map(m => m.DepartureAirport).Name("departure_airport");
        Map(m => m.DepartureDatetime).Name("departure_datetime");
        Map(m => m.ArrivalAirport).Name("arrival_airport");
        Map(m => m.ArrivalDatetime).Name("arrival_datetime");
    }
}
