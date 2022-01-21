# Configuration

The methods in the `Configuration` namespace allow you to retrieve items from the global economy configuration.

## Currencies

#### GetCurrenciesAsync

Retrieves all currencies currently configured in the Economy service. Returns a list of `CurrencyDefinition` objects. This method is asynchronous.

```cs
List<CurrencyDefinition> definitions = await Economy.Configuration.GetCurrenciesAsync();
```

#### GetCurrencyAsync

Retrieves a specific `CurrencyDefinition` using a currency ID. Returns null if the currency doesn't exist.

```cs
string currencyID = "GOLD_BARS";
CurrencyDefinition goldCurrencyDefinition = await Economy.Configuration.GetCurrencyAsync(currencyID);
```

### CurrencyDefinition

A `CurrencyDefinition` object represents a single currency configuration and contains the following data:
- `Id`: The currency ID
- `Name`: The human readable currency name
- `Type`: The type of item as defined in the Economy dashboard (for all CurrencyDefinition objects this will be `CURRENCY`).
- `Initial`: The amount of currency a player initially is given
- `Max`: (Optional, a value of 0 indicates no maximum) The maximum amount of currency available for a player to own
- `CustomData`: Any custom data associated with this currency definition, as a `Dictionary<string, object>`. See below for more details.
- `Created`: The date this currency was created. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `Modified`: The date this currency was modified. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).

It also has the following convenience methods:

#### GetPlayerBalanceAsync

This method gets the balance for the currently signed in player of the currency specified in the `CurrencyDefition`.
It returns a `PlayerBalance` as specified in the [player balances docs](./player_balances.md).

```cs
string currencyID = "GOLD_BARS";
CurrencyDefinition goldCurrencyDefinition = await Economy.Configuration.GetCurrencyAsync(currencyID);
PlayerBalance playersGoldBarBalance = await goldCurrencyDefinition.GetPlayerBalanceAsync();
```

## Inventories

#### GetInventoryItemsAsync

Retrieves all inventory items currently configured in the Economy service. Returns a list of `InventoryItemDefinition` objects. This method is asynchronous.

```cs
List<InventoryItemDefinition> definitions = await Economy.Configuration.GetInventoryItemsAsync();
```
#### GetInventoryItemAsync

Retrieves a specific `InventoryItemDefinition` using an item ID. Returns null if the item doesn't exist.

```cs
string itemID = "SWORD";
InventoryItemDefinition definition = await Economy.Configuration.GetInventoryItemAsync(itemID);
```

### InventoryItemDefinition

A `InventoryItemDefinition` object represents a single inventory item configuration, and contains the following data:
- `Id`: The inventory item ID
- `Name`: The human readable name
- `Type`: The type of item as defined in the Economy dashboard (for all InventoryItemDefinition objects this will be `INVENTORY_ITEM`).
- `CustomData`: Any custom data associated with this item definition, as a `Dictionary<string, object>`. See below for more details.
- `Created`: The date this item was created. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `Modified`: The date this item was modified. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).

It also contains the following helper methods:

#### GetAllPlayersInventoryItemsAsync

Gets all of the players inventory items for the currently logged in player. Returns a `GetInventoryResult` as defined
in [the player inventory docs](./player_inventory.md).

```cs
string itemID = "SWORD";
InventoryItemDefinition definition = await Economy.Configuration.GetAllPlayersInventoryItemsAsync(itemID);
GetInventoryResult allThePlayersSwords = await definition.GetAllInstances();
```

## Purchases

### Virtual Purchases

#### GetVirtualPurchasesAsync

Retrieves all virtual purchases currently configured in the Economy service. Returns a list of `VirtualPurchaseDefinition` objects.

```cs
List<VirtualPurchaseDefinition> definitions = await Economy.Configuration.GetVirtualPurchasesAsync();
```

#### GetVirtualPurchaseAsync

Retrieves a single virtual purchase currently configured in the Economy service. Returns a single `VirtualPurchaseDefinition` object.

```cs
string purchaseId = "VIRTUAL_PURCHASE_ID"
VirtualPurchaseDefinition definition = await Economy.Configuration.GetVirtualPurchaseAsync(purchaseId);
```

#### VirtualPurchaseDefinition

A `VirtualPurchaseDefinition` object represents a virtual purchase definition from your configuration. It is made up of a number of component objects as listed below:

The `VirtualPurchaseDefinition` has the following fields:
- `Id`: The purchase definition ID
- `Name`: The human readable name
- `Type`: The type of item as defined in the Economy dashboard (for all VirtualPurchaseDefinition objects this will be `VIRTUAL_PURCHASE`)
- `CustomData`: Any custom data associated with this purchase definition, as a `Dictionary<string, object>`. See below for more details.
- `Created`: The date this purchase definition was created. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `Modified`: The date this purchase definition was modified. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `Costs`: A list of the costs associated with this purchase, as a `List<PurchaseItemQuantity>` (see below).
- `Rewards`: A list of the rewards associated with this purchase, as a `List<PurchaseItemQuantity>` (see below).

### Real Money Purchases

#### GetRealMoneyPurchasesAsync

Retrieves all real money purchases currently configured in the Economy service. Returns a list of `RealMoneyPurchaseDefinition` objects.

```cs
List<RealMoneyPurchaseDefinition> definitions = await Economy.Configuration.GetRealMoneyPurchasesAsync();
```

#### GetRealMoneyPurchaseAsync

Retrieves a single real money purchase currently configured in the Economy service. Returns a single `RealMoneyPurchaseDefinition` object.

```cs
string purchaseId = "REAL_MONEY_PURCHASE_ID"
RealMoneyPurchaseDefinition definition = await Economy.Configuration.GetRealMoneyPurchaseAsync(purchaseId);
```

#### RealMoneyPurchaseDefinition

A `RealMoneyPurchaseDefinition` object represents a real money purchase definition from your configuration. It is made up of a number of component objects as listed below:

The `RealMoneyPurchaseDefinition` has the following fields:
- `Id`: The purchase definition ID
- `Name`: The human readable name
- `Type`: The type of item as defined in the Economy dashboard (for all RealMoneyPurchaseDefinition objects this will be `MONEY_PURCHASE`)
- `CustomData`: Any custom data associated with this purchase definition, as a `Dictionary<string, object>`. See below for more details.
- `Created`: The date this purchase definition was created. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `Modified`: The date this purchase definition was modified. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `StoreIdentifiers` : The store identifiers for this purchase. It is a `StoreIdentifiers` object (see [here](./configuration.md#StoreIdentifiers)).
- `Rewards` : A list of the rewards associated with this purchase, as a `List<PurchaseItemQuantity>` (see [here](./configuration.md#PurchaseItemQuantity)).

#### StoreIdentifiers
A `StoreIdentifers` object contains both the Google and Apple store identifiers. These are set when creating a purchase in the Unity Dashboard in the "Store connection" step. They can be editied by clicking on your purchase in the Unity Dashboard and scrolling down to "Store connection".

### PurchaseItemQuantity
A `PurchaseItemQuantity` represents an amount of currency/inventory items associated with a purchase. Each one relates to a single currency/inventory item type (for example 4 swords, 10 gold etc.). It has the following fields:
- `Item`: An `EconomyReference` pointing to the item definition represented by this quantity.
- `Amount`: The amount of the item represented, as an integer.

### EconomyReference
An `EconomyReference` is a reference to another definition from within the purchase. It has a single method:
- `GetReferencedConfigurationItem()` which fetches the associated item. 

Getting the referenced item this way happens synchronously as it doesn't require a further network request, and **will get the referenced item as it was when the purchase was retrieved** (i.e. any changes to the definition between fetching the purchase and accessing the reference will not be reflected in the referenced item).

## Using CustomData

CustomData is used for currency, inventory item, and purchase configurations. This property allows the user to set custom data on an objects
definition. It is of type Dictionary<string, object>.

CustomData is set in the Unity Dashboard. Select a currency/inventory/purchase, navigate to custom data and add in the data in a JSON format.

For example, your game may have a color rarity system. You could then add custom data to your sword item using the following JSON:

```
{
    "rarity": "purple"
}
```