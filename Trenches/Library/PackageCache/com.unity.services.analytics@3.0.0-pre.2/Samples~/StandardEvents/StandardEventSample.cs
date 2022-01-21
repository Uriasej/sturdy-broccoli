using System.Collections.Generic;

namespace Unity.Services.Analytics
{
    public class StandardEventSample
    {
        public static void RecordMinimalAdImpressionEvent()
        {
            Events.AdImpressionArgs args = new Events.AdImpressionArgs(Events.AdCompletionStatus.Completed, Events.AdProvider.UnityAds, "PLACEMENTID", "PLACEMENTNAME");
            Events.AdImpression(args);
        }

        public static void RecordCompleteAdImpressionEvent()
        {
            Events.AdImpressionArgs args = new Events.AdImpressionArgs(Events.AdCompletionStatus.Completed, Events.AdProvider.UnityAds, "PLACEMENTID", "PLACEMENTNAME");
            args.PlacementType = "PLACEMENTTYPE";
            args.AdEcpmUsd = 123.4;
            args.SdkVersion = "123.4";
            args.AdImpressionID = "IMPRESSIVE";
            args.AdStoreDstID = "DSTID";
            args.AdMediaType = "MOVIE";
            args.AdTimeWatchedMs = 1234;
            args.AdTimeCloseButtonShownMs = 5678;
            args.AdLengthMs = 2345;
            args.AdHasClicked = false;
            args.AdSource = "ADSRC";
            args.AdStatusCallback = "STATCALL";

            Events.AdImpression(args);
        }

        public static void RecordSaleTransactionWithOnlyRequiredValues()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                productsReceived = new Events.Product(),
                productsSpent = new Events.Product(),
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }

        public static void RecordSaleTransactionWithRealCurrency()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                productsReceived = new Events.Product {
                    realCurrency = new Events.RealCurrency() { realCurrencyType = "currencyType", realCurrencyAmount = 1337 } },
                productsSpent = new Events.Product() {
                    realCurrency = new Events.RealCurrency() { realCurrencyType = "currencyType", realCurrencyAmount = 1338 } },
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }

        public static void RecordSaleTransactionWithVirtualCurrency()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                productsReceived = new Events.Product {
                    virtualCurrencies = new List<Events.VirtualCurrency>() {
                        new Events.VirtualCurrency() {
                            virtualCurrencyType = "PRcurrencyType", virtualCurrencyAmount = 1337, virtualCurrencyName = "PRcurrencyName" }
                    } },
                productsSpent = new Events.Product() {
                    virtualCurrencies = new List<Events.VirtualCurrency>() {
                        new Events.VirtualCurrency() {
                            virtualCurrencyType = "PScurrencyType", virtualCurrencyAmount = 1338, virtualCurrencyName = "PScurrencyName" }
                    } },
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }

        public static void RecordSaleTransactionWithMultipleVirtualCurrencies()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                productsReceived = new Events.Product { virtualCurrencies = new List<Events.VirtualCurrency>() {
                    new Events.VirtualCurrency() {
                        virtualCurrencyType = "PRcurrencyType1", virtualCurrencyAmount = 1337, virtualCurrencyName = "PRcurrencyName1" },
                    new Events.VirtualCurrency() {
                        virtualCurrencyType = "PRcurrencyType2", virtualCurrencyAmount = 1338, virtualCurrencyName = "PRcurrencyName2" },
                } },
                productsSpent = new Events.Product() { virtualCurrencies = new List<Events.VirtualCurrency>() {
                    new Events.VirtualCurrency() {
                        virtualCurrencyType = "PScurrencyType1", virtualCurrencyAmount = 1339, virtualCurrencyName = "PScurrencyName1" },
                    new Events.VirtualCurrency() {
                        virtualCurrencyType = "PScurrencyType2", virtualCurrencyAmount = 1340, virtualCurrencyName = "PScurrencyName2" },
                } },
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }

        public static void RecordSaleEventWithOneItem()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                productsReceived = new Events.Product { items = new List<Events.Item>() {
                    new Events.Item() {
                        itemName = "PRname", itemType = "PRtype", itemAmount = 1
                       } } },
                productsSpent = new Events.Product
                {
                    items = new List<Events.Item>() {
                    new Events.Item() {
                        itemName = "PSname", itemType = "PStype", itemAmount = 3
                       } }
                },
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }

        public static void RecordSaleEventWithMultipleItems()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                productsReceived = new Events.Product
                {
                    items = new List<Events.Item>() {
                    new Events.Item() {
                        itemName = "PRname1", itemType = "PRtype1", itemAmount = 4
                       },
                    new Events.Item() {
                        itemName = "PRname2", itemType = "PRtype2", itemAmount = 8
                       }
                    }
                },
                productsSpent = new Events.Product
                {
                    items = new List<Events.Item>() {
                    new Events.Item() {
                        itemName = "PSname1", itemType = "PStype1", itemAmount = 1
                       },
                    new Events.Item() {
                        itemName = "PSname2", itemType = "PStype2", itemAmount = 2
                       }
                    }
                },
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }

        public static void RecordSaleEventWithOptionalParameters()
        {
            Events.Transaction(new Events.TransactionParameters
            {
                paymentCountry = "PL",
                productID = "productid987",
                revenueValidated = 999,
                transactionID = "123-456-789",
                transactionReceipt = "transactionrecepit",
                transactionReceiptSignature = "signature",
                transactionServer = Events.TransactionServer.APPLE,
                transactorID = "transactorid123-456",
                storeItemSkuID = "storeitemskuid",
                storeItemID = "storeitemid",
                storeID = "storeid",
                storeSourceID = "storesourceid",
                productsReceived = new Events.Product(),
                productsSpent = new Events.Product(),
                transactionName = "transactionName",
                transactionType = Events.TransactionType.SALE
            });
        }
    }
}