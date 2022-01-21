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
    /// InternalServerError model
    /// <param name="title">title param</param>
    /// <param name="status">status param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "InternalServerError")]
    public class InternalServerError
    {
        /// <summary>
        /// Creates an instance of InternalServerError.
        /// </summary>
        /// <param name="title">title param</param>
        /// <param name="status">status param</param>
        [Preserve]
        public InternalServerError(string title = default, int status = default)
        {
            Title = title;
            Status = status;
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

    }
}

