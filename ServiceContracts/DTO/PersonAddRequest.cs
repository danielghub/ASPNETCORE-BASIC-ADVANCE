using System;
using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as DTO for inserting a new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="Person Name is required")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        public string? Address { get; set; }
        public string? TIN { get; set; }
        public Person ToPerson()
        {
            return new Person { Address = Address, CountryID = CountryId, DateOfBirth = DateOfBirth, Email = Email, Gender = Gender.ToString(), PersonName = PersonName , ReceiveNewsLetters = ReceiveNewsLetter, TIN = TIN  };
        }
    }
}
