using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


public class IntegrationTest
// public class IntegrationTest: IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient TestClient;

    protected IntegrationTest()
    {

        var appFactory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            // ... Configure test services
        });
        TestClient = appFactory.CreateClient();
    }

    [Theory]
    [InlineData("/api/v1/music/recommend")]
    public async Task ApiTest(string url)
    {
        // Act
        var response = await TestClient.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}