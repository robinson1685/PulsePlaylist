using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace PulsePlaylist.Integration.Tests.Infrastructure;

public abstract class ApiTestBase : IntegrationTestBase
{
    protected ApiTestBase() : base("apiservice")
    {
        LoginAsync().GetAwaiter().GetResult();
    }

    private async Task LoginAsync()
    {
        var loginRequest = new
        {
            Email = "administrator",
            Password = "P@ssw0rd!"
        };

        var response = await HttpClient.PostAsJsonAsync("/login?useCookies=true", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK, "Login should succeed");
    }
}
