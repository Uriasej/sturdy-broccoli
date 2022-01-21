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
    /// CcdUser model
    /// <param name="id">id param</param>
    /// <param name="name">name param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.user")]
    public class CcdUser
    {
        /// <summary>
        /// Creates an instance of CcdUser.
        /// </summary>
        /// <param name="id">id param</param>
        /// <param name="name">name param</param>
        [Preserve]
        public CcdUser(string id = default, string name = default)
        {
            Id = id;
            Name = name;
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

    }
}

