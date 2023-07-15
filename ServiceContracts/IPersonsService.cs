using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        /// <summary>
        /// Represent business logic for manipulating person entity
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);
        /// <summary>
        /// Return all persons
        /// </summary>
        /// <returns></returns>
        Task<List<PersonResponse>> GetAllPersons();
        /// <summary>
        /// Returns the person object based on the given person id
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        Task<PersonResponse?> GetPersonByPersonID(Guid? personID);
        /// <summary>
        /// Returns all person object that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching persons based on the given search field and search string</returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string searchString);
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personUpdateRequest">Person details to update, including person id</param>
        /// <returns>Returns the person response object</returns>
       Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest);
        /// <summary>
        /// Deletes a person based on person id
        /// </summary>
        /// <param name="PersonID">Supply a valid person Id</param>
        /// <returns>true or false</returns>
        Task<bool> DeletePerson(Guid? PersonID);
    }
}
