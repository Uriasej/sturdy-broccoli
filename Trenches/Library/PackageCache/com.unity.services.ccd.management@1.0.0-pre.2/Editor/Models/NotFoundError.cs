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
    /// NotFoundError model
    /// <param name="title">title param</param>
    /// <param name="status">status param</param>
    /// <param name="detail">detail param</param>
    /// <param name="requestId">requestId param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "NotFoundError")]
    public class NotFoundError
    {
        /// <summary>
        /// Creates an instance of NotFoundError.
        /// </summary>
        /// <param name="title">title param</param>
        /// <param name="status">status param</param>
        /// <param name="detail">detail param</param>
        /// <param name="requestId">requestId param</param>
        [Preserve]
        public NotFoundError(string title = default, int status = default, string detail = default, string requestId = default)
        {
            Title = title;
            Status = status;
            Detail = detail;
            RequestId = requestId;
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
        /// Request id
        /// </summary>
        [Preserve]
        [DataMember(Name = "requestId", EmitDefaultValue = false)]
        public string RequestId { get; }

    }
}

