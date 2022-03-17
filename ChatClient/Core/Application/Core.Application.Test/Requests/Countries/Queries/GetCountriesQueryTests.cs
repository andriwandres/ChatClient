﻿using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Countries.Queries;
using Core.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.ViewModels;
using Xunit;

namespace Core.Application.Test.Requests.Countries.Queries;

public class GetCountriesQueryTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public GetCountriesQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Country, CountryViewModel>();
        });

        _mapperMock = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetCountriesQueryHandler_ReturnsCountries()
    {
        // Arrange
        GetCountriesQuery request = new();

        List<Country> expectedCountries = new()
        {
            new Country { CountryId = 1 },
            new Country { CountryId = 2 },
        };

        _unitOfWorkMock
            .Setup(m => m.Countries.GetAllAsync())
            .ReturnsAsync(expectedCountries);

        GetCountriesQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

        // Act
        IEnumerable<CountryViewModel> actualCountries = await handler.Handle(request);

        // Assert
        Assert.Equal(2, actualCountries.Count());
    }
}