using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Learning_CRUD_dotnetcore.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }
        [Route("persons/index")]
        [Route("/")]
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
