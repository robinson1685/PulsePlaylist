// <auto-generated/>
#pragma warning disable CS0618
using PulsePlaylist.Api.Client.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace PulsePlaylist.Api.Client.Account.GenerateAuthenticator
{
    /// <summary>
    /// Builds and executes requests for operations under \account\generateAuthenticator
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class GenerateAuthenticatorRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GenerateAuthenticatorRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/account/generateAuthenticator?appName={appName}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GenerateAuthenticatorRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/account/generateAuthenticator?appName={appName}", rawUrl)
        {
        }
        /// <summary>
        /// Generates a shared key and an Authenticator URI for a logged-in user. This endpoint is typically used to configure a TOTP authenticator app, such as Microsoft Authenticator or Google Authenticator.
        /// </summary>
        /// <returns>A <see cref="global::PulsePlaylist.Api.Client.Models.AuthenticatorResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::PulsePlaylist.Api.Client.Models.HttpValidationProblemDetails">When receiving a 400 status code</exception>
        /// <exception cref="global::PulsePlaylist.Api.Client.Models.ProblemDetails">When receiving a 404 status code</exception>
        /// <exception cref="global::PulsePlaylist.Api.Client.Models.ProblemDetails">When receiving a 500 status code</exception>
        public async Task<global::PulsePlaylist.Api.Client.Models.AuthenticatorResponse> GetAsync(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder.GenerateAuthenticatorRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "400", global::PulsePlaylist.Api.Client.Models.HttpValidationProblemDetails.CreateFromDiscriminatorValue },
                { "404", global::PulsePlaylist.Api.Client.Models.ProblemDetails.CreateFromDiscriminatorValue },
                { "500", global::PulsePlaylist.Api.Client.Models.ProblemDetails.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::PulsePlaylist.Api.Client.Models.AuthenticatorResponse>(requestInfo, global::PulsePlaylist.Api.Client.Models.AuthenticatorResponse.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Generates a shared key and an Authenticator URI for a logged-in user. This endpoint is typically used to configure a TOTP authenticator app, such as Microsoft Authenticator or Google Authenticator.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder.GenerateAuthenticatorRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder WithUrl(string rawUrl)
        {
            return new global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Generates a shared key and an Authenticator URI for a logged-in user. This endpoint is typically used to configure a TOTP authenticator app, such as Microsoft Authenticator or Google Authenticator.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class GenerateAuthenticatorRequestBuilderGetQueryParameters 
        {
            [QueryParameter("appName")]
            public string AppName { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class GenerateAuthenticatorRequestBuilderGetRequestConfiguration : RequestConfiguration<global::PulsePlaylist.Api.Client.Account.GenerateAuthenticator.GenerateAuthenticatorRequestBuilder.GenerateAuthenticatorRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
