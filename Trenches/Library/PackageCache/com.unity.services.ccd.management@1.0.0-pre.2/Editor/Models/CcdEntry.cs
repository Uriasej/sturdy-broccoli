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
    /// CcdEntry model
    /// <param name="complete">complete param</param>
    /// <param name="content_hash">content_hash param</param>
    /// <param name="content_link">content_link param</param>
    /// <param name="content_size">content_size param</param>
    /// <param name="content_type">content_type param</param>
    /// <param name="current_versionid">current_versionid param</param>
    /// <param name="entryid">entryid param</param>
    /// <param name="labels">labels param</param>
    /// <param name="last_modified">last_modified param</param>
    /// <param name="last_modified_by">last_modified_by param</param>
    /// <param name="last_modified_by_name">last_modified_by_name param</param>
    /// <param name="link">link param</param>
    /// <param name="metadata">metadata param</param>
    /// <param name="path">path param</param>
    /// <param name="updated_at">updated_at param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.entry")]
    public class CcdEntry
    {
        /// <summary>
        /// Creates an instance of CcdEntry.
        /// </summary>
        /// <param name="complete">complete param</param>
        /// <param name="contentHash">content_hash param</param>
        /// <param name="contentLink">content_link param</param>
        /// <param name="contentSize">content_size param</param>
        /// <param name="contentType">content_type param</param>
        /// <param name="currentVersionid">current_versionid param</param>
        /// <param name="entryid">entryid param</param>
        /// <param name="labels">labels param</param>
        /// <param name="lastModified">last_modified param</param>
        /// <param name="lastModifiedBy">last_modified_by param</param>
        /// <param name="lastModifiedByName">last_modified_by_name param</param>
        /// <param name="link">link param</param>
        /// <param name="metadata">metadata param</param>
        /// <param name="path">path param</param>
        /// <param name="updatedAt">updated_at param</param>
        [Preserve]
        public CcdEntry(bool complete = default, string contentHash = default, string contentLink = default, int contentSize = default, string contentType = default, System.Guid currentVersionid = default, System.Guid entryid = default, List<string> labels = default, DateTime lastModified = default, string lastModifiedBy = default, string lastModifiedByName = default, string link = default, object metadata = default, string path = default, DateTime updatedAt = default)
        {
            Complete = complete;
            ContentHash = contentHash;
            ContentLink = contentLink;
            ContentSize = contentSize;
            ContentType = contentType;
            CurrentVersionid = currentVersionid;
            Entryid = entryid;
            Labels = labels;
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;
            LastModifiedByName = lastModifiedByName;
            Link = link;
            Metadata = metadata != null ? new JsonObject(metadata) : null;
            Path = path;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// Complete
        /// </summary>
        [Preserve]
        [DataMember(Name = "complete", EmitDefaultValue = true)]
        public bool Complete { get; }

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
        /// Current version id
        /// </summary>
        [Preserve]
        [DataMember(Name = "current_versionid", EmitDefaultValue = false)]
        public System.Guid CurrentVersionid { get; }

        /// <summary>
        /// Entry id
        /// </summary>
        [Preserve]
        [DataMember(Name = "entryid", EmitDefaultValue = false)]
        public System.Guid Entryid { get; }

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
        /// Last Modified By
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_modified_by", EmitDefaultValue = false)]
        public string LastModifiedBy { get; }

        /// <summary>
        /// Last Modified By Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_modified_by_name", EmitDefaultValue = false)]
        public string LastModifiedByName { get; }

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
        /// Updated DateTime
        /// </summary>
        [Preserve]
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; }

    }
}

