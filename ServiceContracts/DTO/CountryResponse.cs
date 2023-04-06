using System;
using System.Data;
using Entities;


namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most of CountryService Methods
    /// </summary>
    public class CountryResponse
    {
        public Guid? CountryId { get; set; }
        public string? CountryName { get; set; }

        //Override equals method to check properly the types of the objects.
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse country_to_compare = (CountryResponse)obj;

            return this.CountryId == country_to_compare.CountryId 
              && this.CountryName == country_to_compare.CountryName;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse
        (this Country country)
        {
            return new CountryResponse { 
                CountryId = country.CountryId,
                CountryName = country.CountryName };
        }
    }
}
