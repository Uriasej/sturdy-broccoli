namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Enumerates the known error causes when communicating with the Lobby Service.
    /// N.B. Error code range for this service: 16000-16999
    /// </summary>
    public enum LobbyExceptionReason
    {
        #region Lobby Errors
        /// <summary>
        /// Validation check failed on Lobby e.g. in the case of a failed player id match.
        /// </summary>
        ValidationError = 16000,

        /// <summary>
        /// Lobby with the given ID was not found or has already ended.
        /// </summary>
        LobbyNotFound = 16001,

        /// <summary>
        /// Player data with the given ID was not found in the specified Lobby.
        /// </summary>
        PlayerNotFound = 16002,

        /// <summary>
        /// There was a resource conflict when attempting to access Lobby data.
        /// Potentially caused by asynchonous contestion of resources. 
        /// </summary>
        LobbyConflict = 16003,

        /// <summary>
        /// Target Lobby already has the maximum number of players. 
        /// No additional members can be added.
        /// </summary>
        LobbyFull = 16004,

        /// <summary>
        /// No accessible lobbies are currently available for quick-join.
        /// </summary>
        NoOpenLobbies = 16006,
        #endregion

        #region Http Errors
        //HTTP 400's
        InvalidArgument = 16400,
        BadRequest = 16400,
        Unauthorized = 16401,
        PaymentRequired = 16402,
        Forbidden = 16403,
        EntityNotFound = 16404,
        MethodNotAllowed = 16405,
        NotAcceptable = 16406,
        ProxyAuthenticationRequired = 16407,
        RequestTimeOut = 16408,
        Conflict = 16409,
        Gone = 16410,
        LengthRequired = 16411,
        PreconditionFailed = 16412,
        RequestEntityTooLarge = 16413,
        RequestUriTooLong = 16414,
        UnsupportedMediaType = 16415,
        RangeNotSatisfiable = 16416,
        ExpectationFailed = 16417,
        Teapot = 16418,
        Misdirected = 16421,
        UnprocessableTransaction = 16422,
        Locked = 16423,
        FailedDependency = 16424,
        TooEarly = 16425,
        UpgradeRequired = 16426,
        PreconditionRequired = 16428,
        RateLimited = 16429,
        RequestHeaderFieldsTooLarge = 16431,
        UnavailableForLegalReasons = 16451,

        //HTTP 500's
        InternalServerError = 16500,
        NotImplemented = 16501,
        BadGateway = 16502,
        ServiceUnavailable = 16503,
        GatewayTimeout = 16504,
        HttpVersionNotSupported = 16505,
        VariantAlsoNegotiates = 16506,
        InsufficientStorage = 16507,
        LoopDetected = 16508,
        NotExtended = 16510,
        NetworkAuthenticationRequired = 16511,
        #endregion

        /// <summary>
        /// NetworkError is returned when the UnityWebRequest failed with this flag set. See the exception stack trace when this reason is provided for context.
        /// </summary>
        NetworkError = 16998,
        /// <summary>
        /// Unknown is returned when a unrecognized error code is returned by the service. Check the inner exception to get more information.
        /// </summary>
        Unknown = 16999
    }
}
