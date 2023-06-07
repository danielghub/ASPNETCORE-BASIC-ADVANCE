using System;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;

namespace Services
{
    public class PersonService : IPersonsService
    {
        //data store
        private readonly List<Person> _persons;
        private readonly ICountryService _countryService;
        public PersonService(bool initialize = true)
        {
            _countryService = new CountriesService();
            _persons = new List<Person>();
            if (initialize)
            {
                _persons.AddRange(new List<Person>()
                {
                   
                  
                    new Person(){ PersonId = Guid.Parse("BDD9A40F-0333-4AB2-A928-27BBCDDFF99A"), 
                        PersonName = "Radcliffe", 
                        Gender = "Male", 
                        Email ="rbeckles0@yahoo.com", 
                        DateOfBirth = DateTime.Parse("4/9/2023"), 
                        Address = "Room 1131", ReceiveNewsLetters = 
                        false, 
                        CountryID = Guid.Parse("270AA19E-30A5-451E-9BD8-52D14095F0E3")},

                     new Person(){ PersonId = Guid.Parse("E2A1DCF7-1085-4284-BF91-2BBE2D73868C"),
                        PersonName = "Maurie",
                        Gender = "Male",
                        Email ="mladds1@diigo.com",
                        DateOfBirth = DateTime.Parse("1/31/2023"),
                        Address = "Apt 1024", ReceiveNewsLetters =
                        false,
                        CountryID = Guid.Parse("E7AB4067-A0DD-4331-8111-D550E05F408C")},

                      new Person(){ PersonId = Guid.Parse("E4FAB614-B871-44E3-8501-0C23406D31C1"),
                          PersonName = "Hamilton",
                        Gender = "Female",
                        Email ="halthrop2@nytimes.com",
                        DateOfBirth = DateTime.Parse("4/9/1995"),
                        Address = "Room 11321", ReceiveNewsLetters =
                        false,
                        CountryID = Guid.Parse("59676058-EFAA-4B0C-AC9B-1081C30BEC40")},

                       new Person(){ PersonId = Guid.Parse("44156368-ECBB-4E48-9D24-2989F7C61F39"),
                        PersonName = "Jenny",
                        Gender = "Female",
                        Email ="JennyPa@yahoo.com",
                        DateOfBirth = DateTime.Parse("9/9/1992"),
                        Address = "Room 192 St ptr sqr", ReceiveNewsLetters =
                        false,
                        CountryID = Guid.Parse("DC16272C-CA08-4ECD-A692-76A15B8B3408")},

                        new Person(){ PersonId = Guid.Parse("8CBBD3EC-615F-4DE2-80C0-666E5ECD051D"),
                        PersonName = "Randolp",
                        Gender = "Male",
                        Email ="Rndyopl@yahoo.com",
                        DateOfBirth = DateTime.Parse("1/9/1973"),
                        Address = "Blk 9 lot 2", ReceiveNewsLetters =
                        false,
                        CountryID = Guid.Parse("4D5EA2CB-53DA-4601-A7B4-C03D496C85E5")},

                         new Person(){ PersonId = Guid.Parse("7B103D06-F0F3-40DD-8C8A-6B923250A6BE"),
                        PersonName = "Carter",
                        Gender = "Male",
                        Email ="Carter@yahoo.com",
                        DateOfBirth = DateTime.Parse("4/9/1990"),
                        Address = "carter 1131", ReceiveNewsLetters =
                        false,
                        CountryID = Guid.Parse("299881B2-0EB6-43A3-BDE2-68DF2F2E972E")},

                      

                    

                });
            }
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
            _persons.Add(person);
        
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(x => x.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null) return null;

            Person? person = _persons.FirstOrDefault(x => x.PersonId == personID);

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
           Person? matchingPerson =  _persons.FirstOrDefault(temp => temp.PersonId == personUpdateRequest.PersonID);

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

            Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personID);

            if (person == null)
                return false;

            _persons.RemoveAll(temp => temp.PersonId == personID);

            return true;
        }
    }
}
