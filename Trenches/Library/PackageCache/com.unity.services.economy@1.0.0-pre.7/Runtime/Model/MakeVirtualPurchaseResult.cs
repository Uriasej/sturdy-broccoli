using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Returned after making a virtual purchase and contains the Costs and Rewards associate with the purchase.
    /// </summary>
    [Preserve]
    public class MakeVirtualPurchaseResult
    {
        [Preserve]
        [JsonConstructor]
        public MakeVirtualPurchaseResult(Costs costs, Rewards rewards)
        {
            Costs = costs;
            Rewards = rewards;
        }

        /// <summary>
        /// Represents the Costs that were spent in this purchase. 
        /// </summary>
        [Preserve] public Costs Costs;
        /// <summary>
        /// Represents the Rewards that were given in exchange for this purchase.
        /// </summary>
        [Preserve] public Rewards Rewards;
    }
}
