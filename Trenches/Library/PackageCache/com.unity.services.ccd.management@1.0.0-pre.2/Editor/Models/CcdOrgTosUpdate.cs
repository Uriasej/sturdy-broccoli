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
    /// CcdOrgTosUpdate model
    /// <param name="tos_version">tos_version param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.orgTosUpdate")]
    public class CcdOrgTosUpdate
    {
        /// <summary>
        /// Creates an instance of CcdOrgTosUpdate.
        /// </summary>
        /// <param name="tosVersion">tos_version param</param>
        [Preserve]
        public CcdOrgTosUpdate(int tosVersion = default)
        {
            TosVersion = tosVersion;
        }

        /// <summary>
        /// Tos Version
        /// </summary>
        [Preserve]
        [DataMember(Name = "tos_version", EmitDefaultValue = false)]
        public int TosVersion { get; }

    }
}

