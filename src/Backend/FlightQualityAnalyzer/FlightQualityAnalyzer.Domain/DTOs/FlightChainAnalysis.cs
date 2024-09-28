// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.Domain.Entities;

namespace FlightQualityAnalyzer.Domain.DTOs;
public class FlightChainAnalysis
{
    public required Flight Flight { get; set; }

    public string Notes { get; set; } = string.Empty;
}
