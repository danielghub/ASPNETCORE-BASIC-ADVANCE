using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country Entity
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns></returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>a list of object from CountryResponse</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Return a country obj based on the given country ID
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns>matching country as Country response object</returns>
        CountryResponse? GetCountryByCountryId(Guid? countryID);

    }
}