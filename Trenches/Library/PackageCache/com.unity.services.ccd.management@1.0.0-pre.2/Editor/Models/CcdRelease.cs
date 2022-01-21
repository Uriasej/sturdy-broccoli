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
    /// CcdRelease model
    /// <param name="badges">badges param</param>
    /// <param name="changes">changes param</param>
    /// <param name="content_hash">content_hash param</param>
    /// <param name="content_size">content_size param</param>
    /// <param name="created">created param</param>
    /// <param name="created_by">created_by param</param>
    /// <param name="created_by_name">created_by_name param</param>
    /// <param name="entries_link">entries_link param</param>
    /// <param name="metadata">metadata param</param>
    /// <param name="notes">notes param</param>
    /// <param name="promoted_from_bucket">promoted_from_bucket param</param>
    /// <param name="promoted_from_release">promoted_from_release param</param>
    /// <param name="releaseid">releaseid param</param>
    /// <param name="releasenum">releasenum param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.release")]
    public class CcdRelease
    {
        /// <summary>
        /// Creates an instance of CcdRelease.
        /// </summary>
        /// <param name="badges">badges param</param>
        /// <param name="changes">changes param</param>
        /// <param name="contentHash">content_hash param</param>
        /// <param name="contentSize">content_size param</param>
        /// <param name="created">created param</param>
        /// <param name="createdBy">created_by param</param>
        /// <param name="createdByName">created_by_name param</param>
        /// <param name="entriesLink">entries_link param</param>
        /// <param name="metadata">metadata param</param>
        /// <param name="notes">notes param</param>
        /// <param name="promotedFromBucket">promoted_from_bucket param</param>
        /// <param name="promotedFromRelease">promoted_from_release param</param>
        /// <param name="releaseid">releaseid param</param>
        /// <param name="releasenum">releasenum param</param>
        [Preserve]
        public CcdRelease(List<CcdBadge> badges = default, CcdChangecount changes = default, string contentHash = default, int contentSize = default, DateTime created = default, string createdBy = default, string createdByName = default, string entriesLink = default, object metadata = default, string notes = default, System.Guid promotedFromBucket = default, System.Guid promotedFromRelease = default, System.Guid releaseid = default, int releasenum = default)
        {
            Badges = badges;
            Changes = changes;
            ContentHash = contentHash;
            ContentSize = contentSize;
            Created = created;
            CreatedBy = createdBy;
            CreatedByName = createdByName;
            EntriesLink = entriesLink;
            Metadata = metadata != null ? new JsonObject(metadata) : null;
            Notes = notes;
            PromotedFromBucket = promotedFromBucket;
            PromotedFromRelease = promotedFromRelease;
            Releaseid = releaseid;
            Releasenum = releasenum;
        }

        /// <summary>
        /// Badges
        /// </summary>
        [Preserve]
        [DataMember(Name = "badges", EmitDefaultValue = false)]
        public List<CcdBadge> Badges { get; }

        /// <summary>
        /// Changes
        /// </summary>
        [Preserve]
        [DataMember(Name = "changes", EmitDefaultValue = false)]
        public CcdChangecount Changes { get; }

        /// <summary>
        /// Content hash
        /// </summary>
        [Preserve]
        [DataMember(Name = "content_hash", EmitDefaultValue = false)]
        public string ContentHash { get; }

        /// <summary>
        /// Content size
        /// </summary>
        [Preserve]
        [DataMember(Name = "content_size", EmitDefaultValue = false)]
        public int ContentSize { get; }


        /// <summary>
        /// Created DateTime
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", EmitDefaultValue = false)]
        public DateTime Created { get; }

        /// <summary>
        /// Created By
        /// </summary>
        [Preserve]
        [DataMember(Name = "created_by", EmitDefaultValue = false)]
        public string CreatedBy { get; }

        /// <summary>
        /// Created By Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "created_by_name", EmitDefaultValue = false)]
        public string CreatedByName { get; }

        /// <summary>
        /// Entries link
        /// </summary>
        [Preserve]
        [DataMember(Name = "entries_link", EmitDefaultValue = false)]
        public string EntriesLink { get; }

        /// <summary>
        /// Metadata
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public JsonObject Metadata { get; }

        /// <summary>
        /// Notes
        /// </summary>
        [Preserve]
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public string Notes { get; }

        /// <summary>
        /// Promoted from Bucket id
        /// </summary>
        [Preserve]
        [DataMember(Name = "promoted_from_bucket", EmitDefaultValue = false)]
        public System.Guid PromotedFromBucket { get; }

        /// <summary>
        /// Promoted from Release id
        /// </summary>
        [Preserve]
        [DataMember(Name = "promoted_from_release", EmitDefaultValue = false)]
        public System.Guid PromotedFromRelease { get; }

        /// <summary>
        /// Release Id
        /// </summary>
        [Preserve]
        [DataMember(Name = "releaseid", EmitDefaultValue = false)]
        public System.Guid Releaseid { get; }

        /// <summary>
        /// Release Num
        /// </summary>
        [Preserve]
        [DataMember(Name = "releasenum", EmitDefaultValue = false)]
        public int Releasenum { get; }

    }
}

