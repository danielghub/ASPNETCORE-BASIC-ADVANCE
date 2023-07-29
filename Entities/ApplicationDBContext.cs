using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Maps Country model to Countries table
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


            //Fluet API
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
                .HasColumnName("TaxIdentificationNumber")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("ABC12345");

            //unique constraint on column TIN
            //modelBuilder.Entity<Person>().HasIndex(temp => temp.TIN).IsUnique();
            
            //where CHK_TIN is the name of the constraint and the other condition is to check if the TIN data only has 8 characters
            modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TIN] = 8");
        }           

        public List<Person> sp_GetAllPersons()
        {
            //Execute the stored prc through DBSet Iqueryable
           return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }
        public async Task<int> sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@PersonID", person.PersonId),
            new SqlParameter("@PersonName", person.PersonName),
            new SqlParameter("@Email", person.Email),
            new SqlParameter("@DateOfBirth", person.DateOfBirth),
            new SqlParameter("@Gender", person.Gender),
            new SqlParameter("@CountryID", person.CountryID),
            new SqlParameter("@Address", person.Address),
            new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters),
            new SqlParameter("@TIN", person.TIN)
            };

           return await Database.ExecuteSqlRawAsync("Execute [dbo].[InsertPerson] @PersonID, @PersonName" +
                "@Email, @DateOfBirth, @Gender,@CountryID,@Address,@ReceiveNewsLetters,@TIN", parameters);
        }

    }
}
