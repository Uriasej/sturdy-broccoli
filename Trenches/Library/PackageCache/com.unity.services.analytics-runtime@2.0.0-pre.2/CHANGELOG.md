# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [2.0.0-pre.2] - 2021-12-02

### Changed

* Required headers in `ConsentTracker` don't check the consent status anymore, the returned dictionary is solely based on the GeoIP response.

## [2.0.0-pre.1] - 2021-11-26

### Added

* `ConsentTracker` to enable checking whether the required consent was given for sending and storing analytics data

**Breaking Change**
* `Dispatcher` and `AnalyticsForgetter` now require a successful `GeoIP` call before sending events.

## [1.0.0-pre.3] - 2021-10-20

### Added

* AnalyticsForgetter to enable sending of data collection opt-out signal

### Changed

* Added more info logs that are printed out when the UNITY_ANALYTICS_EVENT_LOGS scripting define is enabled
* Removed colors from log messages

### Fixed

* Events being discarded if upload failed due to network or server error

## Removed

- Tests from public package

## [1.0.0-pre.2] - 2020-08-18

### Added

* Add Setter

## [1.0.0] - 2020-08-13

* Open Beta release

## [0.1.0] - 2020-08-04

### Added

* Event abstraction.
* Imported Dispatcher.
* Imported various platform abstractions.
* Buffer to handle event data 
