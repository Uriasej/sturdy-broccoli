using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.Scripting;



namespace Unity.GameBackend.CloudCode.Models
{
    /// <summary>
    /// Validation error response when a value provided from the client does not pass validation on server
    /// <param name="type">type param</param>
    /// <param name="title">title param</param>
    /// <param name="status">status param</param>
    /// <param name="code">code param</param>
    /// <param name="detail">detail param</param>
    /// <param name="instance">instance param</param>
    /// <param name="errors">errors param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "validation-error-response")]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class ValidationErrorResponse
    {
        /// <summary>
        /// Validation error response when a value provided from the client does not pass validation on server
        /// </summary>
        /// <param name="type">type param</param>
        /// <param name="title">title param</param>
        /// <param name="status">status param</param>
        /// <param name="code">code param</param>
        /// <param name="detail">detail param</param>
        /// <param name="errors">errors param</param>
        /// <param name="instance">instance param</param>
        [Preserve]
        public ValidationErrorResponse(string type, string title, int status, int code, string detail, List<ValidationErrorBody> errors, string instance = default)
        {
            Type = type;
            Title = title;
            Status = status;
            Code = code;
            Detail = detail;
            Instance = instance;
            Errors = errors;
        }

    
        [Preserve]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public string Type{ get; }

        [Preserve]
        [DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
        public string Title{ get; }

        [Preserve]
        [DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
        public int Status{ get; }

        [Preserve]
        [DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
        public int Code{ get; }

        [Preserve]
        [DataMember(Name = "detail", IsRequired = true, EmitDefaultValue = true)]
        public string Detail{ get; }

        [Preserve]
        [DataMember(Name = "instance", EmitDefaultValue = false)]
        public string Instance{ get; }

        [Preserve]
        [DataMember(Name = "errors", IsRequired = true, EmitDefaultValue = true)]
        public List<ValidationErrorBody> Errors{ get; }
    
    }

    /// <summary>
    /// Validation error response when a value provided from the client does not pass validation on server
    /// <param name="type">type param</param>
    /// <param name="title">title param</param>
    /// <param name="status">status param</param>
    /// <param name="code">code param</param>
    /// <param name="detail">detail param</param>
    /// <param name="instance">instance param</param>
    /// <param name="errors">errors param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "validation-error-response")]
    internal class ValidationErrorResponseInternal
    {
        /// <summary>
        /// Validation error response when a value provided from the client does not pass validation on server
        /// </summary>
        /// <param name="type">type param</param>
        /// <param name="title">title param</param>
        /// <param name="status">status param</param>
        /// <param name="code">code param</param>
        /// <param name="detail">detail param</param>
        /// <param name="errors">errors param</param>
        /// <param name="instance">instance param</param>
        [Preserve]
        public ValidationErrorResponseInternal(string type, string title, int status, int code, string detail, List<ValidationErrorBodyInternal> errors, string instance = default)
        {
            Type = type;
            Title = title;
            Status = status;
            Code = code;
            Detail = detail;
            Instance = instance;
            Errors = errors;
        }

    
        [Preserve]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public string Type{ get; }

        [Preserve]
        [DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
        public string Title{ get; }

        [Preserve]
        [DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
        public int Status{ get; }

        [Preserve]
        [DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
        public int Code{ get; }

        [Preserve]
        [DataMember(Name = "detail", IsRequired = true, EmitDefaultValue = true)]
        public string Detail{ get; }

        [Preserve]
        [DataMember(Name = "instance", EmitDefaultValue = false)]
        public string Instance{ get; }

        [Preserve]
        [DataMember(Name = "errors", IsRequired = true, EmitDefaultValue = true)]
        public List<ValidationErrorBodyInternal> Errors{ get; }
    
    }
}

