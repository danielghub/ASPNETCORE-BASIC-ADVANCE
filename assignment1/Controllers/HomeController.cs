using assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace assignment1.Controllers
{
    public class HomeController : Controller
    {
        //Access the current environment 
        private readonly IWebHostEnvironment _webHostEnvironment;

        private const string _apiRoute = "/api";
        public HomeController(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
        }

        [Route(_apiRoute + "/index")]
        public IActionResult Index(Person? person)
        {
            string errors = string.Empty;
            if (!ModelState.IsValid)
            {
              
                foreach (var item in ModelState.Values)
                {
                    errors += item.Errors[0].ErrorMessage.ToString();
                }
            }
            return Content(errors);
        }
    }
}
