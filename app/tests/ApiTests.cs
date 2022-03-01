using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


public class APITests
// public class APITests : IClassFixture<WebApplicationFactory<Program>>
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
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}