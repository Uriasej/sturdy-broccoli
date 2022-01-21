using Unity.Services.Economy;
using UnityEngine;

namespace EconomyTools
{
    [CreateAssetMenu(fileName = "PurchasesHelper", menuName = "Economy Tools/Purchases Helper")]
    public class PurchasesHelper : ScriptableObject
    {
        [Header("Make Purchase")]
        public string purchaseId;

        /// <summary>
        /// </summary>
        /// <returns>Currently returns void so the event is selectable/recognised in the editor inspector</returns>
        public async void InvokeAsync()
        {
            await Economy.Purchases.MakeVirtualPurchaseAsync(purchaseId);
        }
    }
}
