using System;
using UnityEngine;

namespace Unity.Services.Analytics
{
    public static partial class Events
    {
        public enum AdCompletionStatus
        {
            /// <summary>
            /// If the ad is fully viewed and therefore will count as an impression for the ad network.
            /// </summary>
            Completed,
            /// <summary>
            /// If there is an option to exit the ad before generating revenue.
            /// </summary>
            Partial,
            /// <summary>
            /// If the ad is not viewed at all (alternatively, don’t record the adImpression event in.
            /// </summary>
            Incomplete
        }

        public enum AdProvider
        {
            AdColony,
            AdMob,
            Amazon,
            AppLovin,
            ChartBoost,
            Facebook,
            Fyber,
            Hyprmx,
            Inmobi,
            Maio,
            Pangle,
            Tapjoy, 
            UnityAds,
            Vungle,
            IrnSource,
            Other
        }

        /// <summary>
        /// Helper object to handle arguments for recording an AdImpression event. 
        /// </summary>
        public class AdImpressionArgs
        {
            /// <param name="adCompletionStatus">(Required) Indicates a successful Ad view. Select one of the `AdCompletionStatus` values.</param>
            /// <param name="adProvider">(Required) The Ad SDK that provided the Ad. Select one of the `AdProvider` values.</param>
            /// <param name="placementID">(Required) The unique identifier for the placement as integrated into the game.</param>
            /// <param name="placementName">(Required) If there is a place in the game that can show Ads from multiple networks, there won’t be a single placementId. This field compensates for that by providing a single name for your placement. Ideally, this would be an easily human-readable name such as ‘revive’ or ‘daily bonus’. This value is here for reporting purposes only.</param>
            public AdImpressionArgs(AdCompletionStatus adCompletionStatus, AdProvider adProvider, string placementID, string placementName)
            {
                this.AdCompletionStatus = adCompletionStatus;
                this.AdProvider = adProvider;
                this.PlacementID = placementID;
                this.PlacementName = placementName;
            }

            /// <summary>
            /// Indicates whether the Ad view was successful or not.
            /// </summary>
            public AdCompletionStatus AdCompletionStatus { get; set; }

            /// <summary>
            /// The Ad SDK that provided the Ad.
            /// </summary>
            public AdProvider AdProvider { get; set; }

            /// <summary>
            /// The unique identifier for the placement where the Ad appeared as integrated into the game.
            /// </summary>
            public string PlacementID { get; set; }

            /// <summary>
            /// If there is a place in the game that can show Ads from multiple networks, there won’t be a single placementId. This field compensates for that by providing a single name for your placement. Ideally, this would be an easily human-readable name such as ‘revive’ or ‘daily bonus’.
            /// This value is here for reporting purposes only.
            /// </summary>
            public string PlacementName { get; set; }

            /// <summary>
            /// Optional.
            /// The placementType should indicate what type of Ad is shown.
            /// This value is here for reporting purposes only.
            /// </summary>
            public string PlacementType { get; set; }

            /// <summary>
            /// Optional.
            /// The estimated ECPM in USD, you should populate this value if you can.
            /// </summary>
            public double? AdEcpmUsd { get; set; }

            /// <summary>
            /// Optional.
            /// The Ad SDK version you are using.
            /// </summary>
            public string SdkVersion { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public string AdImpressionID { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public string AdStoreDstID { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public string AdMediaType { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public Int64? AdTimeWatchedMs { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public Int64? AdTimeCloseButtonShownMs { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public Int64? AdLengthMs { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public bool? AdHasClicked { get; set; }

            /// <summary>
            /// Optional.
            /// </summary>
            public string AdSource { get; set; }
            
            /// <summary>
            /// Optional.
            /// </summary>
            public string AdStatusCallback { get; set; }
        }

        /// <summary>
        /// Record an Ad Impression event.
        /// </summary>
        /// <param name="args">(Required) Helper object to handle arguments.</param>
        public static void AdImpression(AdImpressionArgs args)
        {
            Debug.Assert(!string.IsNullOrEmpty(args.PlacementID), "Required to have a value.");
            Debug.Assert(!string.IsNullOrEmpty(args.PlacementName), "Required to have a value.");

            string completionStatusString = args.AdCompletionStatus.ToString().ToUpper();
            string adProviderString = args.AdProvider.ToString().ToUpper();
            
            Data.Generator.AdImpression(ref dataBuffer, DateTime.UtcNow, s_CommonParams, "com.unity.services.analytics.events.adimpression",
                completionStatusString,  adProviderString, args.PlacementID, args.PlacementName, args.PlacementType, args.AdEcpmUsd, args.SdkVersion,
                args.AdImpressionID, args.AdStoreDstID, args.AdMediaType, args.AdTimeWatchedMs, args.AdTimeCloseButtonShownMs, args.AdLengthMs,
                args.AdHasClicked, args.AdSource, args.AdStatusCallback);
        }
    }
}
