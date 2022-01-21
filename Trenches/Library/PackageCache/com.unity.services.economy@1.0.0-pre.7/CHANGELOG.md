# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0-pre.7] - 2020-12-13
* Added more detailed logging for exceptions.
* Fixed a bug that was causing the created and modified dates to be set incorrectly in the SetBalancesAsync function.

## [1.0.0-pre.6] - 2020-10-20
* Fixed the UI samples and made them responsive to screen size.
* Added a new exception type `EconomyValidationException` that inherits from `EconomyException`, see documentation for more details.
* Fixed leak warnings.

## [1.0.0-pre.5] - 2020-10-12
* Some models have been made internal as they were not designed to be used externally. This has meant we have needed to change some property types and rename some classes. Functionality change is minimal, with the exception of the `GoogleStore` object, detailed below. Here is a full list of changes:
* The `Data` property on the `EconomyAppleAppStorePurchaseFailedException` class has changed type from `PlayerPurchaseAppleappstoreResponse` to `RedeemAppleAppStorePurchaseResult`.
* The `Data` property on the `EconomyGooglePlayStorePurchaseFailedException` class has changed type from `PlayerPurchaseGoogleplaystoreResponse` to `RedeemGooglePlayPurchaseResult`.
* The `Verification` property on the `RedeemAppleAppStorePurchaseResult` has changed type from `Verification` to `AppleVerification`.
  * The `Store` property on the `AppleVerification` has changed type from `Store` to `AppleStore`.
* The `Verification` property on the `RedeemGooglePlayPurchaseResult` has changed type from `Verification` to `GoogleVerification`.
    * The `Store` property on the `GoogleVerification` has changed type from `Store` to `GoogleStore`. There is a functional change here - `GoogleStore` no longer contains the `Code` and `Message` properties - it only contains the `Receipt` property. `AppleStore` still contains the `Code` and `Message` properties. For more information on these models, see the documentation.
* Updated Core and Authentication dependency versions

## [1.0.0] - 2020-08-23
* Open Beta release
* Renaming changes - Instance -> PlayersInventoryItem and Item -> InventoryItem
* Allows users to redeem Google Play Store in-app purchases
* Introduces options and arguments objects for API calls
* Removed all current obsolete methods

## [0.7.0-preview] - 2021-07-30

### New Features

* Improved error handling and detail in Economy exceptions
* Economy will now check a user is signed in via Authentication before making service requests
* Obsolete method `MakeVirtualPurchase` has been removed, use MakeVirtualPurchaseAsync instead
* Obsolete method `GetReferencedItem<ConfigurationItemDefinition>` has been removed, use GetReferencedConfigurationItem instead

## [0.6.0-preview] - 2021-07-07

### New Features

* Allows users to make real money Apple App Store purchases

## [0.5.0-preview] - 2021-06-17

### New Features

* New Scriptable Objects to allow using Economy features with Game Objects
* Helper methods for quickly accessing different parts of your configuration / making purchases

### Fixed

* Dependencies have been updated

## [0.4.0-preview] - 2021-05-26

### New Features

* Events available for SDK balance and inventory item updates

### Bug Fix

* When a currency or inventory item configuration cannot be found, null will now be returned instead of throwing an exception
* Purchase, Player Balance and Player Inventory methods will now function correctly on iOS
* Configurations will now deserialise correctly on iOS
* Documentation has been improved with more detail

## [0.3.0-preview] - 2021-05-17

### New Features

* Access and make virtual purchases
* Updated documentation
* Two importable samples have been added, one basic script example and one with UI
* Core SDK integration - Economy now follows the Core initialisation and authentication flows

### Bug Fix

* Resolved editor warnings around unused editor folder

## [0.2.2-preview] - 2021-05-07

### Bug Fix

* Fixes configurations to work with the new structure as sent by the Economy service

Note, that in order to use this and subsequent versions of the SDK you need to republish your Economy configuration in the dashboard.

## [0.2.1-preview] - 2021-05-06

### Bug Fix

* Fixes a clash between Utiltiies and Auth by removing the unneeded dependency

## [0.2.0-preview] - 2021-05-05

### New Features

* Access the current inventory item configuration
* Access and update the currently signed in player's inventory item instances
* Improved error handling - methods will now throw consistent exceptions
* Improved documentation

## [0.1.1-preview] - 2021-04-14

### Bug Fix

* Resolve an issue with Utilities version not matching scoped registry.

## [0.1.0-preview] - 2021-04-14

This is the initial release of the Game Economy SDK. Note that this version is only available from the candidates registry.

### New Features

* Access the current currency configuration
* Access the currently signed in player's currency balances
* Set, increment or decrement the currently signed in player's balances

### Known Issues

* Some exceptions (e.g. exceeding the max amount for a currency) return a generic error rather than a specific error message
