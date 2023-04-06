using assignment1.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // Add all controllers with a predefined Controller Class name and added as a service in the DI;
var app = builder.Build();
app.MapControllers(); //Best shortcut way to map all the controllers in the route

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
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Run();
