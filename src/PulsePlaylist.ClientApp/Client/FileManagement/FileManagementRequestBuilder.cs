// <auto-generated/>
#pragma warning disable CS0618
using PulsePlaylist.Api.Client.FileManagement.AntiforgeryToken;
using PulsePlaylist.Api.Client.FileManagement.Image;
using PulsePlaylist.Api.Client.FileManagement.Upload;
using PulsePlaylist.Api.Client.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace PulsePlaylist.Api.Client.FileManagement
{
    /// <summary>
    /// Builds and executes requests for operations under \fileManagement
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class FileManagementRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The antiforgeryToken property</summary>
        public global::PulsePlaylist.Api.Client.FileManagement.AntiforgeryToken.AntiforgeryTokenRequestBuilder AntiforgeryToken
        {
            get => new global::PulsePlaylist.Api.Client.FileManagement.AntiforgeryToken.AntiforgeryTokenRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The image property</summary>
        public global::PulsePlaylist.Api.Client.FileManagement.Image.ImageRequestBuilder Image
        {
            get => new global::PulsePlaylist.Api.Client.FileManagement.Image.ImageRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The upload property</summary>
        public global::PulsePlaylist.Api.Client.FileManagement.Upload.UploadRequestBuilder Upload
        {
            get => new global::PulsePlaylist.Api.Client.FileManagement.Upload.UploadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public FileManagementRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/fileManagement?path={path}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public FileManagementRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/fileManagement?path={path}", rawUrl)
        {
        }
        /// <summary>
        /// Allows clients to delete a file by specifying the folder and file name.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::PulsePlaylist.Api.Client.Models.HttpValidationProblemDetails">When receiving a 400 status code</exception>
        public async Task DeleteAsync(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder.FileManagementRequestBuilderDeleteQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "400", global::PulsePlaylist.Api.Client.Models.HttpValidationProblemDetails.CreateFromDiscriminatorValue },
            };
            await RequestAdapter.SendNoContentAsync(requestInfo, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Allows clients to download or preview a file by specifying the folder and file name.
        /// </summary>
        /// <returns>A <see cref="Stream"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::PulsePlaylist.Api.Client.Models.HttpValidationProblemDetails">When receiving a 400 status code</exception>
        public async Task<Stream> GetAsync(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder.FileManagementRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "400", global::PulsePlaylist.Api.Client.Models.HttpValidationProblemDetails.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendPrimitiveAsync<Stream>(requestInfo, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Allows clients to delete a file by specifying the folder and file name.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder.FileManagementRequestBuilderDeleteQueryParameters>> requestConfiguration = default)
        {
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json, application/problem+json");
            return requestInfo;
        }
        /// <summary>
        /// Allows clients to download or preview a file by specifying the folder and file name.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder.FileManagementRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json, application/problem+json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder WithUrl(string rawUrl)
        {
            return new global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Allows clients to delete a file by specifying the folder and file name.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FileManagementRequestBuilderDeleteQueryParameters 
        {
            [QueryParameter("path")]
            public string Path { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FileManagementRequestBuilderDeleteRequestConfiguration : RequestConfiguration<global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder.FileManagementRequestBuilderDeleteQueryParameters>
        {
        }
        /// <summary>
        /// Allows clients to download or preview a file by specifying the folder and file name.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FileManagementRequestBuilderGetQueryParameters 
        {
            [QueryParameter("path")]
            public string Path { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FileManagementRequestBuilderGetRequestConfiguration : RequestConfiguration<global::PulsePlaylist.Api.Client.FileManagement.FileManagementRequestBuilder.FileManagementRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
