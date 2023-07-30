using Entities;

namespace RepositoryContracts
{
    /// <summary>
    ///  Represents data access logic for managing Person Entity
    /// </summary>
    public interface ICountriesRepository
    {
        /// <summary>
        /// Adds a new country object to the data store
        /// </summary>
        /// <param name="country">Country object to add</param>
        /// <returns>Returns the country object after adding it to the data store</returns>
        Task<Country> AddCountry(Country country);
            /// <summary>
            /// Get all country object
            /// </summary>
            /// <returns>List of Countries from the table</returns>
        Task<List<Country>> GetAllCountries();
        /// <summary>
        /// Returns a country object based on the given country id; otherwise, null
        /// </summary>
        /// <param name="countryId">CountryId to search</param>
        /// <returns>Matching country or null</returns>
        Task<Country?> GetCountryByCountryId(Guid? countryId);
        /// <summary>
        /// a country retrieved by countryName
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        Task<Country?> GetCountryByCountryName(string countryName);
    }
}