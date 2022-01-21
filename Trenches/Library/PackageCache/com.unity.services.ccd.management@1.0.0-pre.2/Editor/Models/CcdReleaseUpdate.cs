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
    /// CcdReleaseUpdate model
    /// <param name="notes">notes param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.releaseUpdate")]
    public class CcdReleaseUpdate
    {
        /// <summary>
        /// Creates an instance of CcdReleaseUpdate.
        /// </summary>
        /// <param name="notes">notes param</param>
        [Preserve]
        public CcdReleaseUpdate(string notes = default)
        {
            Notes = notes;
        }

        /// <summary>
        /// Notes
        /// </summary>
        [Preserve]
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public string Notes { get; }

    }
}

