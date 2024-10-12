// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using CsvHelper;
using FlightQualityAnalyzer.Domain.Entities;
using FlightQualityAnalyzer.Domain.Helper;

namespace FlightQualityAnalyzer.Tests.Helper;
public static class Utilities
{
    public static IEnumerable<Flight> LoadTestData(string CsvFilePath)
    {
        using var reader = new StreamReader(CsvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        // this mapping is to map csv cols with entity cols
        csv.Context.RegisterClassMap<FlightCsvMap>();
        return csv.GetRecords<Flight>().ToList();
    }
}
