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
    /// Error Codes
    /// </summary>
    [Preserve]
    public enum CcdErrorCodes
    {
        /// <summary>
        /// Ok
        /// </summary>
        [EnumMember(Value = "0")]
        _0 = 1,

        /// <summary>
        /// Invalid argument
        /// </summary>
        [EnumMember(Value = "1")]
        _1 = 2,

        /// <summary>
        /// Out of range
        /// </summary>
        [EnumMember(Value = "2")]
        _2 = 3,

        /// <summary>
        /// Unauthenticated
        /// </summary>
        [EnumMember(Value = "3")]
        _3 = 4,

        /// <summary>
        /// Permission denied
        /// </summary>
        [EnumMember(Value = "4")]
        _4 = 5,

        /// <summary>
        /// Not found
        /// </summary>
        [EnumMember(Value = "5")]
        _5 = 6,

        /// <summary>
        /// Already exists
        /// </summary>
        [EnumMember(Value = "6")]
        _6 = 7,

        /// <summary>
        /// Unknown error
        /// </summary>
        [EnumMember(Value = "7")]
        _7 = 8,

        /// <summary>
        /// Internal error
        /// </summary>
        [EnumMember(Value = "8")]
        _8 = 9,

        /// <summary>
        /// Invalid operation
        /// </summary>
        [EnumMember(Value = "9")]
        _9 = 10,

        /// <summary>
        /// Organization activation is needed
        /// </summary>
        [EnumMember(Value = "10")]
        _10 = 11,

        /// <summary>
        /// Operation cannot be completed due to an incomplete upload
        /// </summary>
        [EnumMember(Value = "11")]
        _11 = 12,

        /// <summary>
        /// Too many requests
        /// </summary>
        [EnumMember(Value = "12")]
        _12 = 13

    }
}



