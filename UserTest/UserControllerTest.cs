using System.Net.Http.Json;
using System.Text.Json;
using Domain.Models;
using FluentAssertions;

namespace UserTest;

public class ControllerTest(ApiFactory webApplicationFactory)
    : IClassFixture<ApiFactory>
{
    [Fact]
    public async Task AddUser()
    {
        // arrange
        var httpClient = webApplicationFactory.CreateClient();
        var user = new User
        {
            FullName = "alie",
            Password = "test",
            Email = "asdd@fft.com",
            Address = "33-32f-34"
        };

        // act
        var response = await httpClient.PostAsJsonAsync("User/Add", user);
        await response.Content.ReadFromJsonAsync<User>();

        // assert
        response.Should().NotBeNull();
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAllUser()
    {
        //arrange
        const string requestUri = "http://localhost:5201/User/GetAllUser";
        var client = webApplicationFactory.CreateClient();

        //act
        var httpResponseMessage = await client.GetAsync(requestUri);
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<List<User>>(responseContent);
        var counter = items!.Count;

        //assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.EnsureSuccessStatusCode();
        counter.Should().NotBe(0);
    }
}