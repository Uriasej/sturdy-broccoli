using Unity.Services.Core.Editor;

namespace Unity.Services.Analytics.Editor
{
    /// <summary>
    /// Implementation of the <see cref="IEditorGameServiceIdentifier"/> for the Analytics package.
    /// </summary>
    public struct AnalyticsIdentifier : IEditorGameServiceIdentifier
    {
        /// <inheritdoc/>
        public string GetKey() => "Analytics";
    }
}
