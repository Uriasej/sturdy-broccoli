using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace Unity.Services.Economy.Exceptions
{
    static class EconomyAPIErrorHandler
    {
        static Dictionary<long, string> errorDetails = new Dictionary<long, string>
        {
            { 400, "Some of the arguments passed to the Economy request were invalid. Please check the requirements and try again." },
            { 401, "Permission denied when making a request to the Economy SDK. Have you signed in with the Authentication SDK?" },
            { 403, "Permission denied when making a request to the Economy SDK. Have you signed in with the Authentication SDK?" },
            { 404, "The requested entity was not found - please make sure it exists then try again." },
            { 409, "There was a conflict when updating the entity. Please make sure any write locks are using the correct values." },
            { 422, "Either the costs for a purchase were not met, you tried to exceed a currency maximum/minimum or this purchase has already been redeemed." },
            { 429, "Too many requests have been sent, so this device has been rate limited. Please try again later." }
        };

        internal static EconomyException HandleException(HttpException<BasicErrorResponse> e)
        {
            var message = e.ActualError?.Detail;
            var httpStatusCode = e.Response.StatusCode;
            var errorCode = e.ActualError?.Code ?? 0;
            
            if (e.Response.IsNetworkError)
            {
                return new EconomyException(EconomyExceptionReason.NetworkError, Core.CommonErrorCodes.TransportError, message ?? "The request to the Economy service failed - make sure you're connected to an internet connection and try again.", e);
            }

            if (message == null)
            {
                if (errorDetails.ContainsKey(httpStatusCode))
                {
                    message = errorDetails[httpStatusCode];
                }
                else
                {
                    message = e.Message;
                }
            }
            
            // Temporary fix while we wait for MPSSDK-89. Currently Unity logs the inner most exceptions
            // but typically the outer most exception is most relevant to the problem
            Debug.LogError("EconomyException: " + message);

            return new EconomyException(httpStatusCode, errorCode, message, e);
        }
        
        internal static EconomyValidationException HandleException(HttpException<ValidationErrorResponse> e)
        {
            var message = "There was a validation error. Check 'Details' for more information.";

            EconomyValidationException exception = new EconomyValidationException(e.Response.StatusCode, 
                e.ActualError.Code, message, e);

            foreach (var error in e.ActualError.Errors)
            {
                exception.Details.Add(new EconomyValidationErrorDetail(error));
            }

            // Temporary fix while we wait for MPSSDK-89. Currently Unity logs the inner most exceptions
            // but typically the outer most exception is most relevant to the problem
            Debug.LogError("EconomyValidationException: " + message);

            return exception;
        }

        internal static EconomyException HandleException(HttpException e)
        {
            var message = e.Response.ErrorMessage;
            var httpStatusCode = e.Response.StatusCode;
            var errorCode = (int)e.Response.StatusCode + 14000;
            
            if (e.Response.IsNetworkError)
            {
                return new EconomyException(EconomyExceptionReason.NetworkError, Core.CommonErrorCodes.TransportError, message ?? "The request to the Economy service failed - make sure you're connected to an internet connection and try again.", e);
            }

            if (message == null)
            {
                message = errorDetails.ContainsKey(httpStatusCode) ? errorDetails[httpStatusCode] : "An unknown error occurred in the Economy SDK.";
            }
            
            // Temporary fix while we wait for MPSSDK-89. Currently Unity logs the inner most exceptions
            // but typically the outer most exception is most relevant to the problem
            Debug.LogError("EconomyException: " + message);    
            
            return new EconomyException(httpStatusCode, errorCode, message, e);
        }
        
        internal static EconomyAppleAppStorePurchaseFailedException HandleAppleAppStoreFailedExceptions(HttpException<ErrorResponsePurchaseAppleappstoreFailed> e)
        {
            RedeemAppleAppStorePurchaseResult convertedErrorData = Purchases.ConvertBackendApplePurchaseModelToSDKModel(e.ActualError.Data);
            
            // Temporary fix while we wait for MPSSDK-89. Currently Unity logs the inner most exceptions
            // but typically the outer most exception is most relevant to the problem
            Debug.LogError("EconomyAppleAppStorePurchaseFailedException: " + e.ActualError.Detail);    

            return new EconomyAppleAppStorePurchaseFailedException(EconomyExceptionReason.UnprocessableTransaction, e.ActualError.Code, e.ActualError.Detail, convertedErrorData, e);
        }
        
        internal static EconomyGooglePlayStorePurchaseFailedException HandleGoogleStoreFailedExceptions(HttpException<ErrorResponsePurchaseGoogleplaystoreFailed> e)
        {
            RedeemGooglePlayPurchaseResult convertedErrorData = Purchases.ConvertBackendGooglePurchaseModelToSDKModel(e.ActualError.Data);
            
            // Temporary fix while we wait for MPSSDK-89. Currently Unity logs the inner most exceptions
            // but typically the outer most exception is most relevant to the problem
            Debug.LogError("EconomyGooglePlayStorePurchaseFailedException: " + e.ActualError.Detail);    

            return new EconomyGooglePlayStorePurchaseFailedException(EconomyExceptionReason.UnprocessableTransaction, e.ActualError.Code, e.ActualError.Detail, convertedErrorData, e);
        }

        internal static void HandleItemsPerFetchExceptions(int itemsPerFetch)
        {
            if (itemsPerFetch < 1 || itemsPerFetch > 100)
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Core.CommonErrorCodes.Unknown, $"Items per fetch of {itemsPerFetch} is not valid. Please set it between 1 and 100 inclusive.");
            }
        }
    }
}
