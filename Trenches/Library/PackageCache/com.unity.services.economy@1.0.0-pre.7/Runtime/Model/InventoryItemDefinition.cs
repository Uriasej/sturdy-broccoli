using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single inventory item configuration.
    /// </summary>
    [Preserve] 
    public class InventoryItemDefinition : ConfigurationItemDefinition
    {
        /// <summary>
        /// Gets all the PlayersInventoryItems of this inventory item for the currently signed in player
        /// </summary>
        /// <returns>A GetInventoryResult with all the PlayersInventoryItems of this inventory item</returns>
        public async Task<GetInventoryResult> GetAllPlayersInventoryItemsAsync()
        {
            PlayerInventory.GetInventoryOptions options = new PlayerInventory.GetInventoryOptions
            {
                InventoryItemIds = new List<string> { Id }
            };
            return await Economy.PlayerInventory.GetInventoryAsync(options);
        }
    }
}
