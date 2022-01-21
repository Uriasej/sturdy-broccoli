using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Services.Economy.Internal;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.Inventory;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Exceptions;
using Unity.Services.Economy.Model;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests")]

namespace Unity.Services.Economy
{
    /// <summary>
    /// The PlayerInventory methods provide access to the current player's inventory items, and allow you to update them.
    /// </summary>
    public class PlayerInventory {

        readonly IInventoryApiClient m_InventoryApiClient;
        readonly IEconomyAuthentication m_EconomyAuthentication;

        internal PlayerInventory(IInventoryApiClient inventoryApiClient, IEconomyAuthentication economyAuthentication)
        {
            m_InventoryApiClient = inventoryApiClient;
            m_EconomyAuthentication = economyAuthentication;
        }

        /// Fires when the SDK updates a player's inventory item (e.g. by editing the custom data). The called function will be passed the player inventory item ID
        /// that was updated. (Note: this is the ID of the individual inventory item owned by the player, not the item configuration).
        ///
        /// Note that this will NOT fire for balance changes from elsewhere not in this instance of the SDK, for example other
        /// server-side updates or updates from other devices.
        public event Action<string> PlayersInventoryItemUpdated;
        
        /// <summary>
        /// Options for a GetInventoryAsync call.
        /// </summary>
        public class GetInventoryOptions
        {
            /// <summary>
            /// The PlayersInventoryItem IDs of the items in the players inventory that you want to retrieve.
            /// </summary>
            public List<string> PlayersInventoryItemIds = null;

            /// <summary>
            /// The configuration IDs of the items you want to retrieve.
            /// </summary>
            public List<string> InventoryItemIds = null;

            /// <summary>
            /// Used to specify the number of items to fetch per request. Defaults to 20 items.
            /// </summary>
            public int ItemsPerFetch = 20;
        }

        /// <summary>
        /// Gets the inventory items in the inventory of the player that is currently signed in.
        /// The players items are available on the returned object using the <code>PlayersInventoryItems</code> property.
        /// The results are paginated - the first set of results are initially returned, and more can be requested with the <code>GetNextAsync</code> method.
        /// The <code>HasNext</code> property indicates whether there are more results to be returned.
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="options">(Optional) Use to set request options. See GetInventoryOptions for more details.</param>
        /// <returns>A GetInventoryResult object, with properties as specified above.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<GetInventoryResult> GetInventoryAsync(GetInventoryOptions options = null)
        {
            return await GetNextInventory(null, options);
        }

        internal async Task<GetInventoryResult> GetNextInventory(string afterPlayersInventoryItemId, GetInventoryOptions options = null)
        {
            if (options == null)
            {
                options = new GetInventoryOptions();
            }
            
            m_EconomyAuthentication.CheckSignedIn();
            
            EconomyAPIErrorHandler.HandleItemsPerFetchExceptions(options.ItemsPerFetch);
            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();

            GetPlayerInventoryRequest request = new GetPlayerInventoryRequest(
                Application.cloudProjectId,
                m_EconomyAuthentication.GetPlayerId(),
                afterPlayersInventoryItemId,
                options.ItemsPerFetch,
                options.PlayersInventoryItemIds,
                options.InventoryItemIds
            );

            try
            {
                Response<PlayerInventoryResponse> response = await m_InventoryApiClient.GetPlayerInventoryAsync(request);
                
                List<PlayersInventoryItem> playersInventoryItems = ConvertToPlayersInventoryItems(response.Result.Results);

                return new GetInventoryResult(playersInventoryItems, ResponseHasNextLinks(response.Result), options.PlayersInventoryItemIds, options.InventoryItemIds);
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }
        
        /// <summary>
        /// Options for a AddInventoryItemAsync call.
        /// </summary>
        public class AddInventoryItemOptions
        {
            /// <summary>
            /// Sets the ID of the created PlayersInventoryItem. If not supplied, one will be generated.
            /// </summary>
            public string PlayersInventoryItemId = null;
            
            /// <summary>
            /// Dictionary of instance data.
            /// </summary>
            public Dictionary<string, object> InstanceData = null;
        }

        /// <summary>
        /// Adds an inventory item to the player's inventory.
        ///
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="inventoryItemId">The item ID to add</param>
        /// <param name="options">(Optional) Use to set the PlayersInventoryItem ID for the created instance and instance data.</param>
        /// <returns>The created player inventory item.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<PlayersInventoryItem> AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();
            
            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();

            AddInventoryItemRequest request = new AddInventoryItemRequest(
                Application.cloudProjectId,
                m_EconomyAuthentication.GetPlayerId(),
                new AddInventoryRequest(inventoryItemId, options?.PlayersInventoryItemId, options?.InstanceData));

            try
            {
                Response<InventoryResponse> response = await m_InventoryApiClient.AddInventoryItemAsync(request);

                PlayersInventoryItem playersInventoryItem = ConvertToPlayersInventoryItem(response.Result);
                FireInventoryItemUpdated(playersInventoryItem.PlayersInventoryItemId);

                return playersInventoryItem;
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException<ValidationErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }

        }

        /// <summary>
        /// Options for a DeletePlayersInventoryItemAsync call.
        /// </summary>
        public class DeletePlayersInventoryItemOptions
        {
            /// <summary>
            /// A write lock for optimistic concurrency.
            /// </summary>
            public string WriteLock = null;
        }
        
        /// <summary>
        /// Deletes an item in the player's inventory.
        ///
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="playersInventoryItemId">PlayersInventoryItem ID for the created inventory item</param>
        /// <param name="options">(Optional) Use to set a write lock for optimistic concurrency</param>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();
            
            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();

            DeleteInventoryItemRequest request = new DeleteInventoryItemRequest(
                Application.cloudProjectId,
                m_EconomyAuthentication.GetPlayerId(),
                playersInventoryItemId,
                new InventoryDeleteRequest(options?.WriteLock));

            try
            {
                await m_InventoryApiClient.DeleteInventoryItemAsync(request);
                FireInventoryItemUpdated(playersInventoryItemId);
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException<ValidationErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }
        
        /// <summary>
        /// Options for a UpdatePlayersInventoryItemAsync call.
        /// </summary>
        public class UpdatePlayersInventoryItemOptions
        {
            /// <summary>
            /// A write lock for optimistic concurrency.
            /// </summary>
            public string WriteLock = null;
        }

        /// <summary>
        /// Updates the instance data of an item in the player's inventory.
        ///
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="playersInventoryItemId">PlayersInventoryItem ID for the created inventory item</param>
        /// <param name="instanceData">Instance data</param>
        /// <param name="options">(Optional) Use to set a write lock for optimistic concurrency</param>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<PlayersInventoryItem> UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();
            
            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();

            UpdateInventoryItemRequest request = new UpdateInventoryItemRequest(
                Application.cloudProjectId,
                m_EconomyAuthentication.GetPlayerId(),
                playersInventoryItemId,
                new InventoryRequestUpdate(instanceData, options?.WriteLock));

            try
            {
                Response<InventoryResponse> response = await m_InventoryApiClient.UpdateInventoryItemAsync(request);
            
                PlayersInventoryItem playersInventoryItem = ConvertToPlayersInventoryItem(response.Result);

                FireInventoryItemUpdated(playersInventoryItemId);
                
                return playersInventoryItem;
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException<ValidationErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        internal static bool ResponseHasNextLinks(PlayerInventoryResponse response)
        {
            return !string.IsNullOrEmpty(response.Links?.Next);
        }

        static List<PlayersInventoryItem> ConvertToPlayersInventoryItems(List<InventoryResponse> responses)
        {
            List<PlayersInventoryItem> playersInventoryItems = new List<PlayersInventoryItem>(responses.Count);
            foreach (var response in responses)
            {
                playersInventoryItems.Add(ConvertToPlayersInventoryItem(response));
            }

            return playersInventoryItems;
        }

        internal static PlayersInventoryItem ConvertToPlayersInventoryItem(InventoryResponse response)
        {
            return new PlayersInventoryItem
            {
                PlayersInventoryItemId = response.PlayersInventoryItemId,
                InventoryItemId = response.InventoryItemId,
                InstanceData = response.InstanceData.GetAs<Dictionary<string, object>>(),
                WriteLock = response.WriteLock,
                Modified = response.Modified.Date == null ? null : new EconomyDate{Date = response.Modified.Date.Value},
                Created = response.Created.Date == null ? null : new EconomyDate{Date = response.Created.Date.Value}
            };
        }

        internal void FireInventoryItemUpdated(string playersInventoryItemId)
        {
            PlayersInventoryItemUpdated?.Invoke(playersInventoryItemId);
        }
    }
}
