using System;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Missing member handling
    /// </summary>
    public enum MissingMemberHandling
    {
        /// <summary>
        /// Error
        /// </summary>
        Error,
        /// <summary>
        /// Ignore
        /// </summary>
        Ignore
    }

    /// <summary>
    /// Deserialization setting
    /// </summary>
    public class DeserializationSettings
    {
        /// <summary>
        /// Set this to ignore or error on failed deserialization
        /// </summary>
        public MissingMemberHandling MissingMemberHandling = MissingMemberHandling.Error;
    }

}
