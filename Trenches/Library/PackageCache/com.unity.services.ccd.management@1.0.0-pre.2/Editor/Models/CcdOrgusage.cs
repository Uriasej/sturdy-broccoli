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
    /// CcdOrgusage model
    /// <param name="id">id param</param>
    /// <param name="start_time">start_time param</param>
    /// <param name="usage">usage param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.orgusage")]
    public class CcdOrgusage
    {
        /// <summary>
        /// Creates an instance of CcdOrgusage.
        /// </summary>
        /// <param name="id">id param</param>
        /// <param name="startTime">start_time param</param>
        /// <param name="usage">usage param</param>
        [Preserve]
        public CcdOrgusage(string id = default, DateTime startTime = default, List<CcdUsage> usage = default)
        {
            Id = id;
            StartTime = startTime;
            Usage = usage;
        }

        /// <summary>
        /// Id
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; }

        /// <summary>
        /// Start Tiem
        /// </summary>
        [Preserve]
        [DataMember(Name = "start_time", EmitDefaultValue = false)]
        public DateTime StartTime { get; }

        /// <summary>
        /// Usage
        /// </summary>
        [Preserve]
        [DataMember(Name = "usage", EmitDefaultValue = false)]
        public List<CcdUsage> Usage { get; }

    }
}

