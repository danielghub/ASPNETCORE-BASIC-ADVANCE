using Learning_CRUD_dotnetcore.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Learning_CRUD_dotnetcore.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly ICountryService _countryService;

        public PersonsController(IPersonsService personsService, ICountryService countryService)
        {
            _personsService = personsService;
            _countryService = countryService;
        }
        [Route("persons/index")]
        [Route("/")] //"/" means home
        [TypeFilter(typeof(PersonsListActionFilter)) ]
        public async Task<IActionResult> Index(string? searchBy, string? searchString)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.PersonName), "Person Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.Country), "Country" },
                { nameof(PersonResponse.Address), "Address" },
                { nameof(PersonResponse.CountryId), "Country Id" }

            };
            List<PersonResponse> persons = await _personsService.GetFilteredPersons(searchBy, searchString);
            return View(persons);
        }


      
    }
}
