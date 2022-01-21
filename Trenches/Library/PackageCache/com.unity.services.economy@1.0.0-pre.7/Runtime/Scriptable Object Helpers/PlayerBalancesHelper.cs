using System;
using Unity.Services.Core.Internal;
using Unity.Services.Economy;
using UnityEngine;

namespace EconomyTools
{
    [CreateAssetMenu(fileName = "PlayerBalancesHelper", menuName = "Economy Tools/Player Balances Helper")]
    public class PlayerBalancesHelper : ScriptableObject
    {

        public enum CurrencyAction
        {
            Set,
            Increment,
            Decrement
        }

        [Header("Currencies Helper")]
        public CurrencyAction action;
        public string currencyId;
        public int amount;

        /// <summary>
        /// </summary>
        /// <returns>Currently returns void so the event is selectable/recognised in the editor inspector</returns>
        public async void InvokeAsync()
        {
            if (string.IsNullOrEmpty(currencyId))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The currency ID on the player balances helper scriptable object is empty. Please enter an ID.");
            }
            
            switch (action)
            {
                case CurrencyAction.Set:
                    await Economy.PlayerBalances.SetBalanceAsync(currencyId, amount);
                    break;
                case CurrencyAction.Increment:
                    await Economy.PlayerBalances.IncrementBalanceAsync(currencyId, amount);
                    break;
                case CurrencyAction.Decrement:
                    await Economy.PlayerBalances.DecrementBalanceAsync(currencyId, amount);
                    break;
            }
        }
    }
}
