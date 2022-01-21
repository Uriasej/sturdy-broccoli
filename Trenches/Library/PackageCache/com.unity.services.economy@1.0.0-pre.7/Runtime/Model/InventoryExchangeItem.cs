using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a player's inventory item that was part of a purchase.
    /// </summary>
    [Preserve]
    public class InventoryExchangeItem
    {
        [Preserve]
        [JsonConstructor]
        public InventoryExchangeItem(string id, int amount, List<string> playersInventoryItemIds)
        {
            Id = id;
            Amount = amount;
            PlayersInventoryItemIds = playersInventoryItemIds;
        }

        /// <summary>
        /// The configuration ID of this inventory item.
        /// </summary>
        [Preserve] public string Id;
        /// <summary>
        /// The amount of these items that were used/rewarded to make the purchase.
        /// </summary>
        [Preserve] public int Amount;
        /// <summary>
        /// A list of ID's of the PlayersInventoryItem's that were used/rewarded in this purchase. 
        /// </summary>
        [Preserve] public List<string> PlayersInventoryItemIds;
    }
}
