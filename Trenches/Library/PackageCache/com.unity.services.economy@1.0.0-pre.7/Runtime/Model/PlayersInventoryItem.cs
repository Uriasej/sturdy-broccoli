using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single player's inventory item. This is an inventory item unique to a specific player.
    /// </summary>
    [Preserve]
    public class PlayersInventoryItem
    {
        [Preserve]
        public PlayersInventoryItem(string playersInventoryItemId = default(string), string inventoryItemId = default(string), Dictionary<string, object> instanceData = default(Dictionary<string, object>), 
            string writeLock = default(string), EconomyDate created = default(EconomyDate), EconomyDate modified = default(EconomyDate))
        {
            PlayersInventoryItemId = playersInventoryItemId;
            InventoryItemId = inventoryItemId;
            InstanceData = instanceData;
            WriteLock = writeLock;
            Created = created;
            Modified = modified;
        }
        
        /// <summary>
        /// The ID of the unique item specific to this player's inventory.
        /// </summary>
        [Preserve] public string PlayersInventoryItemId;
        /// <summary>
        /// The configuration ID of the inventory item.
        /// </summary>
        [Preserve] public string InventoryItemId;
        /// <summary>
        /// Any instance data specific to this unique item in the player's inventory.
        /// </summary>
        [Preserve] public Dictionary<string, object> InstanceData;
        /// <summary>
        /// The current WriteLock string.
        /// </summary>
        [Preserve] public string WriteLock;
        /// <summary>
        /// The date this players inventory item was created as an EconomyDate object.
        /// </summary>
        [Preserve] public EconomyDate Created;
        /// <summary>
        /// The date this players inventory item was modified as an EconomyDate object.
        /// </summary>
        [Preserve] public EconomyDate Modified;

        /// <summary>
        /// Gets the configuration definition associated with this player's inventory item.
        /// </summary>
        /// <returns>The InventoryItemDefinition associated with this player's inventory item</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<InventoryItemDefinition> GetItemDefinitionAsync()
        {
            return await Economy.Configuration.GetInventoryItemAsync(InventoryItemId);
        }
    }
}
