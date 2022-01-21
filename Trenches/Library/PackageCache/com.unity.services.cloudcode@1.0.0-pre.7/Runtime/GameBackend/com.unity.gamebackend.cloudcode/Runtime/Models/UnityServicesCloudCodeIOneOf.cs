using System;

namespace Unity.GameBackend.CloudCode.Models
{
    [Obsolete("This was made public unintentionally and should not be used.")]
    public interface IOneOf
    {
        Type Type { get; }
        object Value { get; }
    }

    internal interface IOneOfInternal
    {
        Type Type { get; }
        object Value { get; }
    }
}