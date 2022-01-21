using System;
using Unity.Services.Economy.Model;

namespace Unity.Services.Economy
{
    /// <summary>
    /// An exception that is thrown when a request to redeem an Google Play Store purchase fails in one of the following ways:
    /// invalid purchase data, invalid purchase data signature, purchase already redeemed, product ID mismatch,
    /// product ID not defined, currency max would be exceeded.
    /// </summary>
    public class EconomyGooglePlayStorePurchaseFailedException : EconomyException
    {
        /// <summary>
        /// Details on the status of the purchase and the rewards that the purchase gives the player. 
        /// </summary>
        public new RedeemGooglePlayPurchaseResult Data { get; private set; }

        internal EconomyGooglePlayStorePurchaseFailedException(EconomyExceptionReason reason, int serviceErrorCode, string description, RedeemGooglePlayPurchaseResult data, Exception e)
            : base(reason, serviceErrorCode, description, e)
        {
            Data = data;
        }
    }
}
