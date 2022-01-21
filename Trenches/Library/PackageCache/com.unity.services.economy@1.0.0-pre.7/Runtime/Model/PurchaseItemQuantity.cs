using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents an amount of currency/inventory items associated with a purchase. Each one relates to a single currency/inventory item type (for example 4 swords, 10 gold etc.).
    /// </summary>
    [Preserve]
    public class PurchaseItemQuantity
    {
        [Preserve] [JsonRequired] [JsonProperty("itemId")]
        public EconomyReference Item;
        
        [Preserve] [JsonRequired] [JsonProperty("amount")]
        public int Amount;
    }
}
