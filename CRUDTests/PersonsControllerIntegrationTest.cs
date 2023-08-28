using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    public class PersonsControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public PersonsControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
          _httpClient =  factory.CreateClient(); //holds the reference of the CustomWebApplicationFactory and we use the http client of it. Creates a new object of factory

        }
        [Fact]
        public async Task Index_ToReturnView()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/persons/index");

            response.Should().BeSuccessful();
        }
    }
}
