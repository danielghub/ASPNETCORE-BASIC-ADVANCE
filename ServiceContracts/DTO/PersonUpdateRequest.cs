using System;
using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the person details to update
    /// </summary>
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage ="Person Id is Required")]
        public Guid? PersonID { get; set; }
        [Required(ErrorMessage = "Person Name is required")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        public string? Address { get; set; }

        public Person ToPerson()
        {
            return new Person { Address = Address, CountryID = CountryId, DateOfBirth = DateOfBirth, Email = Email, Gender = Gender.ToString(), PersonName = PersonName, ReceiveNewsLetters = ReceiveNewsLetter };
        }
    }
}
