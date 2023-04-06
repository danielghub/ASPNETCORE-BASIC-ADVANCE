using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace assignment1.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private string _userName;
        private string _password;
        private HttpContext _context;
        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //This is auto generated from the middleware template, you need to check httpContext, this will add in the request pipeline
        public async Task Invoke(HttpContext httpContext)
        {  
            _context = httpContext; 
         
            if(_context.Request.Query.ContainsKey("username") && _context.Request.Query.ContainsKey("password"))
            {
                string _username = _context.Request.Query["username"];
                string _password = _context.Request.Query["password"];
                if(_username == "admin@example.com" && _password == "admin1234")
                {
                    await _context.Response.WriteAsync("Successful Login");
                }
                else
                {
                    await _context.Response.WriteAsync("Invalid Login");
                }
              
            }
          
        }
    }

    
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        private static readonly RequestDelegate _next;
        public static IApplicationBuilder UseValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationMiddleware>();
        }
        public static ValidationMiddleware validationMiddleware(this ValidationMiddleware sampleOnly)
        {

            return new ValidationMiddleware(_next);
        }
    }
}
