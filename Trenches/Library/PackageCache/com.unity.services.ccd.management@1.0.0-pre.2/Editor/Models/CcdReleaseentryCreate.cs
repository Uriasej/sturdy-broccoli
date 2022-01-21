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
    /// CcdReleaseentryCreate model
    /// <param name="entryid">entryid param</param>
    /// <param name="versionid">versionid param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.releaseentryCreate")]
    public class CcdReleaseentryCreate
    {
        /// <summary>
        /// Creates an instance of CcdReleaseentryCreate.
        /// </summary>
        /// <param name="entryid">entryid param</param>
        /// <param name="versionid">versionid param</param>
        [Preserve]
        public CcdReleaseentryCreate(System.Guid entryid, System.Guid versionid)
        {
            Entryid = entryid;
            Versionid = versionid;
        }

        /// <summary>
        /// Entry id
        /// </summary>
        [Preserve]
        [DataMember(Name = "entryid", IsRequired = true, EmitDefaultValue = true)]
        public System.Guid Entryid { get; }

        /// <summary>
        /// Version id
        /// </summary>
        [Preserve]
        [DataMember(Name = "versionid", IsRequired = true, EmitDefaultValue = true)]
        public System.Guid Versionid { get; }

    }
}

