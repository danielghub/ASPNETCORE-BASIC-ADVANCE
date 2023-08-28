using System;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Microsoft.VisualBasic;

namespace Services
{
    public class PersonService : IPersonsService
    {
        //data store
        //private readonly ICountryService _countryService;
        private readonly IPersonRepository _personsRepository;
        public PersonService(IPersonRepository personsRepository)
        {
            _personsRepository = personsRepository;           
        }
        //private PersonResponse ConvertPersonToPersonResponse(Person person)
        //{
        //    PersonResponse personResponse = person.ToPersonResponse();
        //    personResponse.Country = _countryService.GetCountryByCountryId(person.CountryID)?.CountryName;
        //    return personResponse;
        //}
        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
        {
           
            if(personAddRequest == null) { throw new ArgumentNullException(nameof(PersonAddRequest)); };

            if(personAddRequest.PersonName == null)
            {
                throw new ArgumentException("PersonName can't be blank");
            }
            ValidationHelper.ModelValidation(personAddRequest);
            Person person = personAddRequest.ToPerson();
            person.PersonId = Guid.NewGuid();
            await _personsRepository.AddPerson(person);
            //await _personsRepository.SaveChangesAsync();
            //await _db.sp_InsertPerson(person);


            return person.ToPersonResponse();
            //return ConvertPersonToPersonResponse(person);
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {
            var persons = await _personsRepository.GetAllPersons();

            return persons.Select(x => x.ToPersonResponse()).ToList();
            //7-12-2023 Commented as we added a new column and this proc should be updated
            //return _db.sp_GetAllPersons().Select(temp => ConvertPersonToPersonResponse(temp)).ToList();
        }

        public async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null) return null;

            Person? person = await _personsRepository.GetPersonByPersonID(personID);

            if (person == null) return null;

            return person.ToPersonResponse();
        }
       
        public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string searchString)
        {
            List<Person> allPersons = searchBy switch
            {
                nameof(PersonResponse.PersonName) =>
                await _personsRepository.GetFilteredPersons(temp =>
                temp.PersonName.Contains(searchString)),

                nameof(PersonResponse.Email) =>
                    await _personsRepository.GetFilteredPersons(temp =>
                    temp.Email.Contains(searchString)),

                nameof(PersonResponse.DateOfBirth) =>
                    await _personsRepository.GetFilteredPersons(temp =>
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString)),

                nameof(PersonResponse.Gender) =>
                    await _personsRepository.GetFilteredPersons(temp =>
                    temp.Gender.Contains(searchString)),

                nameof(PersonResponse.CountryId) =>
                    await _personsRepository.GetFilteredPersons(temp =>
                    temp.Country.CountryName.Contains(searchString)),

                nameof(PersonResponse.Address) =>
                    await _personsRepository.GetFilteredPersons(temp =>
                    temp.Address.Contains(searchString)),

                _ => await _personsRepository.GetAllPersons()
            };
            return allPersons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public async Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string? sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Country), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Country), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
                (nameof(PersonResponse.Age), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Age).ToList(),
                (nameof(PersonResponse.Age), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.Age).ToList(),
                (nameof(PersonResponse.Gender), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Gender), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOptions.ASC)
                    => allPersons.OrderBy(temp => temp.ReceiveNewsLetter).ToList(),
                (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOptions.DESC)
                   => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetter).ToList(),
                _ => allPersons
            };
            return allPersons;
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));
            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);
           Person? matchingPerson =  await _personsRepository.GetPersonByPersonID(personUpdateRequest.PersonID);

            if(matchingPerson == null)
            {
                throw new ArgumentException("Given Person id doesnt exist");
            };
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetter;
            await _personsRepository.UpdatePerson(matchingPerson);
            return matchingPerson.ToPersonResponse();
        }

        public async Task<bool> DeletePerson(Guid? personID)
        {

            if (personID == null)
                throw new ArgumentNullException(nameof(personID));

            Person? person = await _personsRepository.GetPersonByPersonID(personID);

            if (person == null)
                return false;

            return await _personsRepository.DeletePersonByPersonID(person.PersonId);
            //_db.Persons.ExecuteDelete(temp => temp.PersonId == personID);

        }
    }
}
