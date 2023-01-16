using assignment1.Middleware;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
//app.MapControllerRoute()
app.UseValidationMiddleware(); //Validation Middleware
app.MapGet("/", () => "Hello World!");

app.Run();
