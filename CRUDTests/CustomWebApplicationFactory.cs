using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    //Generally accessing the program cs file to use for integration test, this is how we setup
    public class CustomWebApplicationFactory : WebApplicationFactory<Program> //reference a WebApplicationFactory, to call the Program from program.cs
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) //override the configurewebhost to access the Webhost builder programatically
        {
            base.ConfigureWebHost(builder);
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services => { 
                  var descripter =  services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<ApplicationDBContext>)); //import entites and frameworkcore
                    
                if(descripter != null) { 
                services.Remove(descripter); //making sure that the existing service is deleted, not use the original ApplicationDBContext because we are doing testing
                }
                services.AddDbContext<ApplicationDBContext>(options => {
                    options.UseInMemoryDatabase("DatabaseForTesting"); // each execution/run, it will create a new database that runs in memory.
                
                }); //import in memory framework core and extension dpendency injection
            });
        }
    }
}
