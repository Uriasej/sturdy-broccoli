using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Economy.Internal;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Internal.Purchases;
using Unity.Services.Economy.Exceptions;
using Unity.Services.Economy.Model;
using UnityEngine;
using InventoryExchangeItem = Unity.Services.Economy.Model.InventoryExchangeItem;

namespace Unity.Services.Economy
{
    /// <summary>
    /// The Purchases methods allow you to make virtual and real world purchases.
    /// </summary>
    public class Purchases
    {
        readonly IPurchasesApiClient m_PurchasesApiClient;
        readonly IEconomyAuthentication m_EconomyAuthentication;

        internal Purchases(IPurchasesApiClient purchasesApiClient, IEconomyAuthentication economyAuthentication)
        {
            m_PurchasesApiClient = purchasesApiClient;
            m_EconomyAuthentication = economyAuthentication;
        }

        /// <summary>
        /// Options for a MakeVirtualPurchaseAsync call.
        /// </summary>
        public class MakeVirtualPurchaseOptions
        {
            public List<string> PlayersInventoryItemIds;
        }

        /// <summary>
        /// Makes the specified virtual purchase using the items in the players inventory.
        ///
        /// Takes a virtualPurchaseId. This is the ID of the purchase to make.
        /// Takes an optional list of instanceIds. These are the `PlayersInventoryItems` IDs of the items in the players inventory that should be used towards the cost(s) of the purchase. If these are not supplied, the items
        /// used towards the cost(s) will be chosen automatically.
        /// 
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="virtualPurchaseId">Purchase ID of the purchase to be made</param>
        /// <param name="options">(Optional) Use to set a list of instance IDs to use towards the cost(s) of the purchase</param>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<MakeVirtualPurchaseResult> MakeVirtualPurchaseAsync(string virtualPurchaseId, MakeVirtualPurchaseOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();
            
            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();

            MakeVirtualPurchaseRequest request = new MakeVirtualPurchaseRequest(
                Application.cloudProjectId, 
                m_EconomyAuthentication.GetPlayerId(),
                new PlayerPurchaseVirtualRequest(virtualPurchaseId, options?.PlayersInventoryItemIds));

            try
            {
                Response<PlayerPurchaseVirtualResponse> response = await m_PurchasesApiClient.MakeVirtualPurchaseAsync(request);

                string rawResponse = JsonConvert.SerializeObject(response.Result);
                MakeVirtualPurchaseResult convertedResponse = JsonConvert.DeserializeObject<MakeVirtualPurchaseResult>(rawResponse);

                TriggerEventsForVirtualPurchase(convertedResponse);

                return convertedResponse;
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
        /// Arguments for a RedeemAppleAppStorePurchaseAsync call.
        /// </summary>
        public class RedeemAppleAppStorePurchaseArgs
        {
            /// <summary>
            /// Takes a realMoneyPurchaseId. This is the configuration ID of the purchase to make.
            /// Takes a receipt. This is the receipt data as returned from the Apple App Store.
            /// Takes a localCost. This is the cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199
            /// Takes a localCurrency. ISO-4217 code of the currency used in the purchase.
            /// </summary>
            /// <param name="realMoneyPurchaseId">Configuration ID of the purchase to be made</param>
            /// <param name="receipt">Receipt data as returned from the Apple App Store</param>
            /// <param name="localCost">Cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199</param>
            /// <param name="localCurrency">ISO-4217 code of the currency used in the purchase</param>
            public RedeemAppleAppStorePurchaseArgs(string realMoneyPurchaseId, string receipt, int localCost, string localCurrency)
            {
                RealMoneyPurchaseId = realMoneyPurchaseId;
                Receipt = receipt;
                LocalCost = localCost;
                LocalCurrency = localCurrency;
            }

            /// <summary>
            /// Configuration ID of the purchase to make.
            /// </summary>
            public string RealMoneyPurchaseId { get; set; }
            
            /// <summary>
            /// The receipt data as returned from the Apple App Store.
            /// </summary>
            public string Receipt { get; set; }

            /// <summary>
            /// The cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199
            /// </summary>            
            public int LocalCost { get; set; }
            
            /// <summary>
            /// ISO-4217 code of the currency used in the purchase.
            /// </summary>    
            public string LocalCurrency { get; set; }
        }

        /// <summary>
        /// Redeems the specified Apple App Store purchase.
        /// 
        /// Throws a EconomyException with a reason code and explanation
        /// </summary>
        /// <param name="args">The Apple App Store purchase details for the request</param>
        /// <exception cref="EconomyAppleAppStorePurchaseFailedException">Thrown if the purchase fails in one of the following ways:
        /// invalid receipt, purchase already redeemed, product ID mismatch, product ID not defined, currency max would be exceeded.</exception>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<RedeemAppleAppStorePurchaseResult> RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args)
        {
            if(args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            m_EconomyAuthentication.CheckSignedIn();
            
            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();
            
            RedeemAppleAppStorePurchaseRequest request = new RedeemAppleAppStorePurchaseRequest(
                Application.cloudProjectId, 
                m_EconomyAuthentication.GetPlayerId(),
                new PlayerPurchaseAppleappstoreRequest(args.RealMoneyPurchaseId, args.Receipt, args.LocalCost, args.LocalCurrency));

            try
            {
                Response<PlayerPurchaseAppleappstoreResponse> response = await m_PurchasesApiClient.RedeemAppleAppStorePurchaseAsync(request);

                RedeemAppleAppStorePurchaseResult convertedResponse = ConvertBackendApplePurchaseModelToSDKModel(response.Result);

                TriggerEventsForApplePurchase(convertedResponse);

                return convertedResponse;
            }
            catch (HttpException<ErrorResponsePurchaseAppleappstoreFailed> e)
            {
                throw EconomyAPIErrorHandler.HandleAppleAppStoreFailedExceptions(e);
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
        /// Arguments for a RedeemGooglePlayStorePurchaseAsync call.
        /// </summary>
        public class RedeemGooglePlayStorePurchaseArgs
        {
            /// <summary>
            /// Takes a realMoneyPurchaseId. This is the configuration ID of the purchase to make.
            /// Takes a purchaseData. A JSON encoded string returned from a successful in app billing purchase.
            /// Takes a purchaseDataSignature. A signature of the PurchaseData returned from a successful in app billing purchase.
            /// Takes a localCost. This is the cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199
            /// Takes a localCurrency. ISO-4217 code of the currency used in the purchase. 
            /// </summary>
            /// <param name="realMoneyPurchaseId">Configuration ID of the purchase to be made</param>
            /// <param name="purchaseData"></param>
            /// <param name="purchaseDataSignature"></param>
            /// <param name="localCost">Cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199</param>
            /// <param name="localCurrency">ISO-4217 code of the currency used in the purchase</param>
            public RedeemGooglePlayStorePurchaseArgs(string realMoneyPurchaseId, string purchaseData, string purchaseDataSignature, int localCost, string localCurrency)
            {
                RealMoneyPurchaseId = realMoneyPurchaseId;
                PurchaseData = purchaseData;
                PurchaseDataSignature = purchaseDataSignature;
                LocalCost = localCost;
                LocalCurrency = localCurrency;
            }

            /// <summary>
            /// Configuration ID of the purchase to make.
            /// </summary>
            public string RealMoneyPurchaseId { get; set; }

            /// <summary>
            /// A JSON encoded string returned from a successful in app billing purchase.
            /// </summary>
            public string PurchaseData { get; set; }
            
            /// <summary>
            /// A signature of the PurchaseData returned from a successful in app billing purchase.
            /// </summary>
            public string PurchaseDataSignature { get; set; }

            /// <summary>
            /// The cost of the purchase as an integer in the minor currency format, e.g. $1.99 USD would be 199
            /// </summary>            
            public int LocalCost { get; set; }

            /// <summary>
            /// ISO-4217 code of the currency used in the purchase.
            /// </summary>    
            public string LocalCurrency { get; set; }
        }

        /// <summary>
        /// Redeems the specified Google Play Store Store purchase.
        /// 
        /// Throws a EconomyException with a reason code and explanation
        /// </summary>
        /// <param name="args">The Google Play Store purchase details for the request.</param>
        /// <exception cref="EconomyGooglePlayStorePurchaseFailedException">Thrown if the purchase fails in one of the following ways:
        /// invalid purchase data, invalid purchase data signature, purchase already redeemed, product ID mismatch,
        /// product ID not defined, currency max would be exceeded.</exception>
        public async Task<RedeemGooglePlayPurchaseResult> RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args)
        {
            if(args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            
            m_EconomyAuthentication.CheckSignedIn();

            m_EconomyAuthentication.SetAuthenticationTokenForEconomyApi();

            RedeemGooglePlayPurchaseRequest request = new RedeemGooglePlayPurchaseRequest(
                Application.cloudProjectId,
                m_EconomyAuthentication.GetPlayerId(),
                new PlayerPurchaseGoogleplaystoreRequest(
                    args.RealMoneyPurchaseId, 
                    args.PurchaseData, 
                    args.PurchaseDataSignature,
                    args.LocalCost, 
                    args.LocalCurrency));

            try
            {
                Response<PlayerPurchaseGoogleplaystoreResponse> response = await m_PurchasesApiClient.RedeemGooglePlayPurchaseAsync(request);
                
                RedeemGooglePlayPurchaseResult convertedResponse = ConvertBackendGooglePurchaseModelToSDKModel(response.Result);

                TriggerEventsForGooglePurchase(convertedResponse);

                return convertedResponse;
            }
            catch (HttpException<ErrorResponsePurchaseGoogleplaystoreFailed> e)
            {
                throw EconomyAPIErrorHandler.HandleGoogleStoreFailedExceptions(e);
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

        static void TriggerEventsForVirtualPurchase(MakeVirtualPurchaseResult convertedResponse)
        {
            foreach (var currencyCost in convertedResponse.Costs.Currency)
            {
                Economy.PlayerBalances.FireBalanceUpdatedEvent(currencyCost.Id);
            }
            foreach (var currencyReward in convertedResponse.Rewards.Currency)
            {
                Economy.PlayerBalances.FireBalanceUpdatedEvent(currencyReward.Id);
            }
            foreach (var inventoryItemCost in convertedResponse.Costs.Inventory)
            {
                ProcessInventoryInstanceChangeEvents(inventoryItemCost);
            }
            foreach (var inventoryItemReward in convertedResponse.Rewards.Inventory)
            {
                ProcessInventoryInstanceChangeEvents(inventoryItemReward);
            }
        }

        static void TriggerEventsForApplePurchase(RedeemAppleAppStorePurchaseResult convertedResponse)
        {
            foreach (var currencyReward in convertedResponse.Rewards.Currency)
            {
                Economy.PlayerBalances.FireBalanceUpdatedEvent(currencyReward.Id);
            }
            foreach (var inventoryItemReward in convertedResponse.Rewards.Inventory)
            {
                ProcessInventoryInstanceChangeEvents(inventoryItemReward);
            }
        }
        
        static void TriggerEventsForGooglePurchase(RedeemGooglePlayPurchaseResult convertedResponse)
        {
            foreach (var currencyReward in convertedResponse.Rewards.Currency)
            {
                Economy.PlayerBalances.FireBalanceUpdatedEvent(currencyReward.Id);
            }
            foreach (var inventoryItemReward in convertedResponse.Rewards.Inventory)
            {
                ProcessInventoryInstanceChangeEvents(inventoryItemReward);
            }
        }

        static void ProcessInventoryInstanceChangeEvents(InventoryExchangeItem inventoryExchangeItems)
        {
            foreach (var inventoryItemInstanceID in inventoryExchangeItems.PlayersInventoryItemIds)
            {
                Economy.PlayerInventory.FireInventoryItemUpdated(inventoryItemInstanceID);
            }
        }
        
        internal static RedeemAppleAppStorePurchaseResult ConvertBackendApplePurchaseModelToSDKModel(PlayerPurchaseAppleappstoreResponse backendObject)
        {
            string rawResponse = JsonConvert.SerializeObject(backendObject);
            RedeemAppleAppStorePurchaseResult convertedObject = JsonConvert.DeserializeObject<RedeemAppleAppStorePurchaseResult>(rawResponse);
            return convertedObject;
        }
        
        internal static RedeemGooglePlayPurchaseResult ConvertBackendGooglePurchaseModelToSDKModel(PlayerPurchaseGoogleplaystoreResponse backendObject)
        {
            string rawResponse = JsonConvert.SerializeObject(backendObject);
            RedeemGooglePlayPurchaseResult convertedObject = JsonConvert.DeserializeObject<RedeemGooglePlayPurchaseResult>(rawResponse);
            return convertedObject;
        }
    }
}