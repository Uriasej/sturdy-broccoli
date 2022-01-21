namespace TusDotNetClient
{
    /// <summary>
    /// A collection of the header names used by the Tus protocol. See https://github.com/jonstodle/TusDotNetClient
    /// </summary>
    public static class TusHeaderNames
    {

        ///<inheritdoc cref="TusHeaderNames"/>
        public const string TusResumable = "tus-resumable";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string TusVersion = "tus-version";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string TusExtension = "tus-extension";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string TusMaxSize = "tus-max-size";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string TusChecksumAlgorithm = "tus-checksum-algorithm";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string UploadLength = "upload-length";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string UploadOffset = "upload-offset";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string UploadHash = "upload-hash";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string UploadMetadata = "upload-metadata";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string UploadChecksum = "upload-checksum";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string ContentLength = "content-length";
        ///<inheritdoc cref="TusHeaderNames"/>
        public const string ContentType = "content-type";
    }
}