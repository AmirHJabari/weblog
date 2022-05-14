namespace Weblog.Application.Common.Interfaces;

public interface IDateTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    
    DateTimeOffset OffsetNow { get; }
    DateTimeOffset OffsetUtcNow { get; }

    DateOnly DateNow { get; }
    TimeOnly TimeNow { get; }
    
    DateOnly DateUtcNow { get; }
    TimeOnly TimeUtcNow { get; }
}
