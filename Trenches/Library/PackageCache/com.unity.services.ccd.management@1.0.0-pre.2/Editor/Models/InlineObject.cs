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
    /// InlineObject model
    /// <param name="file">File content</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "inline_object")]
    public class InlineObject
    {
        /// <summary>
        /// Creates an instance of InlineObject.
        /// </summary>
        /// <param name="file">File content</param>
        [Preserve]
        public InlineObject(System.IO.Stream file)
        {
            File = file;
        }

    
        /// <summary>
        /// File content
        /// </summary>
        [Preserve]
        [DataMember(Name = "file", IsRequired = true, EmitDefaultValue = true)]
        public System.IO.Stream File{ get; }
    
    }
}

