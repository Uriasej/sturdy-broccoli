# Editor Tools

The Game Economy package comes with some Editor tools that provide more ways to interact with your economy.

## Scriptable Objects

You can interact with your economy directly through the Unity Editor using the scriptable objects provided by the Economy package.

There are scriptable objects for balances, inventories and virtual purchases.

For these objects to work you will need to follow the usual Game Economy setup flows - this includes configuring your Economy through the Unity Dashboard and signing in via Authentication.

To create the objects, right click in your project window and navigate to ```Create -> Economy Tools``` and select the desired helper.

### Example workflow
1. Create a button game object
2. Create a new purchases helper in your project and configure the settings.
3. Drag the purchases helper into the on click event of the button in the inspector.
4. Select the `InvokeAsync()` method as the method to be trigger on click.

That's it! When you click the button, you will attempt to make that purchase.

### Player Balances Helper
This helper allows you to set, increment and decrement a players balance. When configuring your event, trigger the `InvokeAsync()` method on this object.

- `Action` : The method of interaction (set, increment or decrement).
- `Currency ID` : The ID of the currency you want to modify. 
- `Amount` : The amount you want to modify the currency by. 

### Player Inventories Helper

This helper allows you to add and update players inventory items. When configuring your event, trigger the `InvokeAsync()` method on this object.

- `Action` : The method of interaction (add or update).
- `Players Inventory Item ID` : The ID of the players inventory item you want to add or update. If adding, you can leave this blank and an ID will be auto-generated for you.
- `Item ID` : **Only required for the Add action**. The inventory item ID of the item you want to add to the players inventory.
- `Instance Data JSON` : **Only required for the Update action**. The instance data you want to add to the players inventory item you are updating. This must be valid JSON.

### Purchases Helper
This helper allows you to make virtual purchases. When configuring your event, trigger the `InvokeAsync()` method on this object.

Note: This helper does not support real money purchases.

- `Purchase ID` : The ID of the virtual purchase you want to make.