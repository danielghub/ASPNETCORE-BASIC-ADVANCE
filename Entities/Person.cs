using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
     
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public string? TIN { get; set; }

        public Country? Country { get; set; }

    }
}
