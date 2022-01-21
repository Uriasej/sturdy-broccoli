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
    /// CcdBucketCreate model
    /// <param name="description">description param</param>
    /// <param name="name">name param</param>
    /// <param name="projectguid">projectguid param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.bucketCreate")]
    public class CcdBucketCreate
    {
        /// <summary>
        /// Creates an instance of CcdBucketCreate.
        /// </summary>
        /// <param name="name">name param</param>
        /// <param name="projectguid">projectguid param</param>
        /// <param name="description">description param</param>
        [Preserve]
        public CcdBucketCreate(string name, System.Guid projectguid, string description = default)
        {
            Description = description;
            Name = name;
            Projectguid = projectguid;
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
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; }

        /// <summary>
        /// Project Guid
        /// </summary>
        [Preserve]
        [DataMember(Name = "projectguid", IsRequired = true, EmitDefaultValue = true)]
        public System.Guid Projectguid { get; }

    }
}

