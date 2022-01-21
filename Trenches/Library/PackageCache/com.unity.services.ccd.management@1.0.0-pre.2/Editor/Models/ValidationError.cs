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
    /// ValidationError model
    /// <param name="title">title param</param>
    /// <param name="status">status param</param>
    /// <param name="detail">detail param</param>
    /// <param name="details">details param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ValidationError")]
    public class ValidationError
    {
        /// <summary>
        /// Creates an instance of ValidationError.
        /// </summary>
        /// <param name="title">title param</param>
        /// <param name="status">status param</param>
        /// <param name="detail">detail param</param>
        /// <param name="details">details param</param>
        [Preserve]
        public ValidationError(string title = default, int status = default, string detail = default, List<object> details = default)
        {
            Title = title;
            Status = status;
            Detail = detail;
            Details = details;
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

        /// <summary>
        /// Details
        /// </summary>
        [Preserve]
        [DataMember(Name = "details", EmitDefaultValue = false)]
        public List<object> Details { get; }

    }
}

