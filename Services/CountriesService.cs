using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountryService
    {
        private readonly  PersonDBContext _db;
        public CountriesService(PersonDBContext personDBContext)
        {
            _db = personDBContext;
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
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
            _db.Add(country);
            _db.SaveChanges();
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _db.Countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryID)
        {
            if (countryID == null) return null;

           Country? country_response_from_list = _db.Countries.FirstOrDefault(x => x.CountryId == countryID);

            if (country_response_from_list == null) return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}