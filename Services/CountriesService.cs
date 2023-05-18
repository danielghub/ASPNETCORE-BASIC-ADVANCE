using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountryService
    {
        private readonly List<Country> _countries;
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>() {
                new Country() { CountryId = Guid.Parse("270AA19E-30A5-451E-9BD8-52D14095F0E3"), CountryName = "UK" },
                new Country() { CountryId = Guid.Parse("E7AB4067-A0DD-4331-8111-D550E05F408C"), CountryName = "USA" },
                new Country() { CountryId = Guid.Parse("59676058-EFAA-4B0C-AC9B-1081C30BEC40"), CountryName = "Philippines" },
                new Country() { CountryId = Guid.Parse("DC16272C-CA08-4ECD-A692-76A15B8B3408"), CountryName = "Singapore" },
                  new Country() { CountryId = Guid.Parse("4D5EA2CB-53DA-4601-A7B4-C03D496C85E5"), CountryName = "Taiwan" },
                    new Country() { CountryId = Guid.Parse("299881B2-0EB6-43A3-BDE2-68DF2F2E972E"), CountryName = "Australia" }
            });
            
            }
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
            if(_countries.Where(x => x.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Country name already exists");
            }
            //Convert object from DTO to domain model
            Country country = countryAddRequest.ToCountry();
            country.CountryId = Guid.NewGuid();
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryID)
        {
            if (countryID == null) return null;

           Country? country_response_from_list = _countries.FirstOrDefault(x => x.CountryId == countryID);

            if (country_response_from_list == null) return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}