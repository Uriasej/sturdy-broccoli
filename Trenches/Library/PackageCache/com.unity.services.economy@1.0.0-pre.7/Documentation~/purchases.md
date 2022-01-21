# Purchases

The methods in the `Purchases` namespace allow you to make purchases for the currently signed in user.

> **All the methods in this namespace can throw an `EconomyException` as described [here](./exceptions.md#EconomyException)**

## MakeVirtualPurchaseAsync

Makes a virtual purchase specified by ID. 

This method optionally takes a `MakeVirtualPurchaseOptions` object. This can be used to specify the `PlayersInventoryItems` IDs of the items in the players inventory that should be used towards the cost(s) of the purchase. If these are not supplied, the items used towards the cost(s) will be chosen automatically.

Returns a `MakeVirtualPurchaseResult`.

Example:

```cs 
string purchaseID = "BUY_A_SWORD";
MakeVirtualPurchaseResult purchaseResult = await Economy.Purchases.MakeVirtualPurchaseAsync(purchaseID);
```

Alternative Example using PlayersInventoryItem IDs:

```cs
string purchaseID = "BUY_A_SWORD";
Purchases.MakeVirtualPurchaseOptions options = new Purchases.MakeVirtualPurchaseOptions
{
    PlayersInventoryItemIds = new List<string> { "playersInventoryItemId1", "playersInventoryItemId2" }
};

MakeVirtualPurchaseResult purchaseResult = await Economy.Purchases.MakeVirtualPurchaseAsync(purchaseID, options);
```

### MakeVirtualPurchaseOptions

The options object for a `MakeVirtualPurchaseAsync` call. It has the following fields:
- `PlayersInventoryItemIds`: A list of strings. Defaults to null. The `PlayersInventoryItem` IDs of the items in the players inventory that you want to use towards the cost(s) of the purchase. 

### MakeVirtualPurchaseResult

This object is returned by a `MakeVirtualPurchaseAsync` call. It contains the following fields:
- `Costs`: A `Costs` object representing the costs that were spent in this purchase. This in turn has two fields:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies used to make this purchase. See [here](./purchases.md#CurrencyExchangeItem).
    - `Inventory`: A list of `InventoryExchangeItem` describing the items used as a cost in order to make this purchase. See [here](./purchases.md#InventoryExchangeItem).
- `Rewards`: A `Rewards` object representing the rewards given in exchange for this purchase. This also has two fields as above:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies rewarded as part of this purchase. See [here](./purchases.md#CurrencyExchangeItem).
    - `Inventory`: A list of `InventoryExchangeItem` describing the items rewarded as part of this purchase. See [here](./purchases.md#InventoryExchangeItem).

## RedeemAppleAppStorePurchaseAsync
Redeems a real money purchase by submitting a receipt from the Apple App Store. This is validated and if valid the rewards as defined in the configuration are applied to the player’s inventory and currency balances.

Takes a required `RedeemAppleAppStorePurchaseArgs` object. This is used to provide the purchase details. See [here](./purchases.md#RedeemAppleAppStorePurchaseArgs).

Example:

```cs
Purchases.RedeemAppleAppStorePurchaseArgs args = new Purchases.RedeemAppleAppStorePurchaseArgs("PURCHASE_ID", "RECEIPT_FROM_APP_STORE", 0, "USD");

RedeemAppleAppStorePurchaseResult purchaseResult = await Economy.Purchases.RedeemAppleAppStorePurchaseAsync(args);
```

### RedeemAppleAppStorePurchaseArgs
The arguments object for a `RedeemAppleAppStorePurchaseAsync` call. It has the following fields:
- `RealMoneyPurchaseId`: A string. The configuration ID of the purchase to make.
- `Receipt`: A string. The receipt data as returned from the Apple App Store.
- `LocalCost`: An int. The cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199.
- `LocalCurrency`: A string. ISO-4217 code of the currency used in the purchase.

### RedeemAppleAppStorePurchaseResult
This object is returned by a `RedeemAppleAppStorePurchaseAsync` call. It contains the following fields:
- `Verification`: The receipt verification details from the validation service.
    - `Status`: Status of the receipt verification. This will be one of:
        - `VALID`: The purchase was valid. 
        - `VALID_NOT_REDEEMED`: The purchase was valid but seen before, but had not yet been redeemed. 
        - `INVALID_ALREADY_REDEEMED`: The purchase has already been redeemed. 
        - `INVALID_VERIFICATION_FAILED`: The receipt verification service returned that the receipt data was not valid. 
        - `INVALID_ANOTHER_PLAYER`: The receipt has previously been used by a different player and validated. 
        - `INVALID_CONFIGURATION`: The service configuration is invalid, further information in the details section of the response. 
        - `INVALID_PRODUCT_ID_MISMATCH`: The purchase configuration store product identifier does not match the one in the receipt.
    - `Store`: Details from the receipt validation service. This has three fields:
        - `Code`: The status code sent back from the Apple App Store verification service.
        - `Message`: A textual description of the returned status code.
        - `Receipt`: The purchase receipt data.
- `Rewards`: A `Rewards` object representing the rewards given in exchange for this purchase. This has two fields:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies rewarded as part of this purchase. See [here](./purchases.md#CurrencyExchangeItem).
    - `Inventory`: A list of `InventoryExchangeItem` describing the items rewarded as part of this purchase. See [here](./purchases.md#InventoryExchangeItem).
    
### EconomyAppleAppStorePurchaseFailedException

`RedeemAppleAppStorePurchaseAsync` may throw an exception of type `EconomyAppleAppStorePurchaseFailedException`. This inherits from `EconomyException` and contains one additional field called `Data`.

The `Data` field is of type `RedeemAppleAppStorePurchaseResult` (described above).

Please note, this `Data` field is different from the `Data` field in the base `Exception` class.

## RedeemGooglePlayPurchaseAsync
Redeems a real money purchase by submitting a receipt from the Google Play Store. This is validated and if valid the rewards as defined in the configuration are applied to the player’s inventory and currency balances.

Takes a required `RedeemGooglePlayStorePurchaseArgs` object. This is used to provide the purchase details. See [here](./purchases.md#RedeemGooglePlayStorePurchaseArgs).

Example:

```cs
Purchases.RedeemGooglePlayStorePurchaseArgs args = new Purchases.RedeemGooglePlayStorePurchaseArgs("PURCHASE_ID", "PURCHASE_DATA", "PURCHASE_DATA_SIGNATURE", 0, "USD");

RedeemGooglePlayPurchaseResult purchaseResult = await Economy.Purchases.RedeemGooglePlayPurchaseAsync(args);
```

### RedeemGooglePlayStorePurchaseArgs
The arguments object for a `RedeemGooglePlayPurchaseAsync` call. It has the following fields:
- `RealMoneyPurchaseId`: A string. The configuration ID of the purchase to make.
- `PurchaseData`: A string. A JSON encoded string returned from a successful in app billing purchase.
- `PurchaseDataSignature`: A string. A signature of the PurchaseData returned from a successful in app billing purchase.
- `LocalCost`: An int. The cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199.
- `LocalCurrency`: A string. ISO-4217 code of the currency used in the purchase.

### RedeemGooglePlayPurchaseResult
This object is returned by a `RedeemGooglePlayPurchaseAsync` call. It contains the following fields:
- `Verification`: The receipt verification details from the validation service.
    - `Status`: Status of the receipt verification. This will be one of:
        - `VALID`: The purchase was valid. 
        - `VALID_NOT_REDEEMED`: The purchase was valid but seen before, but had not yet been redeemed. 
        - `INVALID_ALREADY_REDEEMED`: The purchase has already been redeemed. 
        - `INVALID_VERIFICATION_FAILED`: The receipt verification service returned that the receipt data was not valid. 
        - `INVALID_ANOTHER_PLAYER`: The receipt has previously been used by a different player and validated. 
        - `INVALID_CONFIGURATION`: The service configuration is invalid, further information in the details section of the response. 
        - `INVALID_PRODUCT_ID_MISMATCH`: The purchase configuration store product identifier does not match the one in the receipt.
    - `Store`: Details from the receipt validation service. It has one field:
        - `Receipt`: The purchase receipt data.

- `Rewards`: A `Rewards` object representing the rewards given in exchange for this purchase. This has two fields:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies rewarded as part of this purchase. See [here](./purchases.md#CurrencyExchangeItem).
    - `Inventory`: A list of `InventoryExchangeItem` describing the items rewarded as part of this purchase. See [here](./purchases.md#InventoryExchangeItem).
    
### EconomyGooglePlayStorePurchaseFailedException

`RedeemGooglePlayPurchaseAsync` may throw an exception of type `EconomyGooglePlayStorePurchaseFailedException`. This inherits from `EconomyException` and contains one additional field called `Data`.

The `Data` field is of type `RedeemGooglePlayPurchaseResult` (described above).

Please note, this `Data` field is different from the `Data` field in the base `Exception` class.

## CurrencyExchangeItem

This object represents a currency that was part of a purchase. It has two fields:

- `Id`: The ID of the currency.
- `Amount`: The amount of this currency used in the purchase.

## InventoryExchangeItem

This object represents a inventory item that was part of a purchase. It has three fields:

- `Id`: The ID of the currency.
- `Amount`: The amount of this inventory item used/rewarded in the purchase.
- `InstanceIds`: A list of instance IDs that were used/rewarded in the purchase.