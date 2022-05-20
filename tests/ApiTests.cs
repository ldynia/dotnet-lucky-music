using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace tests;

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

        // Assert status code 200-299 and is json
        response.EnsureSuccessStatusCode(); 
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}