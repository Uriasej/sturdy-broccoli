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
    /// CcdPermission model
    /// <param name="action">action param</param>
    /// <param name="permission">permission param</param>
    /// <param name="resource">resource param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.permission")]
    public class CcdPermission
    {
        /// <summary>
        /// Creates an instance of CcdPermission.
        /// </summary>
        /// <param name="action">action param</param>
        /// <param name="permission">permission param</param>
        /// <param name="resource">resource param</param>
        [Preserve]
        public CcdPermission(string action = default, string permission = default, string resource = default)
        {
            Action = action;
            Permission = permission;
            Resource = resource;
        }

        /// <summary>
        /// Action
        /// </summary>
        [Preserve]
        [DataMember(Name = "action", EmitDefaultValue = false)]
        public string Action { get; }

        /// <summary>
        /// Permission
        /// </summary>
        [Preserve]
        [DataMember(Name = "permission", EmitDefaultValue = false)]
        public string Permission { get; }

        /// <summary>
        /// Resource
        /// </summary>
        [Preserve]
        [DataMember(Name = "resource", EmitDefaultValue = false)]
        public string Resource { get; }

    }
}

