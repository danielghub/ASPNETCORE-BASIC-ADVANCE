 using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountryService _countryService;

        public CountriesServiceTest()
        {
            _countryService = new CountriesService();
        }
        #region Add Country
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null};

            //Act


            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //?Act
                _countryService.AddCountry(request);
            });
        }
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Act


            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //?Act
                _countryService.AddCountry(request);
            });
        }
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };
            //Act

            //_countryService.AddCountry(request1);

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //?Act
                _countryService.AddCountry(request1);
                _countryService.AddCountry(request2);
            });
        }
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };


            //Act
            CountryResponse response = _countryService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = _countryService.GetAllCountries();
             //Assert
             Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
            
        }
        #endregion

        #region GetAllCountries
        [Fact]
        //List of country should be empty by default
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_country_response_list =  _countryService.GetAllCountries();

            //Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "USA"},
                new CountryAddRequest() { CountryName = "UK"}
            };
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(_countryService.AddCountry(country_request));
            }

            List<CountryResponse> country_response_list = _countryService.GetAllCountries();
            foreach (CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, country_response_list);
            }
        }
        #endregion

        #region GetCountryByCountryID
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryID = null;

            //Act
            CountryResponse country_response_from_get_method = _countryService.GetCountryByCountryId(countryID);

            //Assert
            Assert.Null(country_response_from_get_method);
        } 

        public void GetCountryByCountryId_ValidCountryID()
        {
            //Arrange
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "China" };
            CountryResponse country_response_from_add = _countryService.AddCountry(country_add_request);

            //Act
            CountryResponse? country_response_from_get = _countryService.GetCountryByCountryId(country_response_from_add.CountryId);

            //Assert
            Assert.Equal(country_response_from_add, country_response_from_get);
        }

        #endregion
    }
}
