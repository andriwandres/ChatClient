using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.AvailabilityStatus.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.AvailabilityStatuses;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.AvailabilityStatuses.Queries
{
    public class GetAllAvailabilityStatusesQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetAllAvailabilityStatusesQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<AvailabilityStatus, AvailabilityStatusResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetAllAvailabilityStatusesQueryHandler_ShouldReturnAvailabilityStatuses()
        {
            // Arrange
            List<AvailabilityStatus> databaseStatuses = new()
            {
                new AvailabilityStatus {AvailabilityStatusId = AvailabilityStatusId.Online},
                new AvailabilityStatus {AvailabilityStatusId = AvailabilityStatusId.Busy},
            };

            _unitOfWorkMock
                .Setup(m => m.AvailabilityStatuses.GetAllAsync())
                .ReturnsAsync(databaseStatuses);

            GetAllAvailabilityStatusesQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

            // Act
            IEnumerable<AvailabilityStatusResource> statuses = await handler.Handle(new GetAllAvailabilityStatusesQuery());

            // Assert
            Assert.NotNull(statuses);
            Assert.Equal(2, statuses.Count());
        }
    }
}
