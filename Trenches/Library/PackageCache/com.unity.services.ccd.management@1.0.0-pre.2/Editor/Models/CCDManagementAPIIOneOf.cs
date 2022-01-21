using System;

namespace Unity.Services.CCD.Management.Models
{
    /// <summary>
    /// Public one of interface
    /// </summary>
    public interface IOneOf
    {
        /// <summary>
        /// Type
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// Value
        /// </summary>
        object Value { get; }
    }
}