using Core.Application.Requests.AvailabilityStatus.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.AvailabilityStatuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class AvailabilityStatusControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        public AvailabilityStatusControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetAllAvailabilityStatuses_ShouldReturnAvailabilityStatuses()
        {
            // Arrange
            IEnumerable<AvailabilityStatusResource> statuses = new[]
            {
                new AvailabilityStatusResource {AvailabilityStatusId = AvailabilityStatusId.Online},
                new AvailabilityStatusResource {AvailabilityStatusId = AvailabilityStatusId.Busy},
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllAvailabilityStatusesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(statuses);

            AvailabilityStatusController controller = new AvailabilityStatusController(_mediatorMock.Object);

            // Act
            ActionResult<IEnumerable<AvailabilityStatusResource>> response = await controller.GetAllAvailabilityStatuses();

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            IEnumerable<AvailabilityStatusResource> actualStatuses = result.Value as AvailabilityStatusResource[];

            Assert.NotNull(actualStatuses);
            Assert.Equal(2, actualStatuses.Count());
        }
    }
}
