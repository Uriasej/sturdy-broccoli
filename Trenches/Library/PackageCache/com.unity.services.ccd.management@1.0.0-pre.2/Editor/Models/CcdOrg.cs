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
    /// CcdOrg model
    /// <param name="id">id param</param>
    /// <param name="name">name param</param>
    /// <param name="orgid">orgid param</param>
    /// <param name="tos_accepted">tos_accepted param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.org")]
    public class CcdOrg
    {
        /// <summary>
        /// Creates an instance of CcdOrg.
        /// </summary>
        /// <param name="id">id param</param>
        /// <param name="name">name param</param>
        /// <param name="orgid">orgid param</param>
        /// <param name="tosAccepted">tos_accepted param</param>
        [Preserve]
        public CcdOrg(string id = default, string name = default, string orgid = default, bool tosAccepted = default)
        {
            Id = id;
            Name = name;
            Orgid = orgid;
            TosAccepted = tosAccepted;
        }

        /// <summary>
        /// Id
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; }

        /// <summary>
        /// Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; }

        /// <summary>
        /// Org id
        /// </summary>
        [Preserve]
        [DataMember(Name = "orgid", EmitDefaultValue = false)]
        public string Orgid { get; }

        /// <summary>
        /// TosAccepted
        /// </summary>
        [Preserve]
        [DataMember(Name = "tos_accepted", EmitDefaultValue = true)]
        public bool TosAccepted { get; }

    }
}

