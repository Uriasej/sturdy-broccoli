using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// The result from redeeming an Apple App Store purchase.
    /// </summary>
    public class RedeemAppleAppStorePurchaseResult
    {
        [Preserve]
        [JsonConstructor]
        public RedeemAppleAppStorePurchaseResult(AppleVerification verification, Rewards rewards)
        { 
            Verification = verification;
            Rewards = rewards;
        }

        /// <summary>
        /// The receipt verification details from the Apple App Store validation service.
        /// </summary>
        [Preserve]
        public AppleVerification Verification;

        /// <summary>
        /// The Rewards given in exchange for this purchase.
        /// </summary>
        [Preserve]
        public Rewards Rewards;
    }
}
