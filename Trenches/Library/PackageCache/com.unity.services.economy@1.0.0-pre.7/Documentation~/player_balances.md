# Player Balances

The `PlayerBalances` namespace contains all the methods for fetching and updating a player's currency balances.

These methods will return the balances for the currently signed in player from the Authentication SDK.

> **All the methods in this namespace can throw an `EconomyException` as described [here](./exceptions.md#EconomyException)**

## GetBalancesAsync

Retrieve the currency balances for the current user. Takes an optional `GetBalancesOptions`.

When getting balances, you can use the GetBalancesOptions to set a limit on the number of balances to fetch (between 1 and 100 inclusive). This is to help with pagination. The default number is 20.

Note: This will return balances for currencies that have been deleted in the configuration the user, including those for currencies that have since been deleted in the configuration.

```cs
// Will retrieve the default maximum of 20 balances
GetBalancesResult playerBalancesResponse = await Economy.PlayerBalances.GetBalancesAsync();

List<PlayerBalance> listOfBalances = playerBalancesResponse.Balances;

if (playerBalancesResponse.HasNext) {
    playerBalancesResponse.GetNext();
}
List<PlayerBalance> updatedBalanceList = playerBalancesResponse.Balances;

// ... etc
```

Retrieve the first 5 balances for the current user, and then retrieve the next 5.
```cs
PlayerBalances.GetBalancesOptions options = new PlayerBalances.GetBalancesOptions
{
    ItemsPerFetch = 5
};

GetBalancesResult playerBalancesResponse = await Economy.PlayerBalances.GetBalancesAsync(options);

List<PlayerBalance> listOfBalances = playerBalancesResponse.Balances;

if (playerBalancesResponse.HasNext) {
    GetBalancesResult nextPlayerBalancesResponse = await playerBalancesResponse.GetNext(5);
}

// ... etc
```

These methods return a `GetBalancesResult`. This object handles the pagination for you (see below for details)

### GetBalancesOptions

The options object for a `GetBalancesAsync` call. It has the following fields:
- `ItemsPerFetch`: An int. Defaults to 20. Use this to set the maximum number of balances to fetch per call between 1 and 100 inclusive.

### GetBalancesResult

A `GetBalancesResult` provides paginated access to the list of balances retrieved. It has the following fields:

- `Balances`: A `List<PlayerBalance>` with the currently fetched balances

It has the following methods:

- `GetNextAsync(int itemsToFetch = 20)`: This method asynchronously fetches more results. It has one optional parameter to limit the amount
of results fetched (this can be between 1 and 100 inclusive, default is 20). It will return a new result, which contains both the original items and
the newly fetched items in it's `Balances` list.

## SetBalanceAsync

Sets the balance of the specified currency to the specified value.

This method optionally takes a `SetBalancesOptions` object used to set the write lock. If provided, then an exception will be thrown unless the writeLock matches the writeLock received by a previous read, in order to provide optimistic concurrency. If not provided, the transaction will proceed regardless of any existing writeLock in the data.

This method returns the current balance after the update has been applied, if the operation is successful.

```cs
string currencyID = "GOLD_BARS";
int newAmount = 1000;
string writeLock = "someLockValueFromPreviousRequest";
PlayerBalances.SetBalanceOptions options = new PlayerBalances.SetBalanceOptions
{
    WriteLock = writeLock
};

PlayerBalance newBalance = await Economy.PlayerBalances.SetBalanceAsync(currencyID, newAmount);
// OR
PlayerBalance otherNewBalance = await Economy.PlayerBalances.SetBalanceAsync(currencyID, newAmount, options);
```

### SetBalanceOptions

The options object for a `SetBalanceAsync` call. It has the following fields:
- `WriteLock`: A string. Defaults to null. Use this to set a write lock for optimistic concurrency. More details [here](./common_objects.md#Write Lock).

## IncrementBalanceAsync

Increments the balance of the specified currency by the specified value.

This method optionally takes a `IncrementBalancesOptions` object used to set the write lock. If provided, then an exception will be thrown unless the writeLock matches the writeLock received by a previous read, in order to provide optimistic concurrency. If not provided, the transaction will proceed regardless of any existing writeLock in the data.

This method returns the current balance after the update has been applied, if the operation is successful.

```cs
string currencyID = "GOLD_BARS";
int incrementAmount = 1000;
string writeLock = "someLockValueFromPreviousRequest";
PlayerBalances.IncrementBalanceOptions options = new PlayerBalances.IncrementBalanceOptions
{
    WriteLock = writeLock
};

PlayerBalance newBalance = await Economy.PlayerBalances.IncrementBalanceAsync(currencyID, newAmount);
// OR
PlayerBalance otherNewBalance = await Economy.PlayerBalances.IncrementBalanceAsync(currencyID, newAmount, options);
```

### IncrementBalanceOptions

The options object for a `IncrementBalanceAsync` call. It has the following fields:
- `WriteLock`: A string. Defaults to null. Use this to set a write lock for optimistic concurrency. More details [here](./common_objects.md#Write Lock).

## DecrementBalanceAsync

Decrements the balance of the specified currency by the specified value.

This method optionally takes a `DecrementBalanceOptions` object used to set the write lock. If provided, then an exception will be thrown unless the writeLock matches the writeLock received by a previous read, in order to provide optimistic concurrency. If not provided, the transaction will proceed regardless of any existing writeLock in the data.

This method returns the current balance after the update has been applied, if the operation is successful.

```cs
string currencyID = "GOLD_BARS";
int decrementAmount = 1000;
string writeLock = "someLockValueFromPreviousRequest";
PlayerBalances.DecrementBalanceOptions options = new PlayerBalances.DecrementBalanceOptions
{
    WriteLock = writeLock
};

PlayerBalance newBalance = await Economy.PlayerBalances.DecrementBalanceAsync(currencyID, newAmount);
// OR
PlayerBalance otherNewBalance = await Economy.PlayerBalances.DecrementBalanceAsync(currencyID, newAmount, options);
```

### DecrementBalanceOptions

The options object for a `DecrementBalanceAsync` call. It has the following fields:
- `WriteLock`: A string. Defaults to null. Use this to set a write lock for optimistic concurrency. More details [here](./common_objects.md#Write Lock).

## PlayerBalance

A player balance represents a single currency balance for a player. It has the following fields:

- `CurrencyId`: The ID of the currency this balance represents
- `Balance`: The integer amount of this currency the player has
- `WriteLock`: The current writeLock string
- `Created`: The date this balance was created. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).
- `Modified`: The date this balance was modified. It is an `EconomyDate` object (see [here](./common_objects.md#EconomyDate)).

It also has the following helper methods

### GetCurrencyDefinition

This is a convenience method to get the currency definition for the currency associated with this balance. 
It returns a `CurrencyDefinition`, as described [here](./configuration.md#CurrencyDefinition).

```cs
PlayerBalance myPlayerBalance = ... // Get a player balance from one of the above methods
CurrencyDefinition currencyDefForMyPlayerBalance = myPlayerBalance.GetCurrencyDefinitionAsync();
```

## BalanceUpdated

This event can be subscribed to in order to be notified when the SDK updates the balance of a particular currency.
The subscriber will be passed the currency ID of the balance that was updated.

**Note**: This event will only be called for SDK initiated actions (e.g. updating player's balances, making purchases etc). 
It will _not_ be called for any updates from other devices / service side changes.

```cs
Economy.PlayerBalances.BalanceUpdated += currencyID => {
    Debug.Log($"The currency that was updated was {currencyID}");
};
```