using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServiceTest
    {
        private readonly IPersonsService _personService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper; //output the test case like a console
     
        public PersonServiceTest(ITestOutputHelper testOutputHelper,PersonDBContext personDBContext)
        {
            //Supply DBContext Directly with Options Builder as it is required on the PersonDBContext base class.
            _countryService = new CountriesService(new PersonDBContext(new DbContextOptionsBuilder().Options));
            _personService = new PersonService(new PersonDBContext(new DbContextOptionsBuilder().Options), _countryService);         
            _testOutputHelper = testOutputHelper;
        }
        #region AddPerson
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act, we can also do assert
            
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddRequest));
            //Assert
        }
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };

            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddRequest));
        }
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                  PersonName = "SomeName"
                , Address = "Some Address"
                , CountryId = Guid.NewGuid()
                , DateOfBirth = DateTime.UtcNow
                , Email = "some@email.com"
                , Gender = GenderOptions.Male
                , ReceiveNewsLetter = true
            };
            //Act
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);
            List<PersonResponse> personResponses = _personService.GetAllPersons();
            //Assert
            Assert.True(person_response_from_add.PersonId != Guid.Empty);
            Assert.Contains(person_response_from_add, personResponses);
        }
        #endregion

        #region GetPersonByPersonID

        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            //Arrange
            Guid? PersonId = null;
           //Act
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(PersonId);
            //Assert
            Assert.Null(person_response_from_get);
           
        }
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            CountryAddRequest country_request = new CountryAddRequest() { CountryName = "Canada" };
            
            CountryResponse country_response = _countryService.AddCountry(country_request);

            PersonAddRequest person_request = new PersonAddRequest()
            {
                Address = "Some address..",
                CountryId = country_response.CountryId,
                DateOfBirth = new DateTime(),
                Gender = GenderOptions.Other,
                Email = "sample@email.com",
                PersonName = "Some person name",
                ReceiveNewsLetter = false
            };
            //Response from adding of the data
            PersonResponse person_response_from_add = _personService.AddPerson(person_request);

            //Checking the actual response from the add using get method
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_add.PersonId);
            
            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }
        #endregion
        #region GetAllPerson
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            List<PersonResponse> person_from_get = _personService.GetAllPersons();

            Assert.Empty(person_from_get);
        }
        [Fact]
        public void GetAllPersons_AddFewPerson()
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Philippines" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Japan" };

            _countryService.AddCountry(countryAddRequest1);
            _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "SomeName"
                ,
                Address = "Some Address"
                ,
                CountryId = Guid.NewGuid()
                ,
                DateOfBirth = DateTime.UtcNow
                ,
                Email = "some@email.com"
                ,
                Gender = GenderOptions.Male
                ,
                ReceiveNewsLetter = true
            };
            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "SomeName"
              ,
                Address = "Some Address"
              ,
                CountryId = Guid.NewGuid()
              ,
                DateOfBirth = DateTime.UtcNow
              ,
                Email = "some@email.com"
              ,
                Gender = GenderOptions.Male
              ,
                ReceiveNewsLetter = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {
                
                PersonResponse personResponse = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
           List<PersonResponse> person_list_from_get = _personService.GetAllPersons();
            foreach (PersonResponse person_response_from_get in person_list_from_get)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_get);
            }
        }
        #endregion
        #region GetFilteredPersons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Philippines" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Japan" };

            _countryService.AddCountry(countryAddRequest1);
            _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "SomeName"
                ,
                Address = "Some Address"
                ,
                CountryId = Guid.NewGuid()
                ,
                DateOfBirth = DateTime.UtcNow
                ,
                Email = "some@email.com"
                ,
                Gender = GenderOptions.Male
                ,
                ReceiveNewsLetter = true
            };
            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "SomeName"
              ,
                Address = "Some Address"
              ,
                CountryId = Guid.NewGuid()
              ,
                DateOfBirth = DateTime.UtcNow
              ,
                Email = "some@email.com"
              ,
                Gender = GenderOptions.Male
              ,
                ReceiveNewsLetter = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {

                PersonResponse personResponse = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> person_list_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName),"");
            foreach (PersonResponse person_response_from_get in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_search);
            }
        }
        [Fact]
        public void GetFilteredPersons_SearchByPerson()
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Philippines" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Japan" };

            _countryService.AddCountry(countryAddRequest1);
            _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "SomeName"
                ,
                Address = "Some Address"
                ,
                CountryId = Guid.NewGuid()
                ,
                DateOfBirth = DateTime.UtcNow
                ,
                Email = "some@email.com"
                ,
                Gender = GenderOptions.Male
                ,
                ReceiveNewsLetter = true
            };
            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "SomeName1"
              ,
                Address = "Some Address 2"
              ,
                CountryId = Guid.NewGuid()
              ,
                DateOfBirth = DateTime.UtcNow
              ,
                Email = "some@email.com"
              ,
                Gender = GenderOptions.Male
              ,
                ReceiveNewsLetter = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {

                PersonResponse personResponse = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> person_list_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName), "SomeName1");
            foreach (PersonResponse person_response_from_get in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add.PersonName.Contains("SomeName1", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(person_response_from_add, person_list_from_search);
                }

            }
        }
        #endregion
        #region GetSortedPersons
        [Fact]
        public void GetSortedPersons()
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Philippines" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Japan" };

            _countryService.AddCountry(countryAddRequest1);
            _countryService.AddCountry(countryAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "SomeName"
                ,
                Address = "Some Address"
                ,
                CountryId = Guid.NewGuid()
                ,
                DateOfBirth = DateTime.UtcNow
                ,
                Email = "some@email.com"
                ,
                Gender = GenderOptions.Male
                ,
                ReceiveNewsLetter = true
            };
            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "SomeName1"
              ,
                Address = "Some Address 2"
              ,
                CountryId = Guid.NewGuid()
              ,
                DateOfBirth = DateTime.UtcNow
              ,
                Email = "some@email1.com"
              ,
                Gender = GenderOptions.Male
              ,
                ReceiveNewsLetter = true
            };
            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "SomeName3"
             ,
                Address = "Some Address 3"
             ,
                CountryId = Guid.NewGuid()
             ,
                DateOfBirth = DateTime.UtcNow
             ,
                Email = "some@email3.com"
             ,
                Gender = GenderOptions.Female
             ,
                ReceiveNewsLetter = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse personResponse = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(personResponse);
            }
           
            
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> allPersons = _personService.GetAllPersons();
            _testOutputHelper.WriteLine("Actual:");
            List<PersonResponse> person_list_from_sort = _personService.GetSortedPersons(allPersons, nameof(Person.Gender), SortOrderOptions.ASC);
            foreach (PersonResponse person_response_from_sort in person_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_sort.ToString());
            }

            person_response_list_from_add = person_response_list_from_add.OrderBy(temp => temp.PersonName).ToList();


            //Assert
            for (int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], person_list_from_sort[i]);
            }

        }
        #endregion

        #region UpdatePerson

        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {

                _personService.UpdatePerson(personUpdateRequest);
            });
        }
        //When supplying an invalid person id, should throw argumentexception
        [Fact]
        public void UpdatePerson_InvalidPersonId()
        {
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid()
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {

                _personService.UpdatePerson(personUpdateRequest);
            });
        }
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse country_response_from_add = _countryService.AddCountry(country_add_request);
            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John", CountryId = country_response_from_add.CountryId,Email = "email@cc.com",Address = "Address u ..."
                ,Gender = GenderOptions.Male
            };
            PersonResponse person_response_from_add = 
            _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;
            
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(person_update_request);
            });
            
           
        }
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdation()
        {
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse country_response_from_add = _countryService.AddCountry(country_add_request);
            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryId = country_response_from_add.CountryId,
                Address = "Some Address",
                DateOfBirth = DateTime.Now,
                Email = "SomeEmail@gg.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetter = true
            };
            PersonResponse person_response_from_add =
            _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";
            person_update_request.Email = "NewSomeMail@sg.com";
            PersonResponse person_response_from_update =  _personService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_update.PersonId);

            //Assert
            Assert.Equal(person_response_from_get, person_response_from_update);


        }
        #endregion
        #region DeletePerson

        [Fact]
        public void DeletePerson_ValidPersonId()
        {
            //Arrang
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse country_response_from_add = _countryService.AddCountry(country_add_request);
            //Act
            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryId = country_response_from_add.CountryId,
                Address = "Some Address",
                DateOfBirth = DateTime.Now,
                Email = "SomeEmail@gg.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetter = true
            };
            PersonResponse person_response_from_add =  _personService.AddPerson(person_add_request);

            
            bool isDeleted = _personService.DeletePerson(person_response_from_add.PersonId);
            //Assert
            Assert.True(isDeleted);

        }
        [Fact]
        public void DeletePerson_InvalidPersonId()
        {         
            //Act
            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());
            //Assert
            Assert.False(isDeleted);

        }
        #endregion
    }
}
