using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Services.Economy;
using UnityEngine;

namespace EconomyTools
{
    [CreateAssetMenu(fileName = "PlayerInventoriesHelper", menuName = "Economy Tools/Player Inventories Helper")]
    public class PlayerInventoriesHelper : ScriptableObject
    {
        public enum InventoriesAction
        {
            Add,
            Update
        }

        [Header("Inventories Helper")]
        public InventoriesAction action;
        public string playersInventoryItemId;
        
        [Header("Add Instance Settings")]
        public string inventoryItemId;

        [Header("Update Instance Settings")]
        public string instanceDataJson;
        
        /// <summary>
        /// </summary>
        /// <returns>Currently returns void so the event is selectable/recognised in the editor inspector</returns>
        public async void InvokeAsync() 
        {
            switch (action)
            {
                case InventoriesAction.Add when String.IsNullOrEmpty(playersInventoryItemId):
                    ThrowExceptionIfItemIdNull();
                    await Economy.PlayerInventory.AddInventoryItemAsync(inventoryItemId);
                    break;
                case InventoriesAction.Add:
                    ThrowExceptionIfItemIdNull();
                    PlayerInventory.AddInventoryItemOptions options = new PlayerInventory.AddInventoryItemOptions
                    {
                        PlayersInventoryItemId = playersInventoryItemId
                    };
                    await Economy.PlayerInventory.AddInventoryItemAsync(inventoryItemId, options);
                    break;
                case InventoriesAction.Update:
                {
                    ThrowExceptionIfMissingUpdateFields();
                    Dictionary<string, object> instanceData = JsonConvert.DeserializeObject<Dictionary<string, object>>(instanceDataJson);
                    await Economy.PlayerInventory.UpdatePlayersInventoryItemAsync(playersInventoryItemId, instanceData);
                    break;
                }
            }
        }

        void ThrowExceptionIfItemIdNull()
        {
            if (string.IsNullOrEmpty(inventoryItemId))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The inventory item ID on the player inventories helper scriptable object is empty. Please enter an ID.");
            }
        }

        void ThrowExceptionIfMissingUpdateFields()
        {
            if (string.IsNullOrEmpty(playersInventoryItemId))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The players inventory item ID on the player inventories helper scriptable object is empty. Please enter an ID.");
            }

            if (string.IsNullOrEmpty(instanceDataJson))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The custom data field on the player inventories helper scriptable object is empty. Please enter custom data.");
            }
        }
    }
}
