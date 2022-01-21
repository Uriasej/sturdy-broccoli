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
    /// CcdUserapikey model
    /// <param name="apikey">apikey param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.userapikey")]
    public class CcdUserapikey
    {
        /// <summary>
        /// Creates an instance of CcdUserapikey.
        /// </summary>
        /// <param name="apikey">apikey param</param>
        [Preserve]
        public CcdUserapikey(string apikey)
        {
            Apikey = apikey;
        }

        /// <summary>
        /// Api key
        /// </summary>
        [Preserve]
        [DataMember(Name = "apikey", IsRequired = true, EmitDefaultValue = true)]
        public string Apikey { get; }

    }
}

