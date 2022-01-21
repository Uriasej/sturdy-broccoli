using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics.Internal;
using Unity.Services.Core;

namespace Unity.Services.Analytics
{
    public static partial class Events
    {
        internal static IConsentTracker ConsentTracker = new ConsentTracker();
        
        /// <summary>
        /// Returns identifiers of required consents we need to gather from the user
        /// in order to be allowed to sent analytics events.
        /// This method must be called every time the game starts - without checking the geolocation,
        /// no event will be sent (even if the consent was already given).
        /// If the required consent was already given, an empty list is returned.
        /// If the user already opted out from the current legislation, an empty list is returned.
        /// It involves the GeoIP call.
        /// `ConsentCheckException` is thrown if the GeoIP call was unsuccessful. 
        ///  
        /// </summary>
        /// <returns>A list of consent identifiers that are required for sending analytics events.</returns>
        /// <exception cref="ConsentCheckException">Thrown if the GeoIP call was unsuccessful.</exception>
        public static async Task<List<string>> CheckForRequiredConsents()
        {
            try
            {
                GeoIPResponse response = await ConsentTracker.CheckGeoIP();

                if (response.identifier == Consent.None)
                {
                    return new List<string>();
                } 
                
                if (ConsentTracker.IsConsentDenied())
                {
                    return new List<string>();
                } 
                
                if (!ConsentTracker.IsConsentGiven())
                {
                    return new List<string> { response.identifier };
                }
                
                return new List<string>();
                
            }
            catch (Analytics.Internal.ConsentCheckException e)
            {
                throw new ConsentCheckException((ConsentCheckExceptionReason)e.Reason, e.ErrorCode, e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Sets the consent status for the specified opt-in-based legislation (PIPL etc).
        /// The required legislation identifier can be found by calling `CheckForRequiredConsents` method.
        /// If this method is tried to be used for the incorrect legislation (PIPL outside China etc),
        /// the `ConsentCheckException` is thrown.
        /// 
        /// </summary>
        /// <param name="identifier">The legislation identifier for which the consent status should be changed.</param>
        /// <param name="consent">The consent status which should be set for the specified legislation.</param>
        /// <exception cref="ConsentCheckException">Thrown if the incorrect legislation was being provided or
        /// the required consent flow cannot be determined.</exception>
        public static void ProvideOptInConsent(string identifier, bool consent)
        {
            if (!ConsentTracker.IsGeoIpChecked())
            {
                throw new ConsentCheckException(ConsentCheckExceptionReason.ConsentFlowNotKnown,
                    CommonErrorCodes.Unknown,
                    "The required consent flow cannot be determined. Make sure CheckForRequiredConsents() method was successfully called.",
                    null);
            }
            
            try {
                if (consent == false)
                {
                    if (ConsentTracker.IsConsentGiven(identifier))
                    {
                        ConsentTracker.BeginOptOutProcess(identifier);
                        RevokeWithForgetEvent();
                        return;
                    }
                    
                    Revoke();
                }
                
                ConsentTracker.SetUserConsentStatus(identifier, consent);
            }
            catch (Analytics.Internal.ConsentCheckException e)
            {
                throw new ConsentCheckException((ConsentCheckExceptionReason)e.Reason, e.ErrorCode, e.Message, e.InnerException);
            }
        }
    }
}