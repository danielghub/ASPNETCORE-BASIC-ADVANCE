using Microsoft.AspNetCore.Mvc.Filters;

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
            // TODO: add after logic
            _logger.LogInformation("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // TODO: add before logic
            _logger.LogInformation("OnActionExecuting");

            if(context.ActionArguments.ContainsKey("searchBy"))
            {
                string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);

                if(searchBy != null || !string.IsNullOrEmpty(searchBy))
                {

                }
            }
        }
    }
}
