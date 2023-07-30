 using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using EntityFrameworkCoreMock;
namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountryService _countryService;

        public CountriesServiceTest()
        {
           var countriesInitialData = new List<Country>();
            //_countryService = new CountriesService
            //    (new ApplicationDBContext
            //    (new DbContextOptionsBuilder<ApplicationDBContext>().Options));
            //we require to add options parameter configured on program.cs
          DbContextMock<ApplicationDBContext> dbContextMock = new DbContextMock<ApplicationDBContext>(new DbContextOptionsBuilder<ApplicationDBContext>().Options);

           ApplicationDBContext applicationDBContext= dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
            _countryService = new CountriesService(null);

            
        }
        #region Add Country
        [Fact]
        public async Task AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null};

            //Act

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //?Act
                await _countryService.AddCountry(request);
            });
        }
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Act


            //Assert
           await  Assert.ThrowsAsync<ArgumentNullException>(async() =>
            {
                //?Act
                await _countryService.AddCountry(request);
            });
        }
        [Fact]
        public async Task AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };
            //Act

            //_countryService.AddCountry(request1);

            //Assert
           await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                //?Act
               await _countryService.AddCountry(request1);
                await _countryService.AddCountry(request2);
            });
        }
        [Fact]
        public async Task AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };


            //Act
            CountryResponse response = await _countryService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = await _countryService.GetAllCountries();
             //Assert
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
            
        }
        #endregion

        #region GetAllCountries
        [Fact]
        //List of country should be empty by default
        public async Task GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_country_response_list =  await _countryService.GetAllCountries();

            //Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public async Task GetAllCountries_AddFewCountries()
        {
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "USA"},
                new CountryAddRequest() { CountryName = "UK"}
            };
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add( await _countryService.AddCountry(country_request));
            }

            List<CountryResponse> country_response_list = await _countryService.GetAllCountries();
            foreach (CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, country_response_list);
            }
        }
        #endregion

        #region GetCountryByCountryID
        [Fact]
        public async Task GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryID = null;

            //Act
            //CountryResponse country_response_from_get_method = 

            //Assert
            Assert.Null(await _countryService.GetCountryByCountryId(countryID));
        }
        [Fact]
        public async Task GetCountryByCountryId_ValidCountryID()
        {
            //Arrange
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "China" };
            CountryResponse country_response_from_add = await _countryService.AddCountry(country_add_request);

            //Act
            CountryResponse? country_response_from_get = await _countryService.GetCountryByCountryId(country_response_from_add.CountryId);

            //Assert
            Assert.Equal(country_response_from_add, country_response_from_get);
        }

        #endregion
    }
}
