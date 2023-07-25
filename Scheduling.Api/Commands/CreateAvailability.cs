namespace Scheduling.Api.Commands;

public readonly record struct CreateAvailability(DateOnly StartDate,TimeOnly StartTimeOnly);