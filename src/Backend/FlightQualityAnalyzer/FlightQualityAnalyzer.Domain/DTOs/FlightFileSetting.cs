// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FlightQualityAnalyzer.Domain.DTOs;
public record FlightFileSetting
{
    public string FilePath { get; set; } = string.Empty;

    public string CsvDelimiter { get; set; } = string.Empty;
}
