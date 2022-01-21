using System.Runtime.CompilerServices;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Authentication.Internal;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests")]

// This allows the Economy test to see the generated SDK classes/models etc.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Unity.Services.Economy
{
    public static class Economy
    {
        static IEconomyAuthentication s_EconomyAuth;

        /// <summary>
        /// The Configuration methods allow you to access the global Economy configuration for your game.
        /// </summary>
        public static Configuration Configuration;
        
        /// <summary>
        /// The PlayerBalances methods provide access to the current player's balances, and allow you to update them.
        /// </summary>
        public static PlayerBalances PlayerBalances;

        /// <summary>
        /// The PlayerInventory methods provide access to the current player's inventory items, and allow you to update them.
        /// </summary>
        public static PlayerInventory PlayerInventory;

        /// <summary>
        /// The Purchases methods allow you to make virtual and real world purchases.
        /// </summary>
        public static Purchases Purchases;

        internal static void InitializeEconomy(IAccessToken accessToken, IPlayerId playerId, ICurrenciesApiClient currenciesApiClient, IInventoryApiClient inventoryApiClient, IPurchasesApiClient purchasesApiClient)
        {
            s_EconomyAuth = new EconomyAuthentication(playerId, accessToken);
            Configuration = new Configuration(s_EconomyAuth);
            PlayerBalances = new PlayerBalances(currenciesApiClient, s_EconomyAuth);
            PlayerInventory = new PlayerInventory(inventoryApiClient, s_EconomyAuth);
            Purchases = new Purchases(purchasesApiClient, s_EconomyAuth);
        }
    }
}