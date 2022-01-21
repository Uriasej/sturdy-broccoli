# Documentation for Economy API
    <a name="documentation-for-api-endpoints"></a>
    ## Documentation for API Endpoints
    All URIs are relative to *https://economy.services.api.unity.com*
    Class | Method | HTTP request | Description
    ------------ | ------------- | ------------- | -------------
    *CurrenciesApi* | [**DecrementPlayerCurrencyBalance**](Apis/CurrenciesApi.md#decrementplayercurrencybalance) | **POST** /v2/projects/{projectId}/players/{playerId}/currencies/{currencyId}/decrement | Decrement Currency Balance
    *CurrenciesApi* | [**GetPlayerCurrencies**](Apis/CurrenciesApi.md#getplayercurrencies) | **GET** /v2/projects/{projectId}/players/{playerId}/currencies | Player Currency Balances
    *CurrenciesApi* | [**IncrementPlayerCurrencyBalance**](Apis/CurrenciesApi.md#incrementplayercurrencybalance) | **POST** /v2/projects/{projectId}/players/{playerId}/currencies/{currencyId}/increment | Increment Currency Balance
    *CurrenciesApi* | [**SetPlayerCurrencyBalance**](Apis/CurrenciesApi.md#setplayercurrencybalance) | **PUT** /v2/projects/{projectId}/players/{playerId}/currencies/{currencyId} | Set Currency Balance
    *InventoryApi* | [**AddInventoryItem**](Apis/InventoryApi.md#addinventoryitem) | **POST** /v2/projects/{projectId}/players/{playerId}/inventory | Add Inventory Item
    *InventoryApi* | [**DeleteInventoryItem**](Apis/InventoryApi.md#deleteinventoryitem) | **DELETE** /v2/projects/{projectId}/players/{playerId}/inventory/{playersInventoryItemId} | Delete Inventory Item
    *InventoryApi* | [**GetPlayerInventory**](Apis/InventoryApi.md#getplayerinventory) | **GET** /v2/projects/{projectId}/players/{playerId}/inventory | List Player Inventory
    *InventoryApi* | [**UpdateInventoryItem**](Apis/InventoryApi.md#updateinventoryitem) | **PUT** /v2/projects/{projectId}/players/{playerId}/inventory/{playersInventoryItemId} | Player Inventory Item
    *PurchasesApi* | [**MakeVirtualPurchase**](Apis/PurchasesApi.md#makevirtualpurchase) | **POST** /v2/projects/{projectId}/players/{playerId}/purchases/virtual | Make purchase
    *PurchasesApi* | [**RedeemAppleAppStorePurchase**](Apis/PurchasesApi.md#redeemappleappstorepurchase) | **POST** /v2/projects/{projectId}/players/{playerId}/purchases/appleappstore | Redeem Apple App Store Purchase
    *PurchasesApi* | [**RedeemGooglePlayPurchase**](Apis/PurchasesApi.md#redeemgoogleplaypurchase) | **POST** /v2/projects/{projectId}/players/{playerId}/purchases/googleplaystore | Redeem Google Play Purchase
    
    <a name="documentation-for-models"></a>
    ## Documentation for Models
         - [Models.AddInventoryItem400OneOf](Models/AddInventoryItem400OneOf.md)
         - [Models.AddInventoryRequest](Models/AddInventoryRequest.md)
         - [Models.BasicErrorResponse](Models/BasicErrorResponse.md)
         - [Models.CurrencyBalanceRequest](Models/CurrencyBalanceRequest.md)
         - [Models.CurrencyBalanceResponse](Models/CurrencyBalanceResponse.md)
         - [Models.CurrencyExchangeItem](Models/CurrencyExchangeItem.md)
         - [Models.CurrencyModifyBalanceRequest](Models/CurrencyModifyBalanceRequest.md)
         - [Models.DecrementPlayerCurrencyBalance400OneOf](Models/DecrementPlayerCurrencyBalance400OneOf.md)
         - [Models.DeleteInventoryItem400OneOf](Models/DeleteInventoryItem400OneOf.md)
         - [Models.ErrorResponseConflictCurrencyBalance](Models/ErrorResponseConflictCurrencyBalance.md)
         - [Models.ErrorResponseConflictCurrencyBalanceData](Models/ErrorResponseConflictCurrencyBalanceData.md)
         - [Models.ErrorResponseConflictInventory](Models/ErrorResponseConflictInventory.md)
         - [Models.ErrorResponseConflictInventoryDelete](Models/ErrorResponseConflictInventoryDelete.md)
         - [Models.ErrorResponseConflictInventoryDeleteData](Models/ErrorResponseConflictInventoryDeleteData.md)
         - [Models.ErrorResponseConflictInventoryUpdate](Models/ErrorResponseConflictInventoryUpdate.md)
         - [Models.ErrorResponseConflictInventoryUpdateData](Models/ErrorResponseConflictInventoryUpdateData.md)
         - [Models.ErrorResponsePurchaseAppleappstoreFailed](Models/ErrorResponsePurchaseAppleappstoreFailed.md)
         - [Models.ErrorResponsePurchaseGoogleplaystoreFailed](Models/ErrorResponsePurchaseGoogleplaystoreFailed.md)
         - [Models.IncrementPlayerCurrencyBalance400OneOf](Models/IncrementPlayerCurrencyBalance400OneOf.md)
         - [Models.InventoryDeleteRequest](Models/InventoryDeleteRequest.md)
         - [Models.InventoryExchangeItem](Models/InventoryExchangeItem.md)
         - [Models.InventoryRequestUpdate](Models/InventoryRequestUpdate.md)
         - [Models.InventoryResponse](Models/InventoryResponse.md)
         - [Models.MakeVirtualPurchase400OneOf](Models/MakeVirtualPurchase400OneOf.md)
         - [Models.ModifiedMetadata](Models/ModifiedMetadata.md)
         - [Models.PlayerCurrencyBalanceResponse](Models/PlayerCurrencyBalanceResponse.md)
         - [Models.PlayerCurrencyBalanceResponseLinks](Models/PlayerCurrencyBalanceResponseLinks.md)
         - [Models.PlayerInventoryResponse](Models/PlayerInventoryResponse.md)
         - [Models.PlayerPurchaseAppleappstoreRequest](Models/PlayerPurchaseAppleappstoreRequest.md)
         - [Models.PlayerPurchaseAppleappstoreResponse](Models/PlayerPurchaseAppleappstoreResponse.md)
         - [Models.PlayerPurchaseAppleappstoreResponseVerification](Models/PlayerPurchaseAppleappstoreResponseVerification.md)
         - [Models.PlayerPurchaseAppleappstoreResponseVerificationStore](Models/PlayerPurchaseAppleappstoreResponseVerificationStore.md)
         - [Models.PlayerPurchaseGoogleplaystoreRequest](Models/PlayerPurchaseGoogleplaystoreRequest.md)
         - [Models.PlayerPurchaseGoogleplaystoreResponse](Models/PlayerPurchaseGoogleplaystoreResponse.md)
         - [Models.PlayerPurchaseGoogleplaystoreResponseVerification](Models/PlayerPurchaseGoogleplaystoreResponseVerification.md)
         - [Models.PlayerPurchaseGoogleplaystoreResponseVerificationStore](Models/PlayerPurchaseGoogleplaystoreResponseVerificationStore.md)
         - [Models.PlayerPurchaseVirtualRequest](Models/PlayerPurchaseVirtualRequest.md)
         - [Models.PlayerPurchaseVirtualResponse](Models/PlayerPurchaseVirtualResponse.md)
         - [Models.PlayerPurchaseVirtualResponseCosts](Models/PlayerPurchaseVirtualResponseCosts.md)
         - [Models.PlayerPurchaseVirtualResponseRewards](Models/PlayerPurchaseVirtualResponseRewards.md)
         - [Models.RedeemAppleAppStorePurchase400OneOf](Models/RedeemAppleAppStorePurchase400OneOf.md)
         - [Models.RedeemAppleAppStorePurchase422OneOf](Models/RedeemAppleAppStorePurchase422OneOf.md)
         - [Models.RedeemGooglePlayPurchase400OneOf](Models/RedeemGooglePlayPurchase400OneOf.md)
         - [Models.RedeemGooglePlayPurchase422OneOf](Models/RedeemGooglePlayPurchase422OneOf.md)
         - [Models.SetPlayerCurrencyBalance400OneOf](Models/SetPlayerCurrencyBalance400OneOf.md)
         - [Models.UpdateInventoryItem400OneOf](Models/UpdateInventoryItem400OneOf.md)
         - [Models.ValidationErrorBody](Models/ValidationErrorBody.md)
         - [Models.ValidationErrorResponse](Models/ValidationErrorResponse.md)
        
<a name="documentation-for-authorization"></a>
## Documentation for Authorization
    <a name="JWT"></a>
    ### JWT
        - **Type**: HTTP basic authentication
    