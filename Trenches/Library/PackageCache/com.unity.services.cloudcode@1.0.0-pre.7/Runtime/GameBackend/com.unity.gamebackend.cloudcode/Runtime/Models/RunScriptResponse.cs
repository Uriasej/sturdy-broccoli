using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.GameBackend.CloudCode.Http;



namespace Unity.GameBackend.CloudCode.Models
{
    /// <summary>
    /// RunScriptResponse model
    /// <param name="output">output param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "run_script_response")]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class RunScriptResponse
    {
        /// <summary>
        /// Creates an instance of RunScriptResponse.
        /// </summary>
        /// <param name="output">output param</param>
        [Preserve]
        public RunScriptResponse(object output = default)
        {
            Output = output;
        }

    
        [Preserve]
        [DataMember(Name = "output", EmitDefaultValue = false)]
        public object Output{ get; }
    
    }

    /// <summary>
    /// RunScriptResponse model
    /// <param name="output">output param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "run_script_response")]
    internal class RunScriptResponseInternal
    {
        /// <summary>
        /// Creates an instance of RunScriptResponse.
        /// </summary>
        /// <param name="output">output param</param>
        [Preserve]
        public RunScriptResponseInternal(object output = default)
        {
            Output = output;
        }

    
        [Preserve]
        [DataMember(Name = "output", EmitDefaultValue = false)]
        public object Output{ get; }
    
    }
}

