using Weblog.Application.Common.Interfaces;

namespace Weblog.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTimeOffset OffsetNow => DateTimeOffset.UtcNow;
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.Now;

    public DateOnly DateNow => DateOnly.FromDateTime(Now);
    public TimeOnly TimeNow => TimeOnly.FromDateTime(Now);

    public DateOnly DateUtcNow => DateOnly.FromDateTime(UtcNow);
    public TimeOnly TimeUtcNow => TimeOnly.FromDateTime(UtcNow);
}
