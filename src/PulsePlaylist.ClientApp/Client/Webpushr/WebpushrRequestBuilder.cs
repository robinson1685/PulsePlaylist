// <auto-generated/>
#pragma warning disable CS0618
using PulsePlaylist.Api.Client.Webpushr.Config;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace PulsePlaylist.Api.Client.Webpushr
{
    /// <summary>
    /// Builds and executes requests for operations under \webpushr
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WebpushrRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The config property</summary>
        public global::PulsePlaylist.Api.Client.Webpushr.Config.ConfigRequestBuilder Config
        {
            get => new global::PulsePlaylist.Api.Client.Webpushr.Config.ConfigRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.Webpushr.WebpushrRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WebpushrRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/webpushr", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.Webpushr.WebpushrRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WebpushrRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/webpushr", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
