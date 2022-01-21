using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// The base class used for providing pageable results from the service e.g. GetBalancesResult.
    /// </summary>
    /// <typeparam name="T">The type of resource that is being fetched. E.g. for GetBalancesResult, the type is PlayerBalance.</typeparam>
    /// <typeparam name="TSelf"></typeparam>
    [Preserve]
    public abstract class PageableResult<T,TSelf> where TSelf: PageableResult<T,TSelf>
    {
        /// <summary>
        /// The list of fetched results of type T.
        /// </summary>
        [Preserve] protected List<T> m_Results;
        /// <summary>
        /// True if there are more pages of results to fetch.
        /// </summary>
        [Preserve] public bool HasNext;
        
        [Preserve]
        protected PageableResult(List<T> results, bool hasNext)
        {
            m_Results = results;
            HasNext = hasNext;
        }

        [Preserve] protected abstract Task<TSelf> GetNextResultsAsync(int itemsPerFetch);

        /// <summary>
        /// Fetches the next page of results.
        /// </summary>
        /// <param name="itemsPerFetch">The number of items to fetch. Can be between 1-100 inclusive and defaults to 20.</param>
        /// <returns>A new results of type TSelf</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Preserve]
        public async Task<TSelf> GetNextAsync(int itemsPerFetch = 20)
        {
            if (!HasNext)
            {
                return null;
            }

            return await GetNextResultsAsync(itemsPerFetch);
        }
    }
}
