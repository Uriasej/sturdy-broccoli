using System;
using System.Text;
using Unity.GameBackend.CloudCode.Http;
using Unity.GameBackend.CloudCode.Models;
using Unity.Services.Core;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    /// <summary>
    /// Exception for results failures from Cloud Code
    /// </summary>
    public class CloudCodeException : RequestFailedException
    {
        private CloudCodeException(int errorCode, string message)
            : base(errorCode, message) { }

        /// <summary>
        /// Exception for results failures from Cloud Code
        /// </summary>
        /// <param name="errorCode">The service error code for this exception</param>
        /// <param name="message">The error message that explains the reason for the exception, or an empty string</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public CloudCodeException(int errorCode, string message, Exception innerException)
            : base(errorCode, message, innerException) { }

        string m_Message = null;
        
        public override string ToString()
        {
            if (m_Message == null)
            {
                if (InnerException is HttpException<BasicErrorResponseInternal> err)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(err.Message);

                    if (err.ActualError != null)
                    {
                        sb.AppendLine(err.ActualError.Title);

                        if (!String.IsNullOrEmpty(err.ActualError.Detail))
                        {
                            sb.AppendLine(err.ActualError.Detail);
                        }

                        if (err.ActualError.Details != null)
                        {
                            foreach (object errorMessage in err.ActualError.Details)
                            {
                                sb.AppendLine(errorMessage.ToString());
                            }
                        }
                    }
                    m_Message = sb.ToString();
                    return m_Message;
                }

                if (InnerException is HttpException<ValidationErrorResponseInternal> validationErr)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(validationErr.Message);

                    if (validationErr.ActualError != null)
                    {
                        sb.AppendLine(validationErr.ActualError.Title);

                        if (!String.IsNullOrEmpty(validationErr.ActualError.Detail))
                        {
                            sb.AppendLine(validationErr.ActualError.Detail);
                        }

                        if (validationErr.ActualError.Errors != null)
                        {
                            foreach (var errorMessage in validationErr.ActualError.Errors)
                            {
                                sb.AppendLine($"{errorMessage.Field}: {String.Join(",", errorMessage.Messages)}");
                            }
                        }
                    }

                    m_Message = sb.ToString();
                    return m_Message;
                }

                if (InnerException is HttpException httpException)
                {
                    return httpException.Response.ErrorMessage ?? "Unknown Error";
                } 

                return InnerException?.Message ?? "Unknown Error";
            }

            return m_Message;
        }

        public override string Message => ToString();
    }
}
