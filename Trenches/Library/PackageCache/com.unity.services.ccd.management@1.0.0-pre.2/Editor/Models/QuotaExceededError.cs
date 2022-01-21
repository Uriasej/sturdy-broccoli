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
    /// QuotaExceededError model
    /// <param name="title">title param</param>
    /// <param name="status">status param</param>
    /// <param name="detail">detail param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "QuotaExceededError")]
    public class QuotaExceededError
    {
        /// <summary>
        /// Creates an instance of QuotaExceededError.
        /// </summary>
        /// <param name="title">title param</param>
        /// <param name="status">status param</param>
        /// <param name="detail">detail param</param>
        [Preserve]
        public QuotaExceededError(string title = default, int status = default, string detail = default)
        {
            Title = title;
            Status = status;
            Detail = detail;
        }


        /// <summary>
        /// Title
        /// </summary>
        [Preserve]
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; }

        /// <summary>
        /// Status
        /// </summary>
        [Preserve]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int Status { get; }

        /// <summary>
        /// Detail
        /// </summary>
        [Preserve]
        [DataMember(Name = "detail", EmitDefaultValue = false)]
        public string Detail { get; }

    }
}

