using ProjectNamePlaceHolder.Application.Interfaces.Services;
using System;

namespace ProjectNamePlaceHolder.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}