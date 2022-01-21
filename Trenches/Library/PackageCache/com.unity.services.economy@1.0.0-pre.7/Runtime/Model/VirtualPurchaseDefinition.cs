using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a virtual purchase configuration.
    /// </summary>
    [Preserve]
    public class VirtualPurchaseDefinition : ConfigurationItemDefinition
    {
        /// <summary>
        /// A list of costs associated with this purchase.
        /// </summary>
        [Preserve] [JsonRequired] [JsonProperty("costs")] 
        public List<PurchaseItemQuantity> Costs;

        /// <summary>
        /// A list of rewards associated with this purchase.
        /// </summary>
        [Preserve] [JsonRequired] [JsonProperty("rewards")]
        public List<PurchaseItemQuantity> Rewards;
        
        /// <summary>
        /// Make this purchase. Optionally takes instance IDs of items to use in the purchase.
        /// </summary>
        /// <param name="options">(Optional) Use to set a list of item instance IDs to use in this purchase</param>
        /// <returns>A MakeVirtualPurchaseResult containing details of the purchase</returns>
        /// <exception cref="EconomyException">Thrown if purchase is unsuccessful</exception>
        public async Task<MakeVirtualPurchaseResult> MakePurchaseAsync(Purchases.MakeVirtualPurchaseOptions options = null)
        {
            return await Economy.Purchases.MakeVirtualPurchaseAsync(Id, options);
        }
        
        /// <summary>
        /// Make this purchase using the PlayersInventoryItems provided to pay the inventory item cost.
        /// </summary>
        /// <param name="playersInventoryItems">A list of PlayersInventoryItems to use in this purchase</param>
        /// <returns>A MakeVirtualPurchaseResult containing details of the purchase</returns>
        /// <exception cref="EconomyException">Thrown if purchase is unsuccessful</exception>
        public async Task<MakeVirtualPurchaseResult> MakePurchaseAsync(List<PlayersInventoryItem> playersInventoryItems)
        {
            List<string> playerInventoryItemIds = playersInventoryItems.Select(i => i.PlayersInventoryItemId).ToList();
            return await Economy.Purchases.MakeVirtualPurchaseAsync(Id, new Purchases.MakeVirtualPurchaseOptions{ PlayersInventoryItemIds = playerInventoryItemIds });
        }

        /// <summary>
        /// Check if this purchase is affordable for the currently signed in user.
        ///
        /// Note: This call is very costly, as it will make multiple API calls to check the player's current balances against
        /// the costs specified in this purchase.
        /// </summary>
        /// <returns>True if the player has the inventory/currency balances to pay for the purchase, false otherwise.</returns>
        public async Task<bool> CanPlayerAffordPurchaseAsync()
        {
            // This implementation would be improved with the change in LOSDK-502
            // Also, currently this makes a API call for each cost. That might be OK, as we'd need to do that for affordability anyway,
            // but it's a consideration before using it! If this was something we wanted we might need to discuss batching of requests?
            foreach (var cost in Costs)
            {
                var costItem = cost.Item.GetReferencedConfigurationItem();
                if (costItem.Type == EconomyRemoteConfig.currencyTypeString)
                {
                    CurrencyDefinition currency = (CurrencyDefinition)costItem;
                    var balance = await currency.GetPlayerBalanceAsync();
                    if (balance.Balance < cost.Amount)
                    {
                        return false;
                    }
                }
                else
                {
                    InventoryItemDefinition item = (InventoryItemDefinition)costItem;
                    var instances = await item.GetAllPlayersInventoryItemsAsync();
                    if (instances.PlayersInventoryItems.Count < cost.Amount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
