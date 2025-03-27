using System.Net;
using FluentAssertions;
using PulsePlaylist.Integration.Tests.Infrastructure;

namespace PulsePlaylist.Integration.Tests.Features.Web;

public class WebApplicationTests : IntegrationTestBase
{
    public WebApplicationTests() : base("blazorweb")
    {
    }

    [Fact]
    public async Task RootEndpoint_ShouldReturnOk()
    {
        // Act
        var response = await HttpClient.GetAsync("/");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, "Root endpoint should be accessible");
    }
}
