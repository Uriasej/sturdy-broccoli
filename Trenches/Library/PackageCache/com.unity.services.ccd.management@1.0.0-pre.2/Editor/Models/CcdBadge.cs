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
    /// CcdBadge model
    /// <param name="created">created param</param>
    /// <param name="created_by">created_by param</param>
    /// <param name="created_by_name">created_by_name param</param>
    /// <param name="name">name param</param>
    /// <param name="releaseid">releaseid param</param>
    /// <param name="releasenum">releasenum param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.badge")]
    public class CcdBadge
    {
        /// <summary>
        /// Creates an instance of CcdBadge.
        /// </summary>
        /// <param name="created">created param</param>
        /// <param name="createdBy">created_by param</param>
        /// <param name="createdByName">created_by_name param</param>
        /// <param name="name">name param</param>
        /// <param name="releaseid">releaseid param</param>
        /// <param name="releasenum">releasenum param</param>
        [Preserve]
        public CcdBadge(DateTime created = default, string createdBy = default, string createdByName = default, string name = default, System.Guid releaseid = default, int releasenum = default)
        {
            Created = created;
            CreatedBy = createdBy;
            CreatedByName = createdByName;
            Name = name;
            Releaseid = releaseid;
            Releasenum = releasenum;
        }

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
        /// Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; }

        /// <summary>
        /// Release Id
        /// </summary>
        [Preserve]
        [DataMember(Name = "releaseid", EmitDefaultValue = false)]
        public System.Guid Releaseid { get; }

        /// <summary>
        /// Release Number
        /// </summary>
        [Preserve]
        [DataMember(Name = "releasenum", EmitDefaultValue = false)]
        public int Releasenum { get; }

    }
}

