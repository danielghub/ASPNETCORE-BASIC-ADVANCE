using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Domain Modelfor storing country
    /// Should not  be exposed to the presentation Layer
    /// Should follow DTO, data transfer object
    /// </summary>
    public class Country
    {
        [Key]
        public Guid? CountryId { get; set; }
        public string?  CountryName { get; set; }
    }
}