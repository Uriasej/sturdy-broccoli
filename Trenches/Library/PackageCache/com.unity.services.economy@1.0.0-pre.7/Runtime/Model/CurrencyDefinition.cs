using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single currency configuration.
    /// </summary>
    [Preserve] 
    public class CurrencyDefinition : ConfigurationItemDefinition
    {
        /// <summary>
        /// The amount of currency a player initially is given.
        /// </summary>
        [Preserve] [JsonProperty("initial")] [JsonRequired] public int Initial;
        /// <summary>
        /// (Optional, a value of 0 indicates no maximum) The maximum amount of this currency a player can own.
        /// </summary>
        [Preserve] [JsonProperty("max")] public int Max;
        
        /// <summary>
        /// Gets the current balance of this currency for the currently signed in player.
        /// It is equivalent to the balance for this currency retrieved from Economy.PlayerBalances.GetBalancesAsync()
        /// </summary>
        /// <returns>A PlayerBalance object containing the currency balance for this currency.</returns>
        public async Task<PlayerBalance> GetPlayerBalanceAsync()
        {
            // Note: Could be simplified and performance improved by a "GetBalance" method being available on the API
            GetBalancesResult result = await Economy.PlayerBalances.GetBalancesAsync();
            return result.Balances.Find(b => b.CurrencyId == Id);
        }
    }
}
