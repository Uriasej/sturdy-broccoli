# Quick Reference

Below is a quick reference for the methods available in the SDK. For a more detailed look, see the documentation for the individual namespaces.

## Configuration

The methods in the `Configuration` namespace allow you to retrieve items from the global economy configuration.

For more details on these methods, see [the configuration documentation here.](./configuration.md)

The methods available are:
```cs
public async Task<List<CurrencyDefinition>> GetCurrenciesAsync()
public async Task<CurrencyDefinition> GetCurrencyAsync(string id)
public async Task<List<InventoryItemDefinition>> GetInventoryItemsAsync()
public async Task<InventoryItemDefinition> GetInventoryItemAsync(string id)
public async Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync()
public async Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id)
public async Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync()
public async Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id)
```

## Player Balances 

The methods in the `PlayerBalances` namespace allow you to retrieve and update the user's currency balances. These methods will return the balances for the currently signed in player from the Authentication SDK.

For more details on these methods, see [the player balance documentation here.](./player_balances.md)

The methods available are:
```cs
public async Task<GetBalanceResponse> GetBalancesAsync(GetBalancesOptions options = null)
public async Task<PlayerBalanceDefinition> SetBalanceAsync(string currencyId, int balance, SetBalanceOptions options = null)
public async Task<PlayerBalanceDefinition> IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options = null)
public async Task<PlayerBalanceDefinition> DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options = null)
```

## Player Inventory

The methods in the `PlayerInventory` namespace allow you to retrieve and update the player's inventory items. These methods will return inventory data for the currently signed in player from the Authentication SDK.

For more details on these methods, see [the player inventory documentation here.](./player_inventory.md)

The methods available are:
```cs
public async Task<GetInventoryResult> GetInventoryAsync(GetInventoryOptions options = null)
public async Task<PlayerInventoryInstance> AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options = null)
public async Task<PlayerInventoryInstance> UpdateInventoryInstanceAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options = null)
public async Task DeleteInventoryInstanceAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options = null)
```

## Purchases

The methods in the `Purchases` namespace allow you to make virtual purchases and redeem real money purchases for the player. These methods will make purchases for the player currently signed in with the Authentication SDK.

For more details on these methods, see [the purchases documentation here.](./purchases.md)

The methods available are:
```cs
public async Task<MakeVirtualPurchaseResult> MakeVirtualPurchaseAsync(string virtualPurchaseId, MakeVirtualPurchaseOptions options = null)
public async Task<RedeemAppleAppStorePurchaseResult> RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args)
public async Task<RedeemGooglePlayPurchaseResult> RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args)
```