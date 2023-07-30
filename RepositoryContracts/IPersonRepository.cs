using Entities;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    /// <summary>
    /// Represents the data access logic for managing person entity
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Adds a person object in the data store
        /// </summary>
        /// <param name="person">Person to add</param>
        /// <returns>Returns the person object after adding it to the table </returns>
        Task<Person> AddPerson(Person person);
        /// <summary>
        /// Returns all persons in the data store
        /// </summary>
        /// <returns></returns>
        Task<List<Person>> GetAllPersons();
        /// <summary>
        /// Retrieves person object by person ID in the table
        /// </summary>
        /// <param name="personID">Person ID or null</param>
        /// <returns>PersonResponse Object or null</returns>
        Task<Person?> GetPersonByPersonID(Guid? personID);
        /// <summary>
        /// Returns all person object based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>Returns all matching person with given condition</returns>
        Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);
        /// <summary>
        /// Delete a person object based on the person ID
        /// </summary>
        /// <param name="personID">Person ID (guid) to search</param>
        /// <returns>Return true, if deletion is successful; otherwise false</returns>
        Task<bool> DeletePersonByPersonID(Guid personID);
        /// <summary>
        /// Update the person based on the given object
        /// </summary>
        /// <param name="person">Person object to update</param>
        /// <returns>Returns the update person object</returns>
        Task<Person> UpdatePerson(Person person);

    }
}
