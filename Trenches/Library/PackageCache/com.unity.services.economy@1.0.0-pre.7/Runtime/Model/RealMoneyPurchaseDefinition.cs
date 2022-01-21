using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// The store identifiers.
    /// </summary>
    [Preserve]
    public class StoreIdentifiers
    {
        /// <summary>
        /// Apple App Store identifier
        /// </summary>
        [Preserve] [JsonRequired] [JsonProperty("appleAppStore")] 
        public string AppleAppStore;
        
        /// <summary>
        /// Google Play Store identifier
        /// </summary>
        [Preserve] [JsonRequired] [JsonProperty("googlePlayStore")] 
        public string GooglePlayStore;
    }

    /// <summary>
    /// Represents a single real money purchase configuration.
    /// </summary>
    [Preserve]
    public class RealMoneyPurchaseDefinition : ConfigurationItemDefinition
    {
        /// <summary>
        /// The store identifiers for this purchase.
        /// </summary>
        [Preserve] [JsonRequired] [JsonProperty("storeIdentifiers")] 
        public StoreIdentifiers StoreIdentifiers;
        
        /// <summary>
        /// The rewards associated with this purchase
        /// </summary>
        [Preserve] [JsonRequired] [JsonProperty("rewards")]
        public List<PurchaseItemQuantity> Rewards;
    }
}
