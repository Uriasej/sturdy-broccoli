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
    /// CcdBucketPermissions model
    /// <param name="bucket_promote">bucket_promote param</param>
    /// <param name="bucket_read">bucket_read param</param>
    /// <param name="bucket_release">bucket_release param</param>
    /// <param name="bucket_write">bucket_write param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.bucketPermissions")]
    public class CcdBucketPermissions
    {
        /// <summary>
        /// Creates an instance of CcdBucketPermissions.
        /// </summary>
        /// <param name="bucketPromote">bucket_promote param</param>
        /// <param name="bucketRead">bucket_read param</param>
        /// <param name="bucketRelease">bucket_release param</param>
        /// <param name="bucketWrite">bucket_write param</param>
        [Preserve]
        public CcdBucketPermissions(bool bucketPromote = default, bool bucketRead = default, bool bucketRelease = default, bool bucketWrite = default)
        {
            BucketPromote = bucketPromote;
            BucketRead = bucketRead;
            BucketRelease = bucketRelease;
            BucketWrite = bucketWrite;
        }

        /// <summary>
        /// Promote Permission
        /// </summary>
        [Preserve]
        [DataMember(Name = "bucket_promote", EmitDefaultValue = true)]
        public bool BucketPromote { get; }

        /// <summary>
        /// Read Permission
        /// </summary>
        [Preserve]
        [DataMember(Name = "bucket_read", EmitDefaultValue = true)]
        public bool BucketRead { get; }

        /// <summary>
        /// Release Permission
        /// </summary>
        [Preserve]
        [DataMember(Name = "bucket_release", EmitDefaultValue = true)]
        public bool BucketRelease { get; }

        /// <summary>
        /// Write Permission
        /// </summary>
        [Preserve]
        [DataMember(Name = "bucket_write", EmitDefaultValue = true)]
        public bool BucketWrite { get; }

    }
}

