namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Error interface
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Type
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Title
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Status
        /// </summary>
        int? Status { get; }
        /// <summary>
        /// Code
        /// </summary>
        int Code { get; }
        /// <summary>
        /// Detail
        /// </summary>
        string Detail { get; }
    }
}