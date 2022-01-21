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
    /// CcdPermissionCreate model
    /// <param name="action">action param</param>
    /// <param name="permission">permission param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.permissionCreate")]
    public class CcdPermissionCreate
    {
        /// <summary>
        /// Creates an instance of CcdPermissionCreate.
        /// </summary>
        /// <param name="action">action param</param>
        /// <param name="permission">permission param</param>
        [Preserve]
        public CcdPermissionCreate(ActionOptions action, PermissionOptions permission)
        {
            Action = action;
            Permission = permission;
        }

        /// <summary>
        /// Action
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "action", IsRequired = true, EmitDefaultValue = true)]
        public ActionOptions Action { get; }

        /// <summary>
        /// Permission
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "permission", IsRequired = true, EmitDefaultValue = true)]
        public PermissionOptions Permission { get; }


        /// <summary>
        /// Defines Action
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ActionOptions
        {
            /// <summary>
            /// Enum Write for value: write
            /// </summary>
            [EnumMember(Value = "write")]
            Write = 1

        }


        /// <summary>
        /// Defines Permission
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PermissionOptions
        {
            /// <summary>
            /// Enum Allow for value: allow
            /// </summary>
            [EnumMember(Value = "allow")]
            Allow = 1,

            /// <summary>
            /// Enum Deny for value: deny
            /// </summary>
            [EnumMember(Value = "deny")]
            Deny = 2

        }

    }
}

