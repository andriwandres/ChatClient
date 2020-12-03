using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Languages.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Languages;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Languages.Queries
{
    public class GetAllLanguagesQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetAllLanguagesQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Language, LanguageResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetAllLanguagesQueryHandler_ShouldReturnLanguages()
        {
            // Arrange
            IEnumerable<Language> expectedLanguages = new []
            {
                new Language { LanguageId = 1 },
                new Language { LanguageId = 2 },
            };

            Mock<IQueryable<Language>> languageQueryableMock = expectedLanguages
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Languages.GetAll())
                .Returns(languageQueryableMock.Object);

            GetAllLanguagesQuery.Handler handler = new GetAllLanguagesQuery.Handler(_unitOfWorkMock.Object, _mapperMock);

            // Act
            IEnumerable<LanguageResource> languages = await handler.Handle(new GetAllLanguagesQuery());

            // Assert
            Assert.NotNull(languages);
            Assert.Equal(2, languages.Count());
        }
    }
}
