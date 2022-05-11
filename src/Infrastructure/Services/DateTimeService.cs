using Weblog.Application.Common.Interfaces;

namespace Weblog.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
