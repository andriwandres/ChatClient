using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Languages.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Languages;
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

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<Language, LanguageResource>()
                    .ForMember(d => d.CountryFlagImage, c => c.Ignore());
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetAllLanguagesQueryHandler_ShouldReturnLanguages()
        {
            // Arrange
            List<Language> expectedLanguages = new()
            {
                new Language { LanguageId = 1 },
                new Language { LanguageId = 2 },
            };

            _unitOfWorkMock
                .Setup(m => m.Languages.GetAllAsync())
                .ReturnsAsync(expectedLanguages);

            GetAllLanguagesQuery.Handler handler = new(_unitOfWorkMock.Object, _mapperMock);

            // Act
            IEnumerable<LanguageResource> languages = await handler.Handle(new GetAllLanguagesQuery());

            // Assert
            Assert.NotNull(languages);
            Assert.Equal(2, languages.Count());
        }
    }
}
