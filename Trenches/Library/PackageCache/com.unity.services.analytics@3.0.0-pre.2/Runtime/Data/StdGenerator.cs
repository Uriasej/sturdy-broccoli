using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unity.Services.Analytics.Tests")]

namespace Unity.Services.Analytics.Data
{
    /// <summary>
    /// DataGenerator is used to push event data into the internal buffer.
    /// The reason its split like this is so we can test the output from
    /// The DataGenerator + InternalBuffer. If this output is validated we
    /// can be pretty confident we are always producing valid JSON for the
    /// backend.
    /// </summary>
    static class Generator
    {
        internal static void SdkStartup(ref Analytics.Internal.IBuffer buf, DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier)
        {
            // Schema: http://go/UA2_SDKStart_v1

            buf.PushStartEvent("sdkStart", datetime, 1);
            buf.PushString(SdkVersion.SDK_VERSION, "sdkVersion");

            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);
            buf.PushString("com.unity.services.analytics", "sdkName"); // Schema: Required
            
            buf.PushEndEvent();
        }

        internal static void GameRunning(ref Analytics.Internal.IBuffer buf, DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier)
        {
            // Schema: http://go/UA2_GameRunning_v1
            
            buf.PushStartEvent("gameRunning", datetime, 1);
            
            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);
            
            buf.PushEndEvent();
        }

        internal static void NewPlayer(ref Analytics.Internal.IBuffer buf, DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string deviceModel)
        {
            // Schema: http://go/UA2_NewPlayer_v1
            
            buf.PushStartEvent("newPlayer", datetime, 1);
            
            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);
            // We aren't sending deviceBrand at the moment as deviceModel is sufficient.
            // UA1 did not send deviceBrand either. See JIRA-196 for more info.
            buf.PushString(deviceModel, "deviceModel"); // Schema: Optional
            
            buf.PushEndEvent();
        }

        internal static void GameStarted(
            ref Analytics.Internal.IBuffer buf,
            DateTime datetime,
            StdCommonParams commonParams,
            string callingMethodIdentifier,
            string idLocalProject,
            string osVersion,
            bool isTiny,
            bool debugDevice,
            string userLocale)
        {
            buf.PushStartEvent("gameStarted", datetime, 1);
            
            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);
            
            // Schema: Required
            buf.PushString(userLocale, "userLocale");
            
            // Schema: Optional
            if (!String.IsNullOrEmpty(idLocalProject))
            {
                buf.PushString(idLocalProject, "idLocalProject");
            }
            buf.PushString(osVersion, "osVersion");
            buf.PushBool(isTiny, "isTiny");
            buf.PushBool(debugDevice, "debugDevice");
            
            buf.PushEndEvent();
        }
        
        // Keep the enum values in Caps!
        // We stringify the values.
        // These values aren't listed as an enum the Schema, but they are listed
        // values here http://go/UA2_Spreadsheet
        internal enum SessionEndState
        {
            PAUSED,
            KILLEDINBACKGROUND,
            KILLEDINFOREGROUND,
            QUIT,
        }
        
        internal static void GameEnded(ref Analytics.Internal.IBuffer buf, DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, SessionEndState quitState)
        {
            // Schema: http://go/UA2_GameEnded_v1
            
            buf.PushStartEvent("gameEnded", datetime ,1);
            
            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);
            
	        buf.PushString(quitState.ToString(), "sessionEndState"); // Schema: Required
            
            buf.PushEndEvent();
        }

        internal static void AdImpression(
            ref Analytics.Internal.IBuffer buf,
            DateTime datetime,
            StdCommonParams commonParams,
            string callingMethodIdentifier,
            string adCompletionStatus,
            string adProvider,
            string placementID,
            string placementName,
            string placementType,
            double? adEcpmUsd,
            string sdkVersion,
            string adImpressionID,
            string adStoreDestinationID,
            string adMediaType,
            Int64? adTimeWatchedMs,
            Int64? adTimeCloseButtonShownMs,
            Int64? adLengthMs,
            bool? adHasClicked,
            string adSource,
            string adStatusCallback)
        {
            // Schema: http://go/UA2_AdImpression_v1

            buf.PushStartEvent("adImpression", datetime, 1);

            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);

            // Schema: Required

            buf.PushString(adCompletionStatus, "adCompletionStatus");
            buf.PushString(adProvider, "adProvider");
            buf.PushString(placementID, "placementId");
            buf.PushString(placementName, "placementName");

            // Schema: Optional

            if (adEcpmUsd is double adEcpmUsdValue)
            {
                buf.PushDouble(adEcpmUsdValue, "adEcpmUsd");
            }

            if (!string.IsNullOrEmpty(placementType))
            {
                buf.PushString(placementType, "placementType");
            }

            if (!string.IsNullOrEmpty(sdkVersion))
            {
                buf.PushString(sdkVersion, "adSdkVersion");
            }

            if (!string.IsNullOrEmpty(adImpressionID))
            {
                buf.PushString(adImpressionID, "adImpressionID");
            }

            if (!string.IsNullOrEmpty(adStoreDestinationID))
            {
                buf.PushString(adStoreDestinationID, "adStoreDestinationID");
            }

            if (!string.IsNullOrEmpty(adMediaType))
            {
                buf.PushString(adMediaType, "adMediaType");
            }

            if (adTimeWatchedMs is Int64 adTimeWatchedMsValue)
            {
                buf.PushInt64(adTimeWatchedMsValue, "adTimeWatchedMs");
            }

            if (adTimeCloseButtonShownMs is Int64 adTimeCloseButtonShownMsValue)
            {
                buf.PushInt64(adTimeCloseButtonShownMsValue, "adTimeCloseButtonShownMs");
            }

            if (adLengthMs is Int64 adLengthMsValue)
            {
                buf.PushInt64(adLengthMsValue, "adLengthMs");
            }

            if (adHasClicked is bool adHasClickedValue)
            {
                buf.PushBool(adHasClickedValue, "adHasClicked");
            }

            if (!string.IsNullOrEmpty(adSource))
            {
                buf.PushString(adSource, "adSource");
            }

            if (!string.IsNullOrEmpty(adStatusCallback))
            {
                buf.PushString(adStatusCallback, "adStatusCallback");
            }

            buf.PushEndEvent();
        }

        internal static void Transaction(
            ref Analytics.Internal.IBuffer buf,
            DateTime datetime,
            StdCommonParams commonParams,
            string callingMethodIdentifier,
            Events.TransactionParameters transactionParameters
            )
        {
            // Schema: http://go/UA2_Transaction_v1
            
            buf.PushStartEvent("transaction", datetime, 1);
            
            // Event Params 
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);

            if (!string.IsNullOrEmpty(SdkVersion.SDK_VERSION))
            {
                buf.PushString(SdkVersion.SDK_VERSION, "sdkVersion");
            }

            if (!string.IsNullOrEmpty(transactionParameters.paymentCountry))
            {
                buf.PushString(transactionParameters.paymentCountry, "paymentCountry");
            }

            if (!string.IsNullOrEmpty(transactionParameters.productID))
            {
                buf.PushString(transactionParameters.productID, "productID");
            }

            if (transactionParameters.revenueValidated.HasValue)
            {
                buf.PushInt64(transactionParameters.revenueValidated.Value, "revenueValidated");
            }

            if (!string.IsNullOrEmpty(transactionParameters.transactionID))
            {
                buf.PushString(transactionParameters.transactionID, "transactionID");
            }

            if (!string.IsNullOrEmpty(transactionParameters.transactionReceipt))
            {
                buf.PushString(transactionParameters.transactionReceipt, "transactionReceipt");
            }

            if (!string.IsNullOrEmpty(transactionParameters.transactionReceiptSignature))
            {
                buf.PushString(transactionParameters.transactionReceiptSignature, "transactionReceiptSignature");
            }

            if (!string.IsNullOrEmpty(transactionParameters.transactionServer.ToString()))
            {
                buf.PushString(transactionParameters.transactionServer.ToString(), "transactionServer");
            }

            if (!string.IsNullOrEmpty(transactionParameters.transactorID))
            {
                buf.PushString(transactionParameters.transactorID, "transactorID");
            }

            if (!string.IsNullOrEmpty(transactionParameters.storeItemSkuID))
            {
                buf.PushString(transactionParameters.storeItemSkuID, "storeItemSkuID");
            }

            if (!string.IsNullOrEmpty(transactionParameters.storeItemID))
            {
                buf.PushString(transactionParameters.storeItemID, "storeItemID");
            }

            if (!string.IsNullOrEmpty(transactionParameters.storeID))
            {
                buf.PushString(transactionParameters.storeID, "storeID");
            }

            if (!string.IsNullOrEmpty(transactionParameters.storeSourceID))
            {
                buf.PushString(transactionParameters.storeSourceID, "storeSourceID");
            }

            // Required
            buf.PushString(transactionParameters.transactionName, "transactionName");
            buf.PushString(transactionParameters.transactionType.ToString(), "transactionType");
            SetProduct(ref buf, "productsReceived", transactionParameters.productsReceived);
            SetProduct(ref buf, "productsSpent", transactionParameters.productsSpent);

            buf.PushEndEvent();
        }

        internal static void ClientDevice(
            ref Analytics.Internal.IBuffer buf,
            DateTime datetime,
            StdCommonParams commonParams,
            string callingMethodIdentifier,
            string cpuType,
            string gpuType,
            Int64 cpuCores,
            Int64 ramTotal,
            Int64 screenWidth,
            Int64 screenHeight,
            Int64 screenDPI)
        {
            buf.PushStartEvent("clientDevice", datetime, 1);
            
            commonParams.SerializeCommonEventParams(ref buf, callingMethodIdentifier);
            
            // Schema: Optional
            buf.PushString(cpuType, "cpuType");
            buf.PushString(gpuType, "gpuType");
            buf.PushInt64(cpuCores, "cpuCores");
            buf.PushInt64(ramTotal, "ramTotal");
            buf.PushInt64(screenWidth, "screenWidth");
            buf.PushInt64(screenHeight, "screenHeight");
            buf.PushInt64(screenDPI, "screenResolution");

            buf.PushEndEvent();
        }

        static void SetProduct(ref Analytics.Internal.IBuffer buf, string productName, Events.Product product)
        {
            buf.PushObjectStart(productName);

            if (product.realCurrency.HasValue)
            {
                buf.PushObjectStart("realCurrency");
                buf.PushString(product.realCurrency.Value.realCurrencyType, "realCurrencyType");
                buf.PushInt64(product.realCurrency.Value.realCurrencyAmount, "realCurrencyAmount");
                buf.PushObjectEnd();
            }

            if (product.virtualCurrencies != null && product.virtualCurrencies.Count != 0)
            {
                buf.PushArrayStart("virtualCurrencies");
                foreach (Events.VirtualCurrency virtualCurrency in product.virtualCurrencies)
                {
                    buf.PushObjectStart();
                    buf.PushObjectStart("virtualCurrency");
                    buf.PushString(virtualCurrency.virtualCurrencyName, "virtualCurrencyName");
                    buf.PushString(virtualCurrency.virtualCurrencyType, "virtualCurrencyType");
                    buf.PushInt64(virtualCurrency.virtualCurrencyAmount, "virtualCurrencyAmount");
                    buf.PushObjectEnd();
                    buf.PushObjectEnd();
                }
                buf.PushArrayEnd();
            }

            if (product.items != null && product.items.Count != 0)
            {
                buf.PushArrayStart("items");
                foreach (Events.Item item in product.items)
                {
                    buf.PushObjectStart();
                    buf.PushObjectStart("item");
                    buf.PushString(item.itemName, "itemName");
                    buf.PushString(item.itemType, "itemType");
                    buf.PushInt64(item.itemAmount, "itemAmount");
                    buf.PushObjectEnd();
                    buf.PushObjectEnd();
                }
                buf.PushArrayEnd();
            }

            buf.PushObjectEnd();
        }
    }   
}
