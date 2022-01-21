using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace Unity.Services.Economy
{
    class EconomyRemoteConfig
    {
        internal const string currencyTypeString = "CURRENCY";
        internal const string inventoryItemTypeString = "INVENTORY_ITEM";
        internal const string virtualPurchaseTypeString = "VIRTUAL_PURCHASE";
        internal const string realMoneyPurchaseTypeString = "MONEY_PURCHASE";

        const string k_EconomyConfigKey = "economy";

        object m_FetchLock = new object();
        TaskCompletionSource<bool> m_CurrentRemoteConfigFetchTcs;

        IRemoteConfigRuntimeWrapper m_RuntimeImplementation;
        IEconomyAuthentication m_EconomyAuthHandler;

        internal EconomyRemoteConfig(IRemoteConfigRuntimeWrapper remoteConfigRuntime, IEconomyAuthentication authHandler)
        {
            m_RuntimeImplementation = remoteConfigRuntime;
            m_EconomyAuthHandler = authHandler;
        }

        // Need both settings and serializer here as the two Newtonsoft APIs for strings and JTokens take different objects for their settings...
        static JsonSerializerSettings s_SerializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        
        static JsonSerializer s_Serializer = new JsonSerializer { 
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        internal async Task<ConfigurationItemDefinition> GetEconomyItemWithKeyUsingRefreshAsync(string economyItemKey)
        {
            await EnsureRemoteConfigIsFetchedAsync();
            return GetEconomyItemWithKeyWithoutRefresh(economyItemKey);
        }

        internal ConfigurationItemDefinition GetEconomyItemWithKeyWithoutRefresh(string itemKey)
        {
            string itemAsJson = m_RuntimeImplementation.GetConfig(k_EconomyConfigKey).GetJson(itemKey);
            if (string.IsNullOrEmpty(itemAsJson) || IsEmptyJsonObject(itemAsJson))
            {
                Debug.Log("The provided economy item key doesn't exist in the fetched configuration.");
                return null;
            }
            return DeserializeConfigurationObject(itemAsJson);
        }

        static ConfigurationItemDefinition DeserializeConfigurationObject(string raw) 
        {
            try
            {
                JObject jObject = JObject.Parse(raw);

                string type = jObject["type"]?.Value<string>();
                
                if (string.IsNullOrEmpty(type))
                {
                    Debug.LogError("Economy item is missing a type, this shouldn't be possible. Returning null.");
                    return null;
                }

                switch (type)
                {
                    case currencyTypeString:
                        return jObject.ToObject<CurrencyDefinition>();
                    case inventoryItemTypeString:
                        return jObject.ToObject<InventoryItemDefinition>();
                    case virtualPurchaseTypeString:
                        return jObject.ToObject<VirtualPurchaseDefinition>();
                    case realMoneyPurchaseTypeString:
                        return jObject.ToObject<RealMoneyPurchaseDefinition>();
                    default:
                        Debug.LogWarning($"Attempted to deserialize an unknown economy data type: {type ?? "TYPE_MISSING"}. Returning null.");
                        return null;
                }
            }
            catch (JsonSerializationException e)
            {
                Debug.LogError($"Failed to deserialize configuration item: {raw} due to {e.Message}");
                return null;
            }
        }
        
        static string GetTypeStringForObject(Type type)
        {
            if (type == typeof(CurrencyDefinition))
            {
                return currencyTypeString;
            }

            if (type == typeof(InventoryItemDefinition))
            {
                return inventoryItemTypeString;
            }

            if (type == typeof(VirtualPurchaseDefinition))
            {
                return virtualPurchaseTypeString;
            }
            
            if (type == typeof(RealMoneyPurchaseDefinition))
            {
                return realMoneyPurchaseTypeString;
            }

            Debug.Log("Unknown type used as generic for Configuration request");
            return "UNKNOWN";
        }
        
        internal async Task<List<T>> GetObjectsFromRemoteConfigAsync<T>(string typeString) where T: ConfigurationItemDefinition
        {
            await EnsureRemoteConfigIsFetchedAsync();

            JObject rawRemoteConfigEconomyObject = m_RuntimeImplementation.GetConfig(k_EconomyConfigKey).config;

            return ParseSpecificObjectTypesFromFullConfig<T>(rawRemoteConfigEconomyObject, typeString);
        }

        static List<T> ParseSpecificObjectTypesFromFullConfig<T>(JObject rawConfigObject, string typeString) where T: ConfigurationItemDefinition
        {
            JEnumerable<JToken> items = rawConfigObject.PropertyValues();

            List<T> processedItems = new List<T>();
            foreach (JToken item in items)
            {
                string itemType = item["type"]?.ToString();
                if (string.IsNullOrEmpty(itemType))
                {
                    Debug.LogError("Economy item is missing a type, this shouldn't be possible");
                    continue;
                }

                if (itemType == typeString)
                {
                    try
                    {
                        T itemAsEconomyRemoteConfigCurrency = item.ToObject<T>(s_Serializer);
                        if (itemAsEconomyRemoteConfigCurrency != null)
                        {
                            processedItems.Add(itemAsEconomyRemoteConfigCurrency);
                        }
                        else
                        {
                            Debug.LogError("Couldn't deserialize item with currency type, skipping...");
                        }
                    }
                    catch (JsonSerializationException e)
                    {
                        Debug.Log($"Failed to deserialize configuration item: {item} due to {e.Message}");
                    }
                }
            }

            return processedItems;
        }

        static bool IsEmptyJsonObject(string json)
        {
            return json.Trim() == "{}";
        }

        Task<bool> EnsureRemoteConfigIsFetchedAsync()
        {
            lock (m_FetchLock)
            {
                if (m_CurrentRemoteConfigFetchTcs != null)
                {
                    return m_CurrentRemoteConfigFetchTcs.Task;
                }

                m_CurrentRemoteConfigFetchTcs = new TaskCompletionSource<bool>();

                if (string.IsNullOrEmpty(m_EconomyAuthHandler.GetPlayerId()))
                {
                    m_CurrentRemoteConfigFetchTcs.TrySetException(new EconomyException(EconomyExceptionReason.Unauthorized, Core.CommonErrorCodes.InvalidToken, "You are not signed in to the Authentication Service. Please sign in."));

                    var task = m_CurrentRemoteConfigFetchTcs.Task;

                    // Allows us to retry later, otherwise we'd be blocked at the first if check.
                    m_CurrentRemoteConfigFetchTcs = null;
                    return task;
                }
                
                m_RuntimeImplementation.SetPlayerIdentityToken(m_EconomyAuthHandler.GetAccessToken());

                IRuntimeConfigWrapper economyConfig = m_RuntimeImplementation.GetConfig(k_EconomyConfigKey);
                economyConfig.FetchCompleted += EconomyConfigFetchCompletionHandler;
                m_RuntimeImplementation.FetchConfigs(k_EconomyConfigKey);

                return m_CurrentRemoteConfigFetchTcs.Task;
            }
        }

        void EconomyConfigFetchCompletionHandler(ConfigResponse response)
        {
            lock (m_FetchLock)
            {
                var economyConfig = m_RuntimeImplementation.GetConfig(k_EconomyConfigKey);
                economyConfig.FetchCompleted -= EconomyConfigFetchCompletionHandler;
                
                if (response.status == ConfigRequestStatus.Failed)
                {
                    m_CurrentRemoteConfigFetchTcs.TrySetException(new EconomyException(EconomyExceptionReason.Unknown, Core.CommonErrorCodes.Unknown, "The request to refresh the economy remote configuration failed"));
                }
                else
                {
                    m_CurrentRemoteConfigFetchTcs.TrySetResult(true);
                }

                // Operation is now complete, we can safely restart a new operation.
                m_CurrentRemoteConfigFetchTcs = null;
            }
        }
    }
}
