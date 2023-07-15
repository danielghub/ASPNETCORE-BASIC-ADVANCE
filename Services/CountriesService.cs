using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountryService
    {
        private readonly  ApplicationDBContext _db;
        public CountriesService(ApplicationDBContext personDBContext)
        {
            _db = personDBContext;
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
            if(_db.Countries.Count(x => x.CountryName == countryAddRequest.CountryName) > 0)
            {
                throw new ArgumentException("Country name already exists");
            }
            //Convert object from DTO to domain model
            Country country = countryAddRequest.ToCountry();
            country.CountryId = Guid.NewGuid();
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryId(Guid? countryID)
        {
            if (countryID == null) return null;

           Country? country_response_from_list = await _db.Countries.FirstOrDefaultAsync(x => x.CountryId == countryID);

            if (country_response_from_list == null) return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}