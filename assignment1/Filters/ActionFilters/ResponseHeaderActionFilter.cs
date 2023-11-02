using Microsoft.AspNetCore.Mvc.Filters;

namespace Learning_CRUD_dotnetcore.Filters.ActionFilters
{
    public class ResponseHeaderActionFilter : IActionFilter, IOrderedFilter
    {
        //Logger of the current working class ResponseHeaderActionFilter
        private readonly ILogger<ResponseHeaderActionFilter> _logger;
        private readonly string Key;
        private readonly string Value;

        public int Order { get; }
        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string key, string value, int order)
        {
            _logger = logger;
            Key = key;
            Value = value;
            Order = order;
        }
       
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("{FilterName}.{Method} Name method", nameof(ResponseHeaderActionFilter), nameof(OnActionExecuted));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("{FilterName}.{Method} Name method", nameof(ResponseHeaderActionFilter), nameof(OnActionExecuting));

            context.HttpContext.Response.Headers[Key] = Value;
        }
    }
}
