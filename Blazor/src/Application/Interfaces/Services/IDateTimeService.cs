using System;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}