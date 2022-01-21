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
    /// Single error in the Validation Error Response.
    /// <param name="field">field param</param>
    /// <param name="messages">messages param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "validation-error-body")]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class ValidationErrorBody
    {
        /// <summary>
        /// Single error in the Validation Error Response.
        /// </summary>
        /// <param name="field">field param</param>
        /// <param name="messages">messages param</param>
        [Preserve]
        public ValidationErrorBody(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }

    
        [Preserve]
        [DataMember(Name = "field", IsRequired = true, EmitDefaultValue = true)]
        public string Field{ get; }

        [Preserve]
        [DataMember(Name = "messages", IsRequired = true, EmitDefaultValue = true)]
        public List<string> Messages{ get; }
    
    }

    /// <summary>
    /// Single error in the Validation Error Response.
    /// <param name="field">field param</param>
    /// <param name="messages">messages param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "validation-error-body")]
    internal class ValidationErrorBodyInternal
    {
        /// <summary>
        /// Single error in the Validation Error Response.
        /// </summary>
        /// <param name="field">field param</param>
        /// <param name="messages">messages param</param>
        [Preserve]
        public ValidationErrorBodyInternal(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }

    
        [Preserve]
        [DataMember(Name = "field", IsRequired = true, EmitDefaultValue = true)]
        public string Field{ get; }

        [Preserve]
        [DataMember(Name = "messages", IsRequired = true, EmitDefaultValue = true)]
        public List<string> Messages{ get; }
    
    }
}

