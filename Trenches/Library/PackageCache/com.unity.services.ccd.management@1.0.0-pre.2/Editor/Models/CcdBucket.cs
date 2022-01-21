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
    /// CcdBucket model
    /// <param name="attributes">attributes param</param>
    /// <param name="changes">changes param</param>
    /// <param name="created">created param</param>
    /// <param name="description">description param</param>
    /// <param name="id">id param</param>
    /// <param name="last_release">last_release param</param>
    /// <param name="name">name param</param>
    /// <param name="permissions">permissions param</param>
    /// <param name="projectguid">projectguid param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.bucket")]
    public class CcdBucket
    {
        /// <summary>
        /// Creates an instance of CcdBucket.
        /// </summary>
        /// <param name="attributes">attributes param</param>
        /// <param name="changes">changes param</param>
        /// <param name="created">created param</param>
        /// <param name="description">description param</param>
        /// <param name="id">id param</param>
        /// <param name="lastRelease">last_release param</param>
        /// <param name="name">name param</param>
        /// <param name="permissions">permissions param</param>
        /// <param name="projectguid">projectguid param</param>
        [Preserve]
        public CcdBucket(CcdBucketAttributes attributes = default, CcdChangecount changes = default, DateTime created = default, string description = default, System.Guid id = default, CcdRelease lastRelease = default, string name = default, CcdBucketPermissions permissions = default, System.Guid projectguid = default)
        {
            Attributes = attributes;
            Changes = changes;
            Created = created;
            Description = description;
            Id = id;
            LastRelease = lastRelease;
            Name = name;
            Permissions = permissions;
            Projectguid = projectguid;
        }

        /// <summary>
        /// CCD Bucket Attributes
        /// </summary>
        [Preserve]
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public CcdBucketAttributes Attributes { get; }

        /// <summary>
        /// Changes
        /// </summary>
        [Preserve]
        [DataMember(Name = "changes", EmitDefaultValue = false)]
        public CcdChangecount Changes { get; }

        /// <summary>
        /// Created DateTime
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", EmitDefaultValue = false)]
        public DateTime Created { get; }

        /// <summary>
        /// Description
        /// </summary>
        [Preserve]
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; }

        /// <summary>
        /// Id
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public System.Guid Id { get; }

        /// <summary>
        /// Last Release
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_release", EmitDefaultValue = false)]
        public CcdRelease LastRelease { get; }

        /// <summary>
        /// Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; }

        /// <summary>
        /// Permissions
        /// </summary>
        [Preserve]
        [DataMember(Name = "permissions", EmitDefaultValue = false)]
        public CcdBucketPermissions Permissions { get; }

        /// <summary>
        /// Project Guid
        /// </summary>
        [Preserve]
        [DataMember(Name = "projectguid", EmitDefaultValue = false)]
        public System.Guid Projectguid { get; }

    }
}

