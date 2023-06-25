using assignment1.Middleware;
using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); // Add all controllers with a predefined Controller Class name and added as a service in the DI;
builder.Services.AddMvcCore();
//Best shortcut way to map all the controllers in the route

//add services into IoC container
//Pattern for injecting a service to DI,
//(Interface, object) from which where you need to assign the interface to an object after its creation to the IoC
//builder.Services.AddSingleton<IPersonsService, PersonService>(); //Singleton to have this service available at a lifetime of the application
//builder.Services.AddSingleton<ICountryService,CountriesService>();
builder.Services.AddScoped<IPersonsService, PersonService>(); //Singleton to have this service available at a lifetime of the application
builder.Services.AddScoped<ICountryService, CountriesService>();
//Inject DBcontext
//By Default, an EF is configured Scoped Service. We need to update the injection above
builder.Services.AddDbContext<PersonDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //Telling Aspnet core that we'll be using a sql server


/* optional to use to map the controllers
app.UseRouting(); 
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //Detect all controllers and pick up all action methods and map in the routing depedency
});
*/
//app.MapControllerRoute()
//app.UseValidationMiddleware(); //Validation Middleware, custom created middleware
//app.MapGet("/", () => "Hello World!");
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MapControllers();
app.UseRouting();
app.MapControllers();

app.Run();
