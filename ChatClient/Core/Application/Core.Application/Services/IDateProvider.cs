using System;

namespace Core.Application.Services;

public interface IDateProvider
{
    DateTime Now();
    DateTime UtcNow();
}