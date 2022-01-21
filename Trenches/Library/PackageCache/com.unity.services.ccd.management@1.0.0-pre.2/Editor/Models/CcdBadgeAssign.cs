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
    /// CcdBadgeAssign model
    /// <param name="name">name param</param>
    /// <param name="releaseid">releaseid param</param>
    /// <param name="releasenum">releasenum param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.badgeAssign")]
    public class CcdBadgeAssign
    {
        /// <summary>
        /// Creates an instance of CcdBadgeAssign.
        /// </summary>
        /// <param name="name">name param</param>
        /// <param name="releaseid">releaseid param</param>
        /// <param name="releasenum">releasenum param</param>
        [Preserve]
        public CcdBadgeAssign(string name, System.Guid? releaseid = default, int? releasenum = default)
        {
            Name = name;

            if (releaseid != null)
            {
                Releaseid = releaseid;
            }

            if (releasenum.HasValue)
            {
                Releasenum = releasenum;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; }

        /// <summary>
        /// Release Id
        /// </summary>
        [Preserve]
        [DataMember(Name = "releaseid", EmitDefaultValue = false)]
        public System.Guid? Releaseid { get; }

        /// <summary>
        /// Release Number
        /// </summary>
        [Preserve]
        [DataMember(Name = "releasenum", EmitDefaultValue = false)]
        public int? Releasenum { get; }

    }
}

