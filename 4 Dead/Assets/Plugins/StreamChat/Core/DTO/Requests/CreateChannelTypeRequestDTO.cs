//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.15.5.0 (NJsonSchema v10.6.6.0 (Newtonsoft.Json v9.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using StreamChat.Core.DTO.Responses;
using StreamChat.Core.DTO.Events;
using StreamChat.Core.DTO.Models;

namespace StreamChat.Core.DTO.Requests
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.5.0 (NJsonSchema v10.6.6.0 (Newtonsoft.Json v9.0.0.0))")]
    internal partial class CreateChannelTypeRequestDTO
    {
        /// <summary>
        /// Enables automatic message moderation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("automod", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AutomodType Automod { get; set; }

        /// <summary>
        /// Sets behavior of automatic moderation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("automod_behavior", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AutomodBehaviourType? AutomodBehavior { get; set; }

        /// <summary>
        /// Name of the blocklist to use
        /// </summary>
        [Newtonsoft.Json.JsonProperty("blocklist", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Blocklist { get; set; }

        /// <summary>
        /// Sets behavior of blocklist
        /// </summary>
        [Newtonsoft.Json.JsonProperty("blocklist_behavior", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AutomodBehaviourType? BlocklistBehavior { get; set; }

        /// <summary>
        /// List of commands that channel supports
        /// </summary>
        [Newtonsoft.Json.JsonProperty("commands", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<string> Commands { get; set; }

        /// <summary>
        /// Connect events support
        /// </summary>
        [Newtonsoft.Json.JsonProperty("connect_events", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? ConnectEvents { get; set; }

        /// <summary>
        /// Enables custom events
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_events", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? CustomEvents { get; set; }

        [Newtonsoft.Json.JsonProperty("grants", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> Grants { get; set; }

        /// <summary>
        /// Number of maximum message characters
        /// </summary>
        [Newtonsoft.Json.JsonProperty("max_message_length", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? MaxMessageLength { get; set; }

        /// <summary>
        /// Number of days to keep messages. 'infinite' disables retention
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message_retention", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string MessageRetention { get; set; }

        /// <summary>
        /// Enables mutes
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mutes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Mutes { get; set; }

        /// <summary>
        /// Channel type name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// List of permissions for the channel type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("permissions", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<PolicyRequestDTO> Permissions { get; set; }

        /// <summary>
        /// Enables push notifications
        /// </summary>
        [Newtonsoft.Json.JsonProperty("push_notifications", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? PushNotifications { get; set; }

        /// <summary>
        /// Enables message reactions
        /// </summary>
        [Newtonsoft.Json.JsonProperty("reactions", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Reactions { get; set; }

        /// <summary>
        /// Read events support
        /// </summary>
        [Newtonsoft.Json.JsonProperty("read_events", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? ReadEvents { get; set; }

        /// <summary>
        /// Enables message replies (threads)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("replies", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Replies { get; set; }

        /// <summary>
        /// Enables message search
        /// </summary>
        [Newtonsoft.Json.JsonProperty("search", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Search { get; set; }

        /// <summary>
        /// Typing events support
        /// </summary>
        [Newtonsoft.Json.JsonProperty("typing_events", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? TypingEvents { get; set; }

        /// <summary>
        /// Enables file uploads
        /// </summary>
        [Newtonsoft.Json.JsonProperty("uploads", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Uploads { get; set; }

        /// <summary>
        /// Enables URL enrichment
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url_enrichment", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UrlEnrichment { get; set; }

        private System.Collections.Generic.Dictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }

    }

}
