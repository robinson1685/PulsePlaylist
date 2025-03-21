// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace PulsePlaylist.Api.Client.ConfirmEmail
{
    /// <summary>
    /// Builds and executes requests for operations under \confirmEmail
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ConfirmEmailRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ConfirmEmailRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/confirmEmail?code={code}&userId={userId}{&changedEmail*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ConfirmEmailRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/confirmEmail?code={code}&userId={userId}{&changedEmail*}", rawUrl)
        {
        }
        /// <returns>A <see cref="Stream"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        public async Task<Stream> GetAsync(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder.ConfirmEmailRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendPrimitiveAsync<Stream>(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder.ConfirmEmailRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder WithUrl(string rawUrl)
        {
            return new global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder(rawUrl, RequestAdapter);
        }
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        #pragma warning disable CS1591
        public partial class ConfirmEmailRequestBuilderGetQueryParameters 
        #pragma warning restore CS1591
        {
            [QueryParameter("changedEmail")]
            public string ChangedEmail { get; set; }
            [QueryParameter("code")]
            public string Code { get; set; }
            [QueryParameter("userId")]
            public string UserId { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ConfirmEmailRequestBuilderGetRequestConfiguration : RequestConfiguration<global::PulsePlaylist.Api.Client.ConfirmEmail.ConfirmEmailRequestBuilder.ConfirmEmailRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
