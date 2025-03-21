// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace PulsePlaylist.Api.Client.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ProfileResponse : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The avatarUrl property</summary>
        public string AvatarUrl { get; set; }
        /// <summary>The email property</summary>
        public string Email { get; set; }
        /// <summary>The isEmailConfirmed property</summary>
        public bool? IsEmailConfirmed { get; set; }
        /// <summary>The isTwoFactorEnabled property</summary>
        public bool? IsTwoFactorEnabled { get; set; }
        /// <summary>The languageCode property</summary>
        public string LanguageCode { get; set; }
        /// <summary>The nickname property</summary>
        public string Nickname { get; set; }
        /// <summary>The provider property</summary>
        public string Provider { get; set; }
        /// <summary>The superiorId property</summary>
        public string SuperiorId { get; set; }
        /// <summary>The tenantId property</summary>
        public string TenantId { get; set; }
        /// <summary>The timeZoneId property</summary>
        public string TimeZoneId { get; set; }
        /// <summary>The userId property</summary>
        public string UserId { get; set; }
        /// <summary>The username property</summary>
        public string Username { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.Models.ProfileResponse"/> and sets the default values.
        /// </summary>
        public ProfileResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::PulsePlaylist.Api.Client.Models.ProfileResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::PulsePlaylist.Api.Client.Models.ProfileResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::PulsePlaylist.Api.Client.Models.ProfileResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "avatarUrl", n => { AvatarUrl = n.GetStringValue(); } },
                { "email", n => { Email = n.GetStringValue(); } },
                { "isEmailConfirmed", n => { IsEmailConfirmed = n.GetBoolValue(); } },
                { "isTwoFactorEnabled", n => { IsTwoFactorEnabled = n.GetBoolValue(); } },
                { "languageCode", n => { LanguageCode = n.GetStringValue(); } },
                { "nickname", n => { Nickname = n.GetStringValue(); } },
                { "provider", n => { Provider = n.GetStringValue(); } },
                { "superiorId", n => { SuperiorId = n.GetStringValue(); } },
                { "tenantId", n => { TenantId = n.GetStringValue(); } },
                { "timeZoneId", n => { TimeZoneId = n.GetStringValue(); } },
                { "userId", n => { UserId = n.GetStringValue(); } },
                { "username", n => { Username = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("avatarUrl", AvatarUrl);
            writer.WriteStringValue("email", Email);
            writer.WriteBoolValue("isEmailConfirmed", IsEmailConfirmed);
            writer.WriteBoolValue("isTwoFactorEnabled", IsTwoFactorEnabled);
            writer.WriteStringValue("languageCode", LanguageCode);
            writer.WriteStringValue("nickname", Nickname);
            writer.WriteStringValue("provider", Provider);
            writer.WriteStringValue("superiorId", SuperiorId);
            writer.WriteStringValue("tenantId", TenantId);
            writer.WriteStringValue("timeZoneId", TimeZoneId);
            writer.WriteStringValue("userId", UserId);
            writer.WriteStringValue("username", Username);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
