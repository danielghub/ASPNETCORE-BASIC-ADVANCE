using Entities;
using RepositoryContracts;

namespace Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDBContext _db;
        public CountriesRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public Task<Country> AddCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<List<Country>> GetAllCountries()
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetCountryByCountryId(Guid countryId)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetCountryByCountryName(string countryName)
        {
            throw new NotImplementedException();
        }
    }
}