using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.AvailabilityStatus.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.AvailabilityStatuses;
using MockQueryable.Moq;
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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<AvailabilityStatus, AvailabilityStatusResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetAllAvailabilityStatusesQueryHandler_ShouldReturnAvailabilityStatuses()
        {
            // Arrange
            IEnumerable<AvailabilityStatus> databaseStatuses = new[]
            {
                new AvailabilityStatus {AvailabilityStatusId = AvailabilityStatusId.Online},
                new AvailabilityStatus {AvailabilityStatusId = AvailabilityStatusId.Busy},
            };

            IQueryable<AvailabilityStatus> queryableMock = databaseStatuses
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.AvailabilityStatuses.GetAll())
                .Returns(queryableMock);

            GetAllAvailabilityStatusesQuery.Handler handler = new GetAllAvailabilityStatusesQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            IEnumerable<AvailabilityStatusResource> statuses = await handler.Handle(new GetAllAvailabilityStatusesQuery());

            // Assert
            Assert.NotNull(statuses);
            Assert.Equal(2, statuses.Count());
        }
    }
}
