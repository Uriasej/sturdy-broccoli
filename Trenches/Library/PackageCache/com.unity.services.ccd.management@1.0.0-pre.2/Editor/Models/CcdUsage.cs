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
    /// CcdUsage model
    /// <param name="projectguid">projectguid param</param>
    /// <param name="quantity">quantity param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.usage")]
    public class CcdUsage
    {
        /// <summary>
        /// Creates an instance of CcdUsage.
        /// </summary>
        /// <param name="projectguid">projectguid param</param>
        /// <param name="quantity">quantity param</param>
        [Preserve]
        public CcdUsage(System.Guid projectguid = default, decimal quantity = default)
        {
            Projectguid = projectguid;
            Quantity = quantity;
        }

        /// <summary>
        /// Project Guid
        /// </summary>
        [Preserve]
        [DataMember(Name = "projectguid", EmitDefaultValue = false)]
        public System.Guid Projectguid { get; }

        /// <summary>
        /// Quantity
        /// </summary>
        [Preserve]
        [DataMember(Name = "quantity", EmitDefaultValue = false)]
        public decimal Quantity { get; }

    }
}

