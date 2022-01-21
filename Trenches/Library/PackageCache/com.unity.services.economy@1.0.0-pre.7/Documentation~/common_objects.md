# Common Objects

## Economy Date

An `EconomyDate` object is a wrapper for modified and created dates from the economy service. It currently has one parameter as shown below.

- `Date`: A `DateTime` representation of the wrapped date

## Write Lock

The write lock is used to implement optimistic concurrency. It is optional.

A `writeLock` is returned for each balance or inventory item instance when they are fetched, added or updated. 
The user can then pass the `writeLock` value back to the SDK when updating a currency balance or inventory instance. 
If it matches, the request is successful and will update. If it doesn't, then an error is returned.

The `writeLock` can be any `string` value.

For a code example, take a look at the method `UpdatePlayersInventoryItemUsingWriteLock` in our `InventoriesBasicExample` sample.
   
