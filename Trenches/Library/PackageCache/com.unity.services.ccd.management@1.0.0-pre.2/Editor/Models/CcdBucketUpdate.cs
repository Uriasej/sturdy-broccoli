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
    /// CcdBucketUpdate model
    /// <param name="description">description param</param>
    /// <param name="name">name param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.bucketUpdate")]
    public class CcdBucketUpdate
    {
        /// <summary>
        /// Creates an instance of CcdBucketUpdate.
        /// </summary>
        /// <param name="description">description param</param>
        /// <param name="name">name param</param>
        [Preserve]
        public CcdBucketUpdate(string description = default, string name = default)
        {
            Description = description;
            Name = name;
        }

        /// <summary>
        /// Description
        /// </summary>
        [Preserve]
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; }

        /// <summary>
        /// Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; }

    }
}

