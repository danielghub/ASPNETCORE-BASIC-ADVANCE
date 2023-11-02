using Learning_CRUD_dotnetcore.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace Learning_CRUD_dotnetcore.Filters.ActionFilters
{
    public class PersonsListActionFilter : IActionFilter
    {
        private readonly ILogger<PersonsListActionFilter> _logger;

        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // TODO: add after logic, access viewdata in the context arg
            _logger.LogInformation("The {Filter}.{Methodname}.{Action} has been executed from ", nameof(PersonsListActionFilter), nameof(OnActionExecuted), nameof(ActionExecutedContext));

            //type Cast to access the action args from the controller as it is not possible doing it directly from context obj;
            PersonsController personsController = (PersonsController)context.Controller;

            IDictionary<string, object>? parameters = (IDictionary<string, object>?)context.HttpContext.Items["arguments"];

            if (parameters != null)
            {
                if (parameters.ContainsKey("searchBy"))
                {
                    personsController.ViewData["searchBy"] = parameters["searchBy"];
                }
            }

            personsController.ViewData["searchBy"] = context.HttpContext.Items["arguments"];  //sets the viewData
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // TODO: add before logic
            _logger.LogInformation("");

            //add the logic for accessing the actionArguments on OnActionExecuted via HttpContext
            context.HttpContext.Items["arguments"] = context.ActionArguments;

            if(context.ActionArguments.ContainsKey("searchBy"))
            {
                string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);

                if(searchBy != null || !string.IsNullOrEmpty(searchBy)) //either way, its the same logic
                {
                    var searchByOptions = new List<string>() {                        
                        nameof(PersonResponse.PersonName),
                        nameof(PersonResponse.Email),
                        nameof(PersonResponse.Gender),
                        nameof(PersonResponse.Address),
                        nameof(PersonResponse.Age),
                        nameof(PersonResponse.CountryId),
                        nameof(PersonResponse.Country),
                        nameof(PersonResponse.DateOfBirth)
                    };
                    if(searchByOptions.Any(temp => temp == searchBy) == false)
                    {
                        _logger.LogInformation("The actual value of searchBy is {searchBy}", searchBy); // this is called, structured logging with {}
                         
                        context.ActionArguments["searchBy"] = nameof(PersonResponse.PersonName);
                        _logger.LogInformation("The updated value of searchBy is {searchBy}", context.ActionArguments["searchBy"]);
                    }
                }
            }
        }
    }
}
