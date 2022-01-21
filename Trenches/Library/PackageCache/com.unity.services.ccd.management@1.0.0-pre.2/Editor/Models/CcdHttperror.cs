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
    /// CcdHttperror model
    /// <param name="code">code param</param>
    /// <param name="details">details param</param>
    /// <param name="reason">reason param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.httperror")]
    public class CcdHttperror
    {
        /// <summary>
        /// Creates an instance of CcdHttperror.
        /// </summary>
        /// <param name="code">code param</param>
        /// <param name="details">details param</param>
        /// <param name="reason">reason param</param>
        [Preserve]
        public CcdHttperror(CcdErrorCodes code = default, List<string> details = default, string reason = default)
        {
            Code = code;
            Details = details;
            Reason = reason;
        }

        /// <summary>
        /// Error Code
        /// </summary>
        [Preserve]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public CcdErrorCodes Code { get; }

        /// <summary>
        /// Details
        /// </summary>
        [Preserve]
        [DataMember(Name = "details", EmitDefaultValue = false)]
        public List<string> Details { get; }

        /// <summary>
        /// Reason
        /// </summary>
        [Preserve]
        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string Reason { get; }

    }
}

