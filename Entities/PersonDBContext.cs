using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class PersonDBContext : DbContext
    {
        public PersonDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");
            //Seed persons to DB
            string CountriesJson = System.IO.File.ReadAllText("CountryDetails.json");
            List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(CountriesJson);
            foreach (Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }
            //Seed countries to DB
            string PersonsJson = System.IO.File.ReadAllText("PersonDetails.json");
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(PersonsJson);
            foreach (Person person in persons)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
