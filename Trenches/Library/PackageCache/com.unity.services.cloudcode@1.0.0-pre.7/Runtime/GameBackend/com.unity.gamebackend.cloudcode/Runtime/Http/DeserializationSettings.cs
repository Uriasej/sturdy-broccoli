using System;

namespace Unity.GameBackend.CloudCode.Http
{   
    [Obsolete("This was made public unintentionally and should not be used.")]
    public enum MissingMemberHandling
    {
        Error,
        Ignore
    }

    [Obsolete("This was made public unintentionally and should not be used.")]
    public class DeserializationSettings
    {
        public MissingMemberHandling MissingMemberHandling = MissingMemberHandling.Error;
    }

    internal enum MissingMemberHandlingInternal
    {
        Error,
        Ignore
    }

    internal class DeserializationSettingsInternal
    {
        public MissingMemberHandlingInternal MissingMemberHandling = MissingMemberHandlingInternal.Error;
    }
}
