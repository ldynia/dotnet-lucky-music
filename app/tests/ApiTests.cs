// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;


// public class IntegrationTest
// {
//     protected readonly HttpClient TestClient;
    
//     protected IntegrationTest()
//     {
//         var appFactory = new WebApplicationFactory<Program>();
//         appFactory = appFactory.CreateClient();
//     }

//     public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
//     {
//         // Act
//         var response = await TestClient.GetAsync("/api/v1/music/recommend");

//         // Assert
//         // Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
//         Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
//     }
// }