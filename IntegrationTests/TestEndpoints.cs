using Domain.DTOs;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace IntegrationTests
{
    public class TestEndpoints
    {
        [Fact]
        public async Task TestRoot()
        {

            await using var application = new TenantApplication();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/");
            var responseContent = await response.Content.ReadAsStringAsync();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            responseContent.StartsWith('[');
            responseContent.EndsWith(']');
        }
        [Fact]
        public async Task TestOpen()
        {

            await using var application = new TenantApplication();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/openInvoices");
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Parse the JSON string
            IEnumerable<CustomerStatsDTO> results = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CustomerStatsDTO>>(responseContent);

            for (int i = 0; i < results.Count(); i++)
            {
                Assert.True(results.ElementAt(i).CustomerId > 0);
                Assert.True(results.ElementAt(i).Receivables > 0);
            }
        }
        [Fact]
        public async Task ClosedOpen()
        {

            await using var application = new TenantApplication();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/closedInvoices");
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Parse the JSON string
            IEnumerable<CustomerStatsDTO> results = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CustomerStatsDTO>>(responseContent);

            for (int i = 0; i < results.Count(); i++)
            {
                Assert.True(results.ElementAt(i).CustomerId > 0);
                Assert.True(results.ElementAt(i).Receivables > 0);
            }
        }
        [Fact]
        public async Task TestPost()
        {

            await using var application = new TenantApplication();
            using var client = application.CreateClient();

            using var jsonContent = new StringContent("[\r\n{\r\n\"reference\": \"added_via_integration_tests\",\r\n\"currencyCode\": \"GPB\",\r\n\"issueDate\": \"01/09/2022\",\r\n\"openingValue\": 1234.56,\r\n\"paidValue\": 1234.56,\r\n\"dueDate\": \"1/09/2025\",\r\n\"debtorName\": \"debtor added from tests\",\r\n\"debtorReference\": \"fist in hand\",\r\n\"debtorCountryCode\": \"IR\",\r\n\"debtorAddress1\": \"string\", \r\n\"debtorAddress2\": \"string\", \r\n\"debtorTown\": \"string\", \r\n\"debtorState\": \"string\",\r\n\"debtorZip\": \"string\", \r\n\"debtorRegistrationNumber\":\"atatataa\"\r\n}\r\n]\r\n");
            jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await client.PostAsync("/receivables/1", jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            // Parse the JSON string
            Customer results = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(responseContent);
            var lastAdded = results.Recieveables.LastOrDefault();
            lastAdded.Reference.Should().BeEquivalentTo("added_via_integration_tests");


        }
    }
}