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
    /// CcdReleaseCreate model
    /// <param name="entries">entries param</param>
    /// <param name="metadata">metadata param</param>
    /// <param name="notes">notes param</param>
    /// <param name="snapshot">snapshot param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.releaseCreate")]
    public class CcdReleaseCreate
    {
        /// <summary>
        /// Creates an instance of CcdReleaseCreate.
        /// </summary>
        /// <param name="entries">entries param</param>
        /// <param name="metadata">metadata param</param>
        /// <param name="notes">notes param</param>
        /// <param name="snapshot">snapshot param</param>
        [Preserve]
        public CcdReleaseCreate(List<CcdReleaseentryCreate> entries = default, object metadata = default, string notes = default, DateTime snapshot = default)
        {
            Entries = entries;
            Metadata = metadata != null ? new JsonObject(metadata) : null;
            Notes = notes;
            Snapshot = snapshot;
        }

        /// <summary>
        /// Entries
        /// </summary>
        [Preserve]
        [DataMember(Name = "entries", EmitDefaultValue = false)]
        public List<CcdReleaseentryCreate> Entries { get; }

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
        /// Snapshot
        /// </summary>
        [Preserve]
        [DataMember(Name = "snapshot", EmitDefaultValue = false)]
        public DateTime Snapshot { get; }

    }
}

