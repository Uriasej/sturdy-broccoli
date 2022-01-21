# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [3.0.0-pre.2] - 2021-12-02

* Analytics Runtime dependency has been updated, the PIPL headers are now included in `ForgetMe` event, when appropriate.

## [3.0.0-pre.1] - 2021-11-26

### Added

**Breaking Change**: 
- New APIs provided for checking if PIPL consent is needed, and recording users' consent. 
  It is now required to check if any consent is required, and provide that consent if necessary, before the events will be sent from the SDK.

## [2.0.7-pre.7] - 2021-10-20

### Added

* projectID parameter to all events

### Fixed

* GameStart event `idLocalProject` having a nonsense value
* Heartbeat cadence being affected by Time Scale
* Failing to compile for WebGL with error " The type or namespace name 'DllImportAttribute' could not be found"

### Changed

* User opt-out of data collection. Developers must expose this mechanism to users in an appropriate way:
  * Give users access to the privacy policy, the URL for which is stored in the `Events.PrivacyUrl` property
  * Disable analytics if requested using the `Events.OptOut()` method

### Removed

* Deprecated Transaction event `isInitiator` parameter
* Deprecated previous opt-out mechanism (DataPrivacy and DataPrivacyButton)

## [2.0.7-pre.6] - 2021-08-26

### Fixed

* GameRunning event being recorded and uploaded erratically
* Removed some obsolete steps from readme
* Clarified and added some missing XmlDoc comments on public methods

## [2.0.7-pre.4] - 2021-08-19

### Changed

* Updated README
* Regenerated `.meta` files for privacy

## [2.0.7-pre.2] - 2021-08-18

### Removed

* Version of CustomData method that takes an Event Version

### Changed

* Regen'd `.meta` files for privacy

### Added

* Added UI as a dependency

## [2.0.7] - 2021-08-09

### Changed

* New custom code entry point.
* Arguments for AdImpression now handled by an object.

### Added

* New way to interact with buffer.

## [2.0.6] - 2021-06-17

### Changed

* Bump dependencies

## [2.0.5] - 2021-05-18

### Changed

* Use Core for Authentication ID
* Use Core for Install ID
* Use `https` instead of `http`

## [2.0.4] - 2021-05-10

### Changed

* URL now uses the new collect url based off project_id and not a legacy one.

### Removed

* UI for setting up the collect url.

## [2.0.3] - 2021-05-05

### Added

* Re added support for 2019.4
* Update dependencies

## [2.0.2] - 2021-04-29

### Added

* Project settings UI

### Removed

 * `Setup()` API entry point
 * Custom UserID and SessionID

## [0.1.1] - 2021-04-01

### Changed

* Removed util package
* Changed `RecordEvent` entry point to `CustomData`

## [0.1.0] - 2021-03-31

### Added

* Standard events
