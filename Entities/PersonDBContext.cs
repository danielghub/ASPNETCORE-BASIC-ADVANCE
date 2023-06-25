using Microsoft.Data.SqlClient;
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

        public List<Person> sp_GetAllPersons()
        {
            //Execute the stored prc through DBSet Iqueryable
           return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }
        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@PersonID", person.PersonId),
            new SqlParameter("@PersonName", person.PersonName),
            new SqlParameter("@Email", person.Email),
            new SqlParameter("@DateOfBirth", person.DateOfBirth),
            new SqlParameter("@Gender", person.Gender),
            new SqlParameter("@CountryID", person.CountryID),
            new SqlParameter("@Address", person.Address),
            new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)};

           return Database.ExecuteSqlRaw("Execute [dbo].[InsertPerson] @PersonID, @PersonName" +
                "@Email, @DateOfBirth, @Gender,@CountryID,@Address,@ReceiveNewsLetters",parameters);
        }
    }
}
