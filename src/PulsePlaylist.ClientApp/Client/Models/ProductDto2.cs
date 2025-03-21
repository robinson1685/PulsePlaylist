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
    public partial class ProductDto2 : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The category property</summary>
        public global::PulsePlaylist.Api.Client.Models.NullableOfProductCategoryDto? Category { get; set; }
        /// <summary>The currency property</summary>
        public string Currency { get; set; }
        /// <summary>The description property</summary>
        public string Description { get; set; }
        /// <summary>The id property</summary>
        public string Id { get; set; }
        /// <summary>The name property</summary>
        public string Name { get; set; }
        /// <summary>The price property</summary>
        public double? Price { get; set; }
        /// <summary>The sku property</summary>
        public string Sku { get; set; }
        /// <summary>The uom property</summary>
        public string Uom { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::PulsePlaylist.Api.Client.Models.ProductDto2"/> and sets the default values.
        /// </summary>
        public ProductDto2()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::PulsePlaylist.Api.Client.Models.ProductDto2"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::PulsePlaylist.Api.Client.Models.ProductDto2 CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::PulsePlaylist.Api.Client.Models.ProductDto2();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "category", n => { Category = n.GetEnumValue<global::PulsePlaylist.Api.Client.Models.NullableOfProductCategoryDto>(); } },
                { "currency", n => { Currency = n.GetStringValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "price", n => { Price = n.GetDoubleValue(); } },
                { "sku", n => { Sku = n.GetStringValue(); } },
                { "uom", n => { Uom = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::PulsePlaylist.Api.Client.Models.NullableOfProductCategoryDto>("category", Category);
            writer.WriteStringValue("currency", Currency);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteDoubleValue("price", Price);
            writer.WriteStringValue("sku", Sku);
            writer.WriteStringValue("uom", Uom);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
