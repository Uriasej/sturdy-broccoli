using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    [Preserve] 
    public class EconomyDate
    { 
        [Preserve] [JsonProperty("date")] public DateTime Date;
    }
    
    /// <summary>
    /// The base class for the more specific configuration types, e.g. CurrencyDefinition. These are used to define
    /// the resources that you create in the Unity Dashboard.
    /// </summary>
    [Preserve]
    public class ConfigurationItemDefinition
    {
        /// <summary>
        /// The configuration ID of the resource.
        /// </summary>
        [Preserve] [JsonProperty("id")] [JsonRequired] public string Id;
        /// <summary>
        /// The name of the resource.
        /// </summary>
        [Preserve] [JsonProperty("name")] [JsonRequired] public string Name;
        /// <summary>
        /// Resource type as it appears in the Unity dashboard.
        /// </summary>
        [Preserve] [JsonProperty("type")] [JsonRequired] public string Type;
        /// <summary>
        /// Any custom data associated with this resource definition.
        /// </summary>
        [Preserve] [JsonProperty("customData")] public Dictionary<string, object> CustomData;
        /// <summary>
        /// The date this resource was created.
        /// </summary>
        [Preserve] [JsonProperty("created")] public EconomyDate Created;
        /// <summary>
        /// The date this resource was last modified.
        /// </summary>
        [Preserve] [JsonProperty("modified")] public EconomyDate Modified;
    }
}
