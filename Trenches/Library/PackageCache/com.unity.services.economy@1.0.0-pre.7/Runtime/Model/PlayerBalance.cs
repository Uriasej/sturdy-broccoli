using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single currency balance for a player.
    /// </summary>
    [Preserve]
    public class PlayerBalance
    {
        [Preserve]
        public PlayerBalance(string currencyId = default(string), long balance = default(long), string writeLock = default(string), EconomyDate created = default(EconomyDate), EconomyDate modified = default(EconomyDate))
        {
            CurrencyId = currencyId;
            Balance = balance;
            WriteLock = writeLock;
            Created = created;
            Modified = modified;
        }
        
        /// <summary>
        /// The ID of the currency this balance represents.
        /// </summary>
        [Preserve] public string CurrencyId;
        /// <summary>
        /// The amount of this currency the player has.
        /// </summary>
        [Preserve] public long Balance;
        /// <summary>
        /// The current WriteLock string.
        /// </summary>
        [Preserve] public string WriteLock;
        /// <summary>
        /// The date this balance was created as an EconomyDate object. 
        /// </summary>
        [Preserve] public EconomyDate Created;
        /// <summary>
        /// The date this balance was modified as an EconomyDate object.
        /// </summary>
        [Preserve] public EconomyDate Modified;

        /// <summary>
        /// Gets the currency definition for this balance.
        /// </summary>
        /// <returns>The CurrencyDefinition that this balance refers to.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public async Task<CurrencyDefinition> GetCurrencyDefinitionAsync()
        {
            return await Economy.Configuration.GetCurrencyAsync(CurrencyId);
        }
    }
}
