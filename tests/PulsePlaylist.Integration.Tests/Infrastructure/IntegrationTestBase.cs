using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;
using Projects;

namespace PulsePlaylist.Integration.Tests.Infrastructure;

public abstract class IntegrationTestBase : IAsyncDisposable
{
    protected readonly HttpClient HttpClient;
    protected readonly DistributedApplication App;
    protected readonly ResourceNotificationService ResourceNotificationService;

    protected IntegrationTestBase(string serviceName)
    {
        var appHost = DistributedApplicationTestingBuilder.CreateAsync<PulsePlaylist_AppHost>().GetAwaiter().GetResult();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        App = appHost.BuildAsync().GetAwaiter().GetResult();
        ResourceNotificationService = App.Services.GetRequiredService<ResourceNotificationService>();
        App.StartAsync().GetAwaiter().GetResult();

        HttpClient = App.CreateHttpClient(serviceName);

        ResourceNotificationService
            .WaitForResourceAsync(serviceName, KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30))
            .GetAwaiter()
            .GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        if (HttpClient != null)
        {
            HttpClient.Dispose();
        }

        if (ResourceNotificationService != null)
        {
            ResourceNotificationService.Dispose();
        }

        if (App != null)
        {
            await App.DisposeAsync();
        }
    }
}
