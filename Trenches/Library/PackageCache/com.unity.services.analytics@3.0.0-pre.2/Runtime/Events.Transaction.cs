using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Services.Analytics.Platform;

namespace Unity.Services.Analytics
{
    public static partial class Events
    {
        public enum TransactionServer
        {
            APPLE,
            AMAZON,
            GOOGLE
        }
    
        public enum TransactionType
        {
            INVALID = 0,
            SALE,
            PURCHASE,
            TRADE
        }
    
        public struct Item
        {
            public string itemName;
            public string itemType;
            public Int64 itemAmount;
        }
    
        public struct VirtualCurrency
        {
            public string virtualCurrencyName;
            public string virtualCurrencyType;
            public Int64 virtualCurrencyAmount;
        }
    
        public struct RealCurrency
        {
            public string realCurrencyType;
            public Int64 realCurrencyAmount;
        }
    
        public struct Product
        {
            //Optional
            public RealCurrency? realCurrency;
            public List<VirtualCurrency> virtualCurrencies;
            public List<Item> items;
        }
    
        public struct TransactionParameters
        {
            [Obsolete]
            public bool? isInitiator;
            /// <summary>
            /// Optional.
            /// If this is left null or empty, the machine's locale will be used
            /// </summary>
            public string paymentCountry;
            public string productID;
            public Int64? revenueValidated;
            public string transactionID;
            public string transactionReceipt;
            public string transactionReceiptSignature;
            public TransactionServer? transactionServer;
            public string transactorID;
            public string storeItemSkuID;
            public string storeItemID;
            public string storeID;
            public string storeSourceID;
            //Required
            public string transactionName;
            public TransactionType transactionType;
            public Product productsReceived;
            public Product productsSpent;
        }
        
        /// <summary>
        /// Record a Transaction event.
        /// </summary>
        /// <param name="transactionParameters">(Required) Helper object to handle parameters.</param>
        public static void Transaction(TransactionParameters transactionParameters)
        {
            Debug.Assert(!string.IsNullOrEmpty(transactionParameters.transactionName), "Required to have a value for transactionName");
            Debug.Assert(!transactionParameters.transactionType.Equals(TransactionType.INVALID), "Required to have a value for transactionType");

            // If The paymentCountry is not provided we will generate it.
            
            if (string.IsNullOrEmpty(transactionParameters.paymentCountry))
            {
                transactionParameters.paymentCountry = Analytics.Internal.Platform.UserCountry.Name();
            }

            Data.Generator.Transaction(ref dataBuffer, DateTime.UtcNow, s_CommonParams, "com.unity.services.analytics.events.transaction", transactionParameters);
        }
    }
}