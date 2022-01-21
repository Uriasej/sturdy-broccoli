# Documentation for Cloud Save API
    <a name="documentation-for-api-endpoints"></a>
    ## Documentation for API Endpoints
    All URIs are relative to *https://cloud-save.services.api.unity.com*
    Class | Method | HTTP request | Description
    ------------ | ------------- | ------------- | -------------
    *DataApi* | [**DeleteItem**](Apis/DataApi.md#deleteitem) | **DELETE** /v1/data/projects/{projectId}/players/{playerId}/items/{key} | Delete Item
    *DataApi* | [**GetItems**](Apis/DataApi.md#getitems) | **GET** /v1/data/projects/{projectId}/players/{playerId}/items | Get Items
    *DataApi* | [**GetKeys**](Apis/DataApi.md#getkeys) | **GET** /v1/data/projects/{projectId}/players/{playerId}/keys | Get Keys
    *DataApi* | [**SetItem**](Apis/DataApi.md#setitem) | **POST** /v1/data/projects/{projectId}/players/{playerId}/items | Set Item
    *DataApi* | [**SetItemBatch**](Apis/DataApi.md#setitembatch) | **POST** /v1/data/projects/{projectId}/players/{playerId}/item-batch | Set Item Batch
    
    <a name="documentation-for-models"></a>
    ## Documentation for Models
         - [Models.AttemptedItem](Models/AttemptedItem.md)
         - [Models.BasicErrorResponse](Models/BasicErrorResponse.md)
         - [Models.BatchBasicErrorBody](Models/BatchBasicErrorBody.md)
         - [Models.BatchBasicErrorResponse](Models/BatchBasicErrorResponse.md)
         - [Models.BatchConflictErrorResponse](Models/BatchConflictErrorResponse.md)
         - [Models.BatchConflictErrorResponseData](Models/BatchConflictErrorResponseData.md)
         - [Models.BatchValidationErrorBody](Models/BatchValidationErrorBody.md)
         - [Models.BatchValidationErrorResponse](Models/BatchValidationErrorResponse.md)
         - [Models.ConflictErrorResponse](Models/ConflictErrorResponse.md)
         - [Models.ConflictErrorResponseData](Models/ConflictErrorResponseData.md)
         - [Models.DeleteItem400OneOf](Models/DeleteItem400OneOf.md)
         - [Models.GetItems400OneOf](Models/GetItems400OneOf.md)
         - [Models.GetItemsResponse](Models/GetItemsResponse.md)
         - [Models.GetItemsResponseLinks](Models/GetItemsResponseLinks.md)
         - [Models.GetKeys400OneOf](Models/GetKeys400OneOf.md)
         - [Models.GetKeysResponse](Models/GetKeysResponse.md)
         - [Models.Item](Models/Item.md)
         - [Models.KeyMetadata](Models/KeyMetadata.md)
         - [Models.ModifiedMetadata](Models/ModifiedMetadata.md)
         - [Models.SetItem400OneOf](Models/SetItem400OneOf.md)
         - [Models.SetItemBatch400OneOf](Models/SetItemBatch400OneOf.md)
         - [Models.SetItemBatchBody](Models/SetItemBatchBody.md)
         - [Models.SetItemBatchResponse](Models/SetItemBatchResponse.md)
         - [Models.SetItemBatchResponseResults](Models/SetItemBatchResponseResults.md)
         - [Models.SetItemBody](Models/SetItemBody.md)
         - [Models.SetItemResponse](Models/SetItemResponse.md)
         - [Models.ValidationErrorBody](Models/ValidationErrorBody.md)
         - [Models.ValidationErrorResponse](Models/ValidationErrorResponse.md)
        
<a name="documentation-for-authorization"></a>
## Documentation for Authorization
    <a name="JWT"></a>
    ### JWT
        - **Type**: HTTP basic authentication
    