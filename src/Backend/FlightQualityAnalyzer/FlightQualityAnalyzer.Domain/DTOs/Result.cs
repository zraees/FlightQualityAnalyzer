// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FlightQualityAnalyzer.Domain.DTOs;

/// <summary>
/// Result pattern is used to standard return value, with either success or failure.
/// In this project I used custom result pattern, we can use FluentResult package also.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    public T Value { get; set; }

    public string Error { get; set; } = string.Empty;

    public bool IsSucess { get; set; }

    private Result(T value, string error, bool isSucess)
    {
        Value = value;
        Error = error;
        IsSucess = isSucess;
    }

    /// <summary>
    /// implicitly convert success into Result-Obj with data also.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Success(T value) => new Result<T>(value, string.Empty, true);

    /// <summary>
    /// implicitly convert failure into Result-Obj with error details.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> Failure(string error) => new Result<T>(default!, error, false);
}
