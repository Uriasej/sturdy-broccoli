using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Unity.GameBackend.CloudCode.Http;
using UnityEngine.Scripting;



namespace Unity.GameBackend.CloudCode.Models
{
    /// <summary>
    /// RunScriptArguments model
    /// <param name="params">Object containing Key-Value pairs that map on to the parameter definitions for the script. Parameters are required according to the definition.</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "run_script_arguments")]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class RunScriptArguments
    {
        /// <summary>
        /// Creates an instance of RunScriptArguments.
        /// </summary>
        /// <param name="params">Object containing Key-Value pairs that map on to the parameter definitions for the script. Parameters are required according to the definition.</param>
        [Preserve]
        public RunScriptArguments(Dictionary<string, object> _params = default)
        {
            Params = new JsonObject(_params);
        }

    
        /// <summary>
        /// Object containing Key-Value pairs that map on to the parameter definitions for the script. Parameters are required according to the definition.
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "params", EmitDefaultValue = false)]
        public JsonObject Params{ get; }
    
    }

    /// <summary>
    /// RunScriptArguments model
    /// <param name="params">Object containing Key-Value pairs that map on to the parameter definitions for the script. Parameters are required according to the definition.</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "run_script_arguments")]
    internal class RunScriptArgumentsInternal
    {
        /// <summary>
        /// Creates an instance of RunScriptArguments.
        /// </summary>
        /// <param name="params">Object containing Key-Value pairs that map on to the parameter definitions for the script. Parameters are required according to the definition.</param>
        [Preserve]
        public RunScriptArgumentsInternal(Dictionary<string, object> _params = default)
        {
            Params = new JsonObjectInternal(_params);
        }

    
        /// <summary>
        /// Object containing Key-Value pairs that map on to the parameter definitions for the script. Parameters are required according to the definition.
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "params", EmitDefaultValue = false)]
        public JsonObjectInternal Params{ get; }
    
    }
}

