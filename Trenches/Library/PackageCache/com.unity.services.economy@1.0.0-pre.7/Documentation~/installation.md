# Installation

## Get started

To get started with the Economy SDK:

1. Sign in to your cloud project using the Services window in Unity 
2. Install the version of the package you wish using Package Manager
3. Initialize the Core SDK using `await UnityServices.InitializeAsync()`
4. Sign into the authentication SDK, as mentioned below

**The Economy SDK requires that an authentication flow from the Authentication SDK has been completed prior to using any of the Economy APIs**, as a valid player ID and access token are required to access the Economy services. See the documentation for the Unity Authentication SDK for more details.

## Using the SDK

The Economy SDK is ready to use immediately once sign in with the Authentication SDK is complete. You may then call any of the SDK methods to start interacting with the Economy data.

## Using the Economy Dashboard

The functionality of the SDK is only available once you have published your first Economy configuration from the Economy Dashboard in uDash.

To get started:
1. Sign into the Unity dashboard
2. Navigate to the Economy section using the side menu
3. Add some resources to the configuration
4. When you are ready for the configuration to be used in the SDK, click "Publish"

## Environments

Environments are logical partitions for Unity Game Services that contain data associated with your project.

You can set the target environment in the Economy Dashboard and in your initialization scripts. For more information on how to do this, please see the Environments documentation.

## IL2CPP Builds

Economy currently doesn't support using the `Faster (smaller) builds` option when building with IL2CPP as the scripting backend. Please use the `Faster runtime` option instead.