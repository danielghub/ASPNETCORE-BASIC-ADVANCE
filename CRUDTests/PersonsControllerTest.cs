using AutoFixture;
using Castle.Core.Smtp;
using FluentAssertions;
using Learning_CRUD_dotnetcore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDTests
{

    public class PersonsControllerTest
    {
        private readonly Mock<IPersonsService> _personsServiceMock;
        private readonly Mock<ICountryService> _countriesServiceMock;
        private readonly IPersonsService _personService;
        private readonly ICountryService _countryService;
        private readonly IFixture _autoFixture;
        public PersonsControllerTest()
        {
            _personsServiceMock = new Mock<IPersonsService>();
            _countriesServiceMock = new Mock<ICountryService>();

            _personService = _personsServiceMock.Object;
            _countryService = _countriesServiceMock.Object;

            _autoFixture = new Fixture();
        }

        [Fact]
        public async Task Index_Should_Return_ViewResult()
        {
            //Arrange
            List<PersonResponse> personResponses = _autoFixture.Create<List<PersonResponse>>();

            _personsServiceMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(personResponses);
            _personsServiceMock.Setup(temp => temp.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(), It.IsAny<SortOrderOptions>())).ReturnsAsync(personResponses);
            PersonsController personsController = new PersonsController(_personService, _countryService);

            //Act
            IActionResult result = await personsController.Index(_autoFixture.Create<string>(), _autoFixture.Create<string>());

            //Assert
           ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();
        }

    }
}
