using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.GroupMemberships;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries;

public class GetMembershipByIdQueryTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public GetMembershipByIdQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        MapperConfiguration mapperConfiguration = new(config =>
        {
            config.CreateMap<GroupMembership, GroupMembershipResource>();
        });

        _mapperMock = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetMembershipByIdQueryHandler_ShouldReturnNull_WhenMembershipDoesNotExist()
    {
        // Arrange
        GetMembershipByIdQuery request = new() { GroupMembershipId = 1 };

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByIdAsync(request.GroupMembershipId))
            .ReturnsAsync(null as GroupMembership);

        GetMembershipByIdQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

        // Act
        GroupMembershipResource membership = await handler.Handle(request);

        // Assert
        Assert.Null(membership);
    }

    [Fact]
    public async Task GetMembershipByIdQueryHandler_ShouldReturnMembership_WhenMembershipExists()
    {
        // Arrange
        GetMembershipByIdQuery request = new() { GroupMembershipId = 1 };

        GroupMembership expectedMembership = new() {GroupMembershipId = 1};

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByIdAsync(request.GroupMembershipId))
            .ReturnsAsync(expectedMembership);

        GetMembershipByIdQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

        // Act
        GroupMembershipResource membership = await handler.Handle(request);

        // Assert
        Assert.NotNull(membership);
        Assert.Equal(request.GroupMembershipId, membership.GroupMembershipId);
    }
}