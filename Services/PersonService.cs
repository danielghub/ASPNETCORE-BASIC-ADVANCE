﻿using System;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PersonService : IPersonsService
    {
        //data store
        private readonly ICountryService _countryService;
        private readonly PersonDBContext _db;
        public PersonService(PersonDBContext personDBContext, ICountryService countryService)
        {
            _countryService = countryService;
            _db = personDBContext;           
        }
        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryByCountryId(person.CountryID)?.CountryName;
            return personResponse;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
           
            if(personAddRequest == null) { throw new ArgumentNullException(nameof(PersonAddRequest)); };

            if(personAddRequest.PersonName == null)
            {
                throw new ArgumentException("PersonName can't be blank");
            }
            ValidationHelper.ModelValidation(personAddRequest);
            Person person = personAddRequest.ToPerson();
            person.PersonId = Guid.NewGuid();
            _db.Persons.Add(person);
        
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _db.Persons.ToList().Select(x => ConvertPersonToPersonResponse(x)).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null) return null;

            Person? person = _db.Persons.FirstOrDefault(x => x.PersonId == personID);

            if (person == null) return null;

            return person.ToPersonResponse();
        }
       
        public List<PersonResponse> GetFilteredPersons(string searchBy, string searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> MatchingPersons = allPersons;

            if(String.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return MatchingPersons;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    MatchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.PersonName) ?
                    temp.PersonName.Contains(searchString,StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(PersonResponse.Email):
                    MatchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Email) ?
                   temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(PersonResponse.DateOfBirth):
                    MatchingPersons = allPersons.Where(temp => (temp.DateOfBirth != null) ?
                   temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(PersonResponse.Gender):
                    MatchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Gender) ?
                   temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(PersonResponse.CountryId):
                    MatchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Country) ?
                   temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    MatchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Address) ?
                   temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                default:
                    MatchingPersons = allPersons;
                    break;
            }
            return MatchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string? sortBy, SortOrderOptions sortOrder)
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

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));
            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);
           Person? matchingPerson =  _db.Persons.FirstOrDefault(temp => temp.PersonId == personUpdateRequest.PersonID);

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

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personID)
        {

            if (personID == null)
                throw new ArgumentNullException(nameof(personID));

            Person? person = _db.Persons.FirstOrDefault(temp => temp.PersonId == personID);

            if (person == null)
                return false;

            //_db.Persons.ExecuteDelete(temp => temp.PersonId == personID);

            return true;
        }
    }
}
