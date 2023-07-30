using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountryService
    {
        private readonly  ICountriesRepository _countriesRepository;
        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }
        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Parameter can't be null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //Check country name is null
            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest.CountryName));
            }
            //Check Duplicate
            if(_countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName) != null)
            {
                throw new ArgumentException("Country name already exists");
            }
            //Convert object from DTO to domain model
            Country country = countryAddRequest.ToCountry();
            country.CountryId = Guid.NewGuid();
            await _countriesRepository.AddCountry(country);            
            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return (await _countriesRepository.GetAllCountries()).Select(country => country.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountryByCountryId(Guid? countryID)
        {
            if (countryID == null) return null;

           Country? country_response_from_list = await _countriesRepository.GetCountryByCountryId(countryID);

            if (country_response_from_list == null) return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}