
using MudBlazor.Services;
using MudBlazor;
using Blazored.LocalStorage;
using PulsePlaylist.ClientApp.Services.Interfaces;
using PulsePlaylist.ClientApp.Services.UserPreferences;
using PulsePlaylist.ClientApp.Services;
using PulsePlaylist.ClientApp.Configurations;
using PulsePlaylist.ClientApp.Services.Identity;
using PulsePlaylist.ClientApp.Services.JsInterop;
using PulsePlaylist.Api.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Serialization.Form;
using Microsoft.Kiota.Serialization.Json;
using Microsoft.Kiota.Serialization.Multipart;
using Microsoft.Kiota.Serialization.Text;
using PulsePlaylist.ClientApp.Services.PushNotifications;
using PulsePlaylist.ClientApp.Services.Products;
using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace PulsePlaylist.ClientApp;

public static class DependencyInjection
{
    public static void TryAddMudBlazor(this IServiceCollection services, IConfiguration config)
    {
        #region register MudBlazor.Services
        services.AddMudServices(config =>
        {
            MudGlobal.InputDefaults.ShrinkLabel = true;
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
        services.AddMudPopoverService();
        services.AddMudBlazorSnackbar();
        services.AddMudBlazorDialog();
        services.AddMudLocalization();
        services.AddBlazoredLocalStorage();
        services.AddScoped<IStorageService, LocalStorageService>();
        services.AddScoped<IUserPreferencesService, UserPreferencesService>();
        services.AddScoped<LayoutService>();
        services.AddScoped<DialogServiceHelper>();
        #endregion
    }


    public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {

        // Cookie and Authentication Handlers
        services.AddTransient<CookieHandler>();
        services.AddTransient<WebpushrAuthHandler>();

        // Scoped Services
        services.AddSingleton<LanguageService>();
        services.AddSingleton<WebpushrOptionsCache>();
        services.AddScoped<UserProfileStore>();
        services.AddScoped<OnlineStatusInterop>();
        services.AddScoped<OfflineModeState>();
        services.AddScoped<IndexedDbCache>();
        services.AddScoped<ProductCacheService>();
        services.AddScoped<ProductServiceProxy>();
        services.AddScoped<OfflineSyncService>();
        services.AddScoped<IWebpushrService, WebpushrService>();

        // Configuration
        var clientAppSettings = configuration.GetSection(ClientAppSettings.KEY).Get<ClientAppSettings>();
        services.AddSingleton(clientAppSettings!);

        // MudBlazor Integration
        services.TryAddMudBlazor(configuration);
    }

    public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        // HttpClient Registration
        services.AddHttpClient("apiservice", (sp, options) =>
        {
            var settings = sp.GetRequiredService<ClientAppSettings>();
            options.BaseAddress = new Uri(settings.ServiceBaseUrl);
        }).AddHttpMessageHandler<CookieHandler>();

        services.AddHttpClient("Webpushr", client =>
        {
            client.BaseAddress = new Uri("https://api.webpushr.com");
        }).AddHttpMessageHandler<WebpushrAuthHandler>();

        // ApiClient
        services.AddScoped<ApiClient>(sp =>
        {
            ApiClientBuilder.RegisterDefaultSerializer<JsonSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<TextSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<FormSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<MultipartSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<JsonParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<TextParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<FormParseNodeFactory>();

            var settings = sp.GetRequiredService<ClientAppSettings>();
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("apiservice");
            var authProvider = new AnonymousAuthenticationProvider();
            var requestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
            var apiClient = new ApiClient(requestAdapter);

            if (!string.IsNullOrEmpty(settings.ServiceBaseUrl))
            {
                requestAdapter.BaseUrl = settings.ServiceBaseUrl;
            }

            return apiClient;
        });

        // ApiClient Service
        services.AddScoped<ApiClientServiceProxy>();
    }

    public static void AddAuthenticationAndLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        // Authentication and Authorization
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
        services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
        services.AddScoped(sp => (ISignInManagement)sp.GetRequiredService<AuthenticationStateProvider>());

        // Localization
        services.AddLocalization(options => options.ResourcesPath = "Resources");
    }

    public static async Task InitializeCultureAsync(this WebAssemblyHost app, string storageKey = "_Culture")
    {
        var storageService = app.Services.GetRequiredService<IStorageService>();
        var languageCode = await storageService.GetItemAsync<string>(storageKey);
        var culture = new CultureInfo(languageCode ?? CultureInfo.CurrentCulture.Name);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
