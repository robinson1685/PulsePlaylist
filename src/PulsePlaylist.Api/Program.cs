using System.Text.Json.Serialization;
using PulsePlaylist.Api;
using PulsePlaylist.Application;
using PulsePlaylist.Application.Common.Services;
using PulsePlaylist.Domain.Identities;
using PulsePlaylist.Infrastructure;
using PulsePlaylist.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using Microsoft.OpenApi;
using PulsePlaylist.Api.Identity;
using Microsoft.Extensions.FileProviders;
using PulsePlaylist.Api.Endpoints;
using PulsePlaylist.Infrastructure.Configurations;
using Microsoft.AspNetCore.Http.Features;
using PulsePlaylist.Api.ExceptionHandlers;
using PulsePlaylist.Api.Webpushr;

var builder = WebApplication.CreateBuilder(args);


builder.RegisterSerilog();

builder.Services.Configure<WebpushrOptions>(builder.Configuration.GetSection(WebpushrOptions.Key));



builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Add session support for OAuth state parameter
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId") ?? string.Empty;
    googleOptions.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret") ?? string.Empty; ;
})
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        if (activity != null)
        {
            context.ProblemDetails.Extensions.TryAdd("traceId", activity.Id);
        }
    };
});
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();
builder.Services.AddAntiforgery();
// add a CORS policy for the client
var allowedCorsOrigins = builder.Configuration.GetValue<string>("AllowedCorsOrigins")?
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    ?? new[] { "https://localhost:7341", "https://localhost:7123", "https://PulsePlaylist.blazorserver.com" };
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins(allowedCorsOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));


builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo<IEndpointRegistrar>())
    .As<IEndpointRegistrar>()
    .WithScopedLifetime());

builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
    options.UseCookieAuthentication();
    options.UseExamples();
});
builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Don't serialize null values
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // Pretty print JSON
    options.SerializerOptions.WriteIndented = true;
});
builder.Services.AddServiceDiscovery();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();
await app.InitializeDatabaseAsync();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseCors("wasm");
app.MapEndpointDefinitions();
app.UseAntiforgery();
// Add session middleware
app.UseSession();
app.Use(async (context, next) =>
{
    var currentUserContextSetter = context.RequestServices.GetRequiredService<ICurrentUserContextSetter>();
    try
    {
        currentUserContextSetter.SetCurrentUser(context.User);
        await next.Invoke();
    }
    finally
    {
        currentUserContextSetter.Clear();
    }
});

app.MapDefaultEndpoints();
app.MapIdentityApi<ApplicationUser>();
app.MapIdentityApiAdditionalEndpoints<ApplicationUser>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"files")))
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"files"));
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"files")),
    RequestPath = new PathString("/files")
});
await app.RunAsync();
