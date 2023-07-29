using Entities;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;
using Moq;
using AutoFixture;
using FluentAssertions;
namespace CRUDTests
{
    public class PersonServiceTest
    {
        private readonly IPersonsService _personService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper; //output the test case like a console
        private readonly IFixture _fixture;
        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            //Supply DBContext Directly with Options Builder as it is required on the PersonDBContext base class.
            //_countryService = new CountriesService(new ApplicationDBContext(new DbContextOptionsBuilder().Options));
            //_personService = new PersonService(new ApplicationDBContext(new DbContextOptionsBuilder().Options), _countryService);         

            _fixture = new Fixture();
            var personsInitialData = new List<Person>();
            var countriesInitialData = new List<Country>();
            //_countryService = new CountriesService
            //    (new ApplicationDBContext
            //    (new DbContextOptionsBuilder<ApplicationDBContext>().Options));
            //we require to add options parameter configured on program.cs
            DbContextMock<ApplicationDBContext> dbContextMock = new DbContextMock<ApplicationDBContext>(new DbContextOptionsBuilder<ApplicationDBContext>().Options);

            ApplicationDBContext dbContext = dbContextMock.Object; //Mock the DB context
            dbContextMock.CreateDbSetMock(temp => temp.Persons, personsInitialData); //Mock the DBset
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
            _countryService = new CountriesService(dbContext);
            _personService = new PersonService(dbContext, _countryService);
            _testOutputHelper = testOutputHelper;
        }
        #region AddPerson
        [Fact]
        public async Task AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act, we can also do assert
            Func<Task> action = (async () =>
            {
                await _personService.AddPerson(personAddRequest);
            });

            await action.Should().ThrowAsync<ArgumentNullException>();

            //Assert.ThrowsAsync<ArgumentNullException>(async () => await _personService.AddPerson(personAddRequest));
            //Assert
        }
        [Fact]
        public async Task AddPerson_PersonNameIsNull()
        {
            PersonAddRequest? personAddRequest = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, null as string).Create();

            Func<Task> action = (async () => await _personService.AddPerson(personAddRequest));

            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task AddPerson_ProperPersonDetails()
        {
            //Arrange
            //fixture will create a dummy data for this testing
            PersonAddRequest? personAddRequest = 
                _fixture
                .Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com") //override the value, case to case if the data is failing on fixture we can do this.
                .Create();
            //Act
            PersonResponse person_response_from_add = await _personService.AddPerson(personAddRequest);
            List<PersonResponse> personResponses = await _personService.GetAllPersons();
            //Assert
            //Assert.True(person_response_from_add.PersonId != Guid.Empty);           
            person_response_from_add.PersonId.Should().NotBe(Guid.Empty);
            // Assert.Contains(person_response_from_add, personResponses);
            personResponses.Should().Contain(person_response_from_add);
        }
        #endregion

        #region GetPersonByPersonID

        [Fact]
        public async Task GetPersonByPersonID_NullPersonID()
        {
            //Arrange
            Guid? PersonId = null;
           //Act
            PersonResponse? person_response_from_get = await _personService.GetPersonByPersonID(PersonId);
            //Assert
            //Assert.Null(person_response_from_get);
            person_response_from_get.Should().BeNull();
           
        }
        [Fact]
        public async Task GetPersonByPersonID_WithPersonID()
        {
            CountryAddRequest country_request = _fixture.Create<CountryAddRequest>(); //if there's no modification or special request on data, we can use Create.
            
            CountryResponse country_response = await _countryService.AddCountry(country_request);

            PersonAddRequest person_request = _fixture
                .Build<PersonAddRequest>().With(temp => temp.Email, "somebody@acc.com").Create();
            //Response from adding of the data
            PersonResponse person_response_from_add = await _personService.AddPerson(person_request);

            //Checking the actual response from the add using get method
            PersonResponse? person_response_from_get = await _personService.GetPersonByPersonID(person_response_from_add.PersonId);
            
            //Assert
           // Assert.Equal(person_response_from_add, person_response_from_get);
            person_response_from_get.Should().Be(person_response_from_add);
        }
        #endregion
        #region GetAllPerson
        [Fact]
        public async Task GetAllPersons_EmptyList()
        {
            List<PersonResponse> person_from_get = await _personService.GetAllPersons();

            //Assert.Empty(person_from_get);
            person_from_get.Should().BeNullOrEmpty();
        }
        [Fact]
        public async Task GetAllPersons_AddFewPerson()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest countryAddRequest2 = _fixture.Create<CountryAddRequest>();

            await _countryService.AddCountry(countryAddRequest1);
            await _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew1@acc.com").Create();
            PersonAddRequest personAddRequest2 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew2@acc.com").Create();
            PersonAddRequest personAddRequest3 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew3@acc.com").Create();
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {
                
                PersonResponse personResponse = await _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
           List<PersonResponse> person_list_from_get = await _personService.GetAllPersons();
            foreach (PersonResponse person_response_from_get in person_list_from_get)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                //Assert.Contains(person_response_from_add, person_list_from_get);
                person_list_from_get.Should().BeEquivalentTo(person_response_list_from_add);
            }
        }
        #endregion
        #region GetFilteredPersons
        [Fact]
        public async Task GetFilteredPersons_EmptySearchText()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest countryAddRequest2 = _fixture.Create<CountryAddRequest>();

            await _countryService.AddCountry(countryAddRequest1);
            await _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew1@acc.com").Create();
            PersonAddRequest personAddRequest2 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew2@acc.com").Create();
            PersonAddRequest personAddRequest3 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew3@acc.com").Create();
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {

                PersonResponse personResponse = await _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> person_list_from_search = await _personService.GetFilteredPersons(nameof(Person.PersonName), "");
            foreach (PersonResponse person_response_from_get in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {


                //Assert.Contains(person_response_from_add, person_list_from_search);
                person_list_from_search.Should().Contain(person_response_from_add);
            }
        }
        [Fact]
        public async Task GetFilteredPersons_SearchByPerson()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest countryAddRequest2 = _fixture.Create<CountryAddRequest>();

            await _countryService.AddCountry(countryAddRequest1);
            await _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someonefew1@acc.com")               
                .Create();
            PersonAddRequest personAddRequest2 = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someonefew2@acc.com")
                .With(temp => temp.PersonName, "SomeName1")
                .With(temp => temp.CountryId, personAddRequest1.CountryId)
                .Create();
            PersonAddRequest personAddRequest3 = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someonefew3@acc.com")
                .With(temp => temp.PersonName, "SomeName2")
                .With(temp => temp.CountryId, personAddRequest2.CountryId)
                .Create();
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {

                PersonResponse personResponse = await _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> person_list_from_search = await _personService.GetFilteredPersons(nameof(Person.PersonName), "SomeName1");
            foreach (PersonResponse person_response_from_get in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                //if (person_response_from_add.PersonName.Contains("SomeName1", StringComparison.OrdinalIgnoreCase))
                //{
                //    Assert.Contains(person_response_from_add, person_list_from_search);
                //}
                person_list_from_search.Should().OnlyContain(temp => temp.PersonName.Contains("SomeName1", StringComparison.OrdinalIgnoreCase));

            }
        }
        #endregion
        #region GetSortedPersons
        [Fact]
        public async Task GetSortedPersons()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest countryAddRequest2 = _fixture.Create<CountryAddRequest>();

            await _countryService.AddCountry(countryAddRequest1);
            await _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew1@acc.com").Create();
            PersonAddRequest personAddRequest2 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew2@acc.com").Create();
            PersonAddRequest personAddRequest3 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew3@acc.com").Create();
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2, personAddRequest3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse personResponse = await _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }           
            
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> allPersons = await _personService.GetAllPersons();
            _testOutputHelper.WriteLine("Actual:");
            List<PersonResponse> person_list_from_sort = await _personService.GetSortedPersons(allPersons, nameof(Person.Gender), SortOrderOptions.ASC);
            foreach (PersonResponse person_response_from_sort in person_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_sort.ToString());
            }

            person_response_list_from_add = person_response_list_from_add.OrderBy(temp => temp.PersonName).ToList();


            //Assert
            //for (int i = 0; i < person_response_list_from_add.Count; i++)
            //{
            //    //Assert.Equal(person_response_list_from_add[i], person_list_from_sort[i]);
            //    person_list_from_sort[i].Should().BeEquivalentTo(person_response_list_from_add[i]);
            //}
            person_list_from_sort.Should().BeEquivalentTo(person_response_list_from_add); //Option 1
            // person_list_from_sort.Should().BeInAscendingOrder(temp => temp.PersonName);
        }
        #endregion

        #region UpdatePerson

        [Fact]
        public async Task UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            //Assert
           //await Assert.ThrowsAsync<ArgumentNullException>(async () =>
           // {

           //     await _personService.UpdatePerson(personUpdateRequest);
           // });

            Func<Task> action = (async () => await _personService.UpdatePerson(personUpdateRequest));

           await action.Should().ThrowAsync<ArgumentNullException>();
        }
        //When supplying an invalid person id, should throw argumentexception
        [Fact]
        public async Task UpdatePerson_InvalidPersonId()
        {
            PersonUpdateRequest? personUpdateRequest = _fixture.Build<PersonUpdateRequest>().With(temp => temp.Email, "someonefew1@acc.com").Create();

            //Assert
            //await Assert.ThrowsAsync<ArgumentException>(async () =>
            //{

            //    await _personService.UpdatePerson(personUpdateRequest);
            //});

            Func<Task> action = (async () => await _personService.UpdatePerson(personUpdateRequest));

            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task UpdatePerson_PersonNameIsNull()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();
           

            await _countryService.AddCountry(countryAddRequest1);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew4@acc.com").Create();
         
            PersonResponse person_response_from_add = 
            await _personService.AddPerson(personAddRequest1);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;
            
            //await Assert.ThrowsAsync<ArgumentException>(async () =>
            //{
            //    await _personService.UpdatePerson(person_update_request);
            //});
            Func<Task> action = (async () => await _personService.UpdatePerson(person_update_request));
            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task UpdatePerson_PersonFullDetailsUpdation()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();


            await _countryService.AddCountry(countryAddRequest1);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew6@acc.com").Create();

            PersonResponse person_response_from_add =
            await _personService.AddPerson(personAddRequest1);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";
            person_update_request.Email = "NewSomeMail@sg.com";
            PersonResponse person_response_from_update =  await _personService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = await _personService.GetPersonByPersonID(person_response_from_update.PersonId);

            //Assert
            //Assert.Equal(person_response_from_get, person_response_from_update);
            person_response_from_update.Should().Be(person_response_from_get);

        }
        #endregion
        #region DeletePerson

        [Fact]
        public async Task DeletePerson_ValidPersonId()
        {
            //Arrang
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();


            await _countryService.AddCountry(countryAddRequest1);

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someonefew7@acc.com").Create();

            PersonResponse person_response_from_add =
            await _personService.AddPerson(personAddRequest1);


            bool isDeleted = await _personService.DeletePerson(person_response_from_add.PersonId);
            //Assert
            //Assert.True(isDeleted);
            isDeleted.Should().BeTrue();

        }
        [Fact]
        public async Task DeletePerson_InvalidPersonId()
        {         
            //Act
            bool isDeleted = await _personService.DeletePerson(Guid.NewGuid());
            //Assert
            //Assert.False(isDeleted);
            isDeleted.Should().BeFalse();

        }
        #endregion
    }
}
