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
    /// CcdPromotebucket model
    /// <param name="from_release">from_release param</param>
    /// <param name="notes">If unset, the release notes of the \&quot;from release\&quot; will be used.</param>
    /// <param name="to_bucket">to_bucket param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.promotebucket")]
    public class CcdPromotebucket
    {
        /// <summary>
        /// Creates an instance of CcdPromotebucket.
        /// </summary>
        /// <param name="fromRelease">from_release param</param>
        /// <param name="toBucket">to_bucket param</param>
        /// <param name="notes">If unset, the release notes of the \&quot;from release\&quot; will be used.</param>
        [Preserve]
        public CcdPromotebucket(System.Guid fromRelease, System.Guid toBucket, string notes = default)
        {
            FromRelease = fromRelease;
            Notes = notes;
            ToBucket = toBucket;
        }

        /// <summary>
        /// Release Id to promote
        /// </summary>
        [Preserve]
        [DataMember(Name = "from_release", IsRequired = true, EmitDefaultValue = true)]
        public System.Guid FromRelease { get; }

        /// <summary>
        /// If unset, the release notes of the \&quot;from release\&quot; will be used.
        /// </summary>
        [Preserve]
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public string Notes { get; }

        /// <summary>
        /// Bucket Id to release to
        /// </summary>
        [Preserve]
        [DataMember(Name = "to_bucket", IsRequired = true, EmitDefaultValue = true)]
        public System.Guid ToBucket { get; }

    }
}

