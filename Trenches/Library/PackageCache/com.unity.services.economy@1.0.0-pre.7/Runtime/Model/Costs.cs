using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// The costs associated with a virtual purchase. This includes a list of currencies and inventory items that were
    /// required to make the purchase.
    /// </summary>
    [Preserve]
    public class Costs
    {
        [Preserve]
        [JsonConstructor]
        public Costs(List<CurrencyExchangeItem> currencies, List<InventoryExchangeItem> inventory)
        {
            Currency = currencies;
            Inventory = inventory;
        }

        /// <summary>
        /// A list of currencies and their amounts required to make the purchase.
        /// </summary>
        [Preserve] public List<CurrencyExchangeItem> Currency;
        /// <summary>
        /// A list of player's inventory items and their amounts required to make the purchases.
        /// </summary>
        [Preserve] public List<InventoryExchangeItem> Inventory;
    }
}
