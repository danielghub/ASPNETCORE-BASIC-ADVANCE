using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO class that us ysed as return type of most methods of person service
    /// </summary>
    public class PersonResponse
    {
        public string? PersonName { get; set; }
        public Guid? PersonId { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
        public Guid? CountryId { get; set; }
        //public string? Country { get; set; }
        public double? Age { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        public string? Address { get; set; }

        /// <summary>
        /// Override the equals method to meet our requirements
        /// by default, contains method from assert or any method calls the equals method to
        /// just compare the objs
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;

            return PersonId == person.PersonId && 
                PersonName == person.PersonName &&
                Email == person.Email &&
                DateOfBirth == person.DateOfBirth &&
                Gender == person.Gender &&
                CountryId == person.CountryId;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"PersonId: {PersonId} \n PersonName: {PersonName} \n Email: {Email} \n DateOfBirth: {DateOfBirth} \n Gender: {Gender} \n CountryId: {CountryId}"; 
        }
        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                Address = Address,
                CountryId = CountryId,
                DateOfBirth = DateOfBirth,
                PersonID = PersonId,
                Email = Email,
                PersonName = PersonName,
                ReceiveNewsLetter = ReceiveNewsLetter,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true)

            };
        }
    }

    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonId = person.PersonId,
                Address = person.Address,
                Gender = person.Gender,
                CountryId = person.CountryID,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                PersonName = person.PersonName,
                ReceiveNewsLetter = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }
}
