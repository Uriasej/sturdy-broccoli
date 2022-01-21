using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.CCD.Management.Http;



namespace Unity.Services.CCD.Management.Models
{
    /// <summary>
    /// CcdVersion model
    /// <param name="content_hash">content_hash param</param>
    /// <param name="content_link">content_link param</param>
    /// <param name="content_size">content_size param</param>
    /// <param name="content_type">content_type param</param>
    /// <param name="labels">labels param</param>
    /// <param name="last_modified">last_modified param</param>
    /// <param name="link">link param</param>
    /// <param name="metadata">metadata param</param>
    /// <param name="path">path param</param>
    /// <param name="versionid">versionid param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.version")]
    public class CcdVersion
    {
        /// <summary>
        /// Creates an instance of CcdVersion.
        /// </summary>
        /// <param name="contentHash">content_hash param</param>
        /// <param name="contentLink">content_link param</param>
        /// <param name="contentSize">content_size param</param>
        /// <param name="contentType">content_type param</param>
        /// <param name="labels">labels param</param>
        /// <param name="lastModified">last_modified param</param>
        /// <param name="link">link param</param>
        /// <param name="metadata">metadata param</param>
        /// <param name="path">path param</param>
        /// <param name="versionid">versionid param</param>
        [Preserve]
        public CcdVersion(string contentHash = default, string contentLink = default, int contentSize = default, string contentType = default, List<string> labels = default, DateTime lastModified = default, string link = default, object metadata = default, string path = default, System.Guid versionid = default)
        {
            ContentHash = contentHash;
            ContentLink = contentLink;
            ContentSize = contentSize;
            ContentType = contentType;
            Labels = labels;
            LastModified = lastModified;
            Link = link;
            Metadata = metadata != null ? new JsonObject(metadata) : null;
            Path = path;
            Versionid = versionid;
        }

        /// <summary>
        /// Content hash
        /// </summary>
        [Preserve]
        [DataMember(Name = "content_hash", EmitDefaultValue = false)]
        public string ContentHash { get; }

        /// <summary>
        /// Content link
        /// </summary>
        [Preserve]
        [DataMember(Name = "content_link", EmitDefaultValue = false)]
        public string ContentLink { get; }

        /// <summary>
        /// Content size
        /// </summary>
        [Preserve]
        [DataMember(Name = "content_size", EmitDefaultValue = false)]
        public int ContentSize { get; }

        /// <summary>
        /// Content type
        /// </summary>
        [Preserve]
        [DataMember(Name = "content_type", EmitDefaultValue = false)]
        public string ContentType { get; }

        /// <summary>
        /// Labels
        /// </summary>
        [Preserve]
        [DataMember(Name = "labels", EmitDefaultValue = false)]
        public List<string> Labels { get; }

        /// <summary>
        /// Last Modified DateTime
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_modified", EmitDefaultValue = false)]
        public DateTime LastModified { get; }

        /// <summary>
        /// Link
        /// </summary>
        [Preserve]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public string Link { get; }

        /// <summary>
        /// Metadata
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public JsonObject Metadata { get; }

        /// <summary>
        /// Path
        /// </summary>
        [Preserve]
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; }

        /// <summary>
        /// Version id
        /// </summary>
        [Preserve]
        [DataMember(Name = "versionid", EmitDefaultValue = false)]
        public System.Guid Versionid { get; }

    }
}

