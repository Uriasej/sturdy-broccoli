using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class InventoriesUIExample : MonoBehaviour
{
    // Get config
    [SerializeField]
    TextMeshProUGUI m_GetConfigsText;
    [SerializeField]
    TMP_InputField m_GetConfigItemIdInput;
    
    // Get instance
    [SerializeField]
    TextMeshProUGUI m_GetPlayersInventoryItemsText;
    
    // Add instance
    [SerializeField]
    TextMeshProUGUI m_AddInventoryItemText;
    [SerializeField]
    TMP_InputField m_AddInventoryItemCustomIdInput;
    [SerializeField]
    TMP_InputField m_AddInventoryItemConfigIdInput;
    
    // Delete instance
    [SerializeField]
    TextMeshProUGUI m_DeleteInstanceText;
    [SerializeField]
    TMP_InputField m_DeletePlayersInventoryItemIdInput;
    
    // Update instance
    [SerializeField]
    TextMeshProUGUI m_UpdatePlayersInventoryItemText;
    [SerializeField]
    TMP_InputField m_UpdatePlayersInventoryItemIdInput;
    [SerializeField]
    TMP_InputField m_UpdatePlayersInventoryItemInstanceDataInput;

    GetInventoryResult m_LatestGetInventoryResponse = new GetInventoryResult(new List<PlayersInventoryItem>(), false, new List<string>(), new List<string>());
    bool m_HasNext;
    
    [SerializeField]
    int m_ItemsPerFetch = 20;

    async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += delegate
        {
            Debug.Log("All signed in and ready to go!");
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void FetchInventoryItems()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        }
        
        string outputString = "";
        List<InventoryItemDefinition> items = await Economy.Configuration.GetInventoryItemsAsync();
        ClearOutputTextBoxes();
        if (items.Count == 0)
        {
            m_GetConfigsText.text = "No items";
        }
        else
        {
            foreach (var item in items)
            {
                outputString += $"ID: {item.Id}, Name: {item.Name}\n";
            }
            m_GetConfigsText.text = outputString;    
        }
    }
    
    public async void FetchInventoryItem()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        }
        
        if (string.IsNullOrEmpty(m_GetConfigItemIdInput.text))
        {
            Debug.Log("Please enter an item ID");
            return;
        }
        
        InventoryItemDefinition inventoryItem = await Economy.Configuration.GetInventoryItemAsync(m_GetConfigItemIdInput.text);
        ClearOutputTextBoxes();
        if (inventoryItem == null)
        {
            m_GetConfigsText.text = "Item not found";
        } else 
        {
            m_GetConfigsText.text = $"ID: {inventoryItem.Id}, Name: {inventoryItem.Name}\n";
        }
    }
    
    public async void FetchInstances()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        }
        
        string outputString = "";
        
        PlayerInventory.GetInventoryOptions options = new PlayerInventory.GetInventoryOptions
        {
            ItemsPerFetch = m_ItemsPerFetch
        };
        GetInventoryResult response = await Economy.PlayerInventory.GetInventoryAsync(options);
        ClearOutputTextBoxes();
        
        m_LatestGetInventoryResponse = response;
        m_HasNext = response.HasNext;

        if (response.PlayersInventoryItems.Count == 0)
        {
            m_GetPlayersInventoryItemsText.text = "No instances";
        }
        else
        {
            foreach (var playersInventoryItem in response.PlayersInventoryItems)
            {
                outputString += $"{playersInventoryItem.InventoryItemId}: {playersInventoryItem.PlayersInventoryItemId}\n";
                if (playersInventoryItem.InstanceData != null)
                {
                    outputString += "Custom Data: ";
                    foreach (var key in playersInventoryItem.InstanceData.Keys)
                    {
                        outputString += $"{key} - {playersInventoryItem.InstanceData[key]} | ";
                    }    
                }
                outputString += "\n";
            }
            m_GetPlayersInventoryItemsText.text = outputString;   
        }
    }

    public async void FetchNextInstances()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        }

        string outputString = "";

        if (!m_HasNext)
        {
            Debug.Log("Economy: There are no available pages of results.");
            return;
        }
        
        Debug.Log("Getting next page...");

        
        GetInventoryResult nextResponse = await m_LatestGetInventoryResponse.GetNextAsync(m_ItemsPerFetch);
        ClearOutputTextBoxes();
        
        m_LatestGetInventoryResponse = nextResponse;
        m_HasNext = nextResponse.HasNext;

        foreach (var playersInventoryItem in nextResponse.PlayersInventoryItems)
        {
            outputString += $"{playersInventoryItem.InventoryItemId}: {playersInventoryItem.PlayersInventoryItemId}\n";
        }

        m_GetPlayersInventoryItemsText.text += outputString;
    }

    public async void AddInstance()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        };
        
        string outputString = "";
        string playersInventoryItemId = null;
        
        if (!string.IsNullOrEmpty(m_AddInventoryItemCustomIdInput.text))
        {
            playersInventoryItemId = m_AddInventoryItemCustomIdInput.text;
        }

        PlayerInventory.AddInventoryItemOptions options = new PlayerInventory.AddInventoryItemOptions
        {
            PlayersInventoryItemId = playersInventoryItemId
        };
        PlayersInventoryItem playersInventoryItem = await Economy.PlayerInventory.AddInventoryItemAsync(m_AddInventoryItemConfigIdInput.text, options);
        ClearOutputTextBoxes();
        
        if (playersInventoryItem != null)
        {
            outputString += $"New {playersInventoryItem.InventoryItemId} add to players inventory with ID {playersInventoryItem.PlayersInventoryItemId}";
        }

        m_AddInventoryItemText.text = outputString;
    }
    
    public async void DeleteInstance()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        };
        
        if (string.IsNullOrEmpty(m_DeletePlayersInventoryItemIdInput.text))
        {
            Debug.Log("Please enter the players inventory ID of the item you want to delete.");
        }

        await Economy.PlayerInventory.DeletePlayersInventoryItemAsync(m_DeletePlayersInventoryItemIdInput.text);
        ClearOutputTextBoxes();
        
        m_DeleteInstanceText.text = $"Deleted players inventory item with ID {m_DeletePlayersInventoryItemIdInput.text}";
    }
    
    public async void UpdateInstance()
    {
        if (!IsAuthenticationSignedIn())
        {
            return;
        };
        
        if (string.IsNullOrEmpty(m_UpdatePlayersInventoryItemIdInput.text))
        {
            Debug.Log("Please enter the players inventory item ID of the item you want to update.");
        }
        
        Dictionary<string, object> instanceData = JsonConvert.DeserializeObject<Dictionary<string, object>>(m_UpdatePlayersInventoryItemInstanceDataInput.text);

        PlayersInventoryItem instance = await Economy.PlayerInventory.UpdatePlayersInventoryItemAsync(m_UpdatePlayersInventoryItemIdInput.text, instanceData);
        ClearOutputTextBoxes();
        
        m_UpdatePlayersInventoryItemText.text = $"Updated instance {instance.PlayersInventoryItemId}\n";

        if (instance.InstanceData != null)
        {
            m_UpdatePlayersInventoryItemText.text += $"Custom Data: ";
            foreach (var key in instance.InstanceData.Keys)
            {
                m_UpdatePlayersInventoryItemText.text += $"{key} - {instance.InstanceData[key]} | ";
            }
        }
    }

    void ClearOutputTextBoxes()
    {
        m_GetConfigsText.text = "";
        m_GetPlayersInventoryItemsText.text = "";
        m_AddInventoryItemText.text = "";
        m_DeleteInstanceText.text = "";
        m_UpdatePlayersInventoryItemText.text = "";
    }

    static bool IsAuthenticationSignedIn()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Wait until sign in is done");
            return false;
        }

        return true;
    }
}
