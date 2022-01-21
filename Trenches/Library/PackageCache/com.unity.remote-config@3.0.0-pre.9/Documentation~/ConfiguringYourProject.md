# Configuring your Project for Unity Remote Config

## Requirements

* Unity Remote Config requires Unity Editor 2018.3 or higher for versions 2.0.x, and Unity Editor 2019.4 for versions 3.0.x.
* Set your [API Compatibility Level](https://docs.unity3d.com/2019.4/Documentation/Manual/dotnetProfileSupport.html) to **.NET 4.x**.
* [Enable Unity Services](https://docs.unity3d.com/2019.4/Documentation/Manual/SettingUpProjectServices.html) for your Project.
* Install the Remote Config package (detailed below).

## Installing the Remote Config package
See documentation on [packages](https://docs.unity3d.com/2019.4/Documentation/Manual/Packages.html) for more information on working with packages in a Project. These steps may vary depending on which version of the Unity Editor you’re using.

### Verified Release

1. In the Unity Editor, select **Window** > **Package Manager**.
2. From the Package Manager Window select Remote Config in the Packages List View.
3. In the Package Specific Detail View select the version and install to import the package into your Project.

### Preview Release
1. Click **Advanced** in the Package Manager Window to toggle **Preview Packages**, making them visible in the Package List View.
2. Follow the instructions for the Verified Release Installation (directly above this section).

### Beta Customers
When you receive Remote Config package from your account manager, follow these steps:

1. Download and unzip the package.
2. In the Unity Editor, select **Window** > **Package Manager**.
3. Click the plus (**+**) button at the bottom of the Package Manager window to open the **Add package from disk...** dialog.
4. Locate the _package.json_ file inside your unzipped copy of the Remote Config package.
5. Click **Open** to import the package into your Project.

See documentation on [packages](https://docs.unity3d.com/2019.4/Documentation/Manual/Packages.html) for more information on working with packages in a Project. These steps may vary depending on which version of the Unity Editor you’re using.

## Remote Config Environments

To get started, create an Environment and give it a name.

1. Go to the [Web Dashboard](http://dashboard.unity3d.com/remote-config).
2. Select the corresponding project.
3. Select **Add Environment**.
4. Enter a name for the Environment and select **Create**.

**Note: Environment names are immutable.**

The first Environment you create is set as the default Environment. This is the Environment which is requested unless otherwise specified by the client. You can assign the default Environment to an **EnvironmentID** in the Dashboard, or via the REST API.

Once you’ve configured your project, configure your rules and settings in the [Dashboard](http://dashboard.unity3d.com/remote-config)
The `production` Environment will be the default Environment, which is the Environment requested unless otherwise specified by the client. The default Environment can't be changed. The **EnvironmentID** is an automatically created ID and is viewable in the Remote Config Window in the Editor, through the Dashboard, or through the REST API.

The Legacy Remote Config service included only two build Environments. Starting with version 1.1.0 developers can create Environments, choose the Environment used in a build, and set a default Environment. **Note the Environment name `development` and `production` are reserved names.**

If a 1.0.x version package was used in your Unity Project, there will be two Environments created for your project: `production` and `development`. Additional Environments can be created but the default Environment `production` will be delivered unless an `environmentId` is specified in the request, with the ConfigManager API method `SetEnvironmentID`.

For users downgrading from 1.1.x to 1.0.x, an Environment with the name `development` will be created if it doesn't exist.

Deleting the Environment named `development` is possible but using a **Development Build** version of your Unity Project may cause unintended or breaking behavior. Those builds will receive the Settings and Campaigns from the Environment which is set as the default Environment.

## Assembly Definition References

The Remote Config package depends on Unity's authentication and core services.
These dependencies require a small amount of user code for proper configuration.

To use Remote Config, you'll need to include the following references:
* com.unity.remote-config-runtime
* Unity.Services.Authentication
* Unity.Services.Core

Prior to using Remote Config, you'll then need to:
* Initialize Unity Services
  * `UnityServices.Initialize()`
* Authenticate with Unity Authentication
  * `AuthenticationService.Instance.SignInAnonymously()` for anonymous authentication
  * `AuthenticationService.Instance.SignInWithAppleAsync()` for Apple authentication
  * `AuthenticationService.Instance.SignInWithFacebookAsync()` for Facebook authentication
  * `AuthenticationService.Instance.SignInWithGoogleAsync()` for Google authentication
  * `AuthenticationService.Instance.SignInWithSteamAsync()` for Steam authentication
  * `AuthenticationService.Instance.SignInWithSessionTokenAsync()` for authentication with an existing token


## Legacy Remote Config Environments (Only for packages in version range <=1.0.x)

### Development Rnvironment
Remote Config sends settings from the Development Environment to builds marked as development builds.

**Note**: For Unity to request the Development environment settings, you must check the **Development Build** setting in the [Build Settings window](https://docs.unity3d.com/2019.3/Documentation/Manual/BuildSettings.html) when building your application.

![Configuring your build for the Development environment.](images/BuildSettingsWindow.png)

### Production environment
Remote Config sends settings from the `production` environment to any non-development builds.

**Note**: The Editor’s Play mode always uses the Development Environment settings.

Once you’ve configured your Project, start to configure your [Campaigns and Settings](CampaignsAndSettings.md) in the **Remote Config** window and the Dashboard.
