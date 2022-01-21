using System;
using Unity.Services.Economy.Model;

namespace Unity.Services.Economy
{
    /// <summary>
    /// An exception that is thrown when a request to redeem an Apple App Store purchase fails in one of the following ways:
    /// invalid receipt, purchase already redeemed, product ID mismatch, product ID not defined, currency max would be exceeded.
    /// </summary>
    public class EconomyAppleAppStorePurchaseFailedException : EconomyException
    {
        /// <summary>
        /// Details on the status of the purchase and the rewards that the purchase gives the player. This Data takes the same form as
        /// an RedeemAppleAppStorePurchaseResult object.
        /// </summary>
        public new RedeemAppleAppStorePurchaseResult Data { get; private set; }

        internal EconomyAppleAppStorePurchaseFailedException(EconomyExceptionReason reason, int serviceErrorCode, string description, RedeemAppleAppStorePurchaseResult data, Exception e)
            : base(reason, serviceErrorCode, description, e)
        {
            Data = data;
        }
    }
}
