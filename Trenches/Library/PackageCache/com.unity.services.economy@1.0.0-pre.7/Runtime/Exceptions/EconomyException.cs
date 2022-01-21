using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Services.Economy.Internal.Models;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Editor")]
namespace Unity.Services.Economy
{        
    /// <summary>
    /// An enum of possible reasons that Economy would throw an exception. These are mapped to particular HTTP status
    /// codes.
    /// </summary>
    public enum EconomyExceptionReason : long
    {
        Unknown = 0,
        NetworkError = 1,
        
        InvalidArgument = 400,
        Unauthorized = 401,
        Forbidden = 403,
        EntityNotFound = 404,
        RequestTimeOut = 408,
        Conflict = 409,
        UnprocessableTransaction = 422,
        RateLimited = 429,
        
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
    }

    /// <summary>
    /// An exception specific to the Economy service.
    /// </summary>
    public class EconomyException : Core.RequestFailedException
    {
        internal static readonly string unknownError = "An unknown error occurred in the Economy SDK.";
        
        /// <summary>
        /// The reason the exception was thrown, selected from the EconomyExceptionReason enum.
        /// </summary>
        public EconomyExceptionReason Reason { get; private set; }
        
        internal EconomyException(EconomyExceptionReason reason, int serviceErrorCode, string description) 
            : base(serviceErrorCode, description ?? unknownError)
        {
            Reason = reason;
        }
        
        internal EconomyException(EconomyExceptionReason reason, int serviceErrorCode, string description, Exception inner) 
            : base(serviceErrorCode, description ?? unknownError, inner)
        {
            Reason = reason;
        }

        internal EconomyException(long httpStatusCode, int serviceErrorCode, string description, Exception inner)
            : base(serviceErrorCode, description ?? unknownError, inner)
        {
            Reason = GetEconomyExceptionReason(httpStatusCode);
        }

        internal EconomyExceptionReason GetEconomyExceptionReason(long httpStatusCode)
        {
            if (Enum.IsDefined(typeof(EconomyExceptionReason), httpStatusCode))
            {
                return (EconomyExceptionReason)httpStatusCode;
            }
            
            return EconomyExceptionReason.Unknown;
        }
    }
    
    /// <summary>
    /// Represents a validation error from the Economy service.
    /// </summary>
    public class EconomyValidationException: EconomyException
    {
        /// <summary>
        /// The reason the exception was thrown, selected from the EconomyExceptionReason enum.
        /// </summary>
        public EconomyExceptionReason Reason { get; private set; }
        
        /// <summary>
        /// A list of errors returned from the API's Validation Error Response.
        /// </summary>
        public List<EconomyValidationErrorDetail> Details { get; private set; }

        internal EconomyValidationException(long httpStatusCode, int serviceErrorCode, string description, Exception innerException)
            : base(httpStatusCode, serviceErrorCode, description ?? unknownError, innerException)
        {
            Reason = GetEconomyExceptionReason(httpStatusCode);
            Details = new List<EconomyValidationErrorDetail>();
        }
        
        internal EconomyValidationException(long httpStatusCode, int serviceErrorCode, string description, 
            List<EconomyValidationErrorDetail> details, Exception innerException)
            : base(httpStatusCode, serviceErrorCode, description ?? unknownError, innerException)
        {
            Reason = GetEconomyExceptionReason(httpStatusCode);
            Details = details;
        }
    }
    
    public class EconomyValidationErrorDetail
    {
        /// <summary>
        /// Single error in the Validation Error Response.
        /// </summary>
        /// <param name="field">The field in the data that caused the error.</param>
        /// <param name="messages">Messages that describe the errors.</param>
        public EconomyValidationErrorDetail(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }

        internal EconomyValidationErrorDetail(ValidationErrorBody errorBody)
        {
            Field = errorBody.Field;
            Messages = errorBody.Messages;
        }

        /// <summary>
        /// The field in the data that caused the error.
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// Messages that describe the errors.
        /// </summary>
        public List<string> Messages { get; }
    }
}
