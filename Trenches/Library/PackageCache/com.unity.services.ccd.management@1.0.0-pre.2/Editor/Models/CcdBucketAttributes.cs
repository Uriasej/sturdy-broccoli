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
    /// CcdBucketAttributes model
    /// <param name="promoteOnly">promote_only param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.bucketAttributes")]
    public class CcdBucketAttributes
    {
        /// <summary>
        /// Creates an instance of CcdBucketAttributes.
        /// </summary>
        /// <param name="promoteOnly">promote_only param</param>
        [Preserve]
        public CcdBucketAttributes(bool promoteOnly = default)
        {
            PromoteOnly = promoteOnly;
        }

        /// <summary>
        /// Promotion Only
        /// </summary>
        [Preserve]
        [DataMember(Name = "promote_only", EmitDefaultValue = true)]
        public bool PromoteOnly { get; }

    }
}

