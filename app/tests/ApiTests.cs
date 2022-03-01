using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// TODO: Make UnitTest
// - Code coverage
// - Conncet to Redis
// - Security scann
// - Dockerize https://docs.docker.com/samples/dotnetcore/
// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
// https://jakeydocs.readthedocs.io/en/latest/testing/integration-testing.html#

public class APITests
{
    private readonly HttpClient client;

    public APITests()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        client = application.CreateClient();
    }

    [Theory]
    [InlineData("/api/v1/music/recommend")]
    public async Task IntegrationTest(string url)
    {
        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}