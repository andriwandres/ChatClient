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

namespace Core.Application.Test.Requests.Languages
{
    public class GetAllLanguagesQueryTests
    {
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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Languages.GetAll())
                .Returns(languageQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Language, LanguageResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetAllLanguagesQuery.GetAllLanguagesQueryHandler handler =
                new GetAllLanguagesQuery.GetAllLanguagesQueryHandler(unitOfWorkMock.Object, mapperMock);

            // Act
            IEnumerable<LanguageResource> languages = await handler.Handle(new GetAllLanguagesQuery());

            // Assert
            Assert.NotNull(languages);
            Assert.Equal(2, languages.Count());
        }
    }
}
