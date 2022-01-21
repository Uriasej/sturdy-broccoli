# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0-pre.7] - 2021-12-07

### Fixed
- NullReferenceException being thrown instead of some service errors
- Documentation URL in package manifest
- Deprecated some elements that should not have been public, these will be deleted in a later release

## [1.0.0-pre.6] - 2021-09-22
- Fixes a crash that could occur with certain exceptions returned from the API

### Known Issues
- When a cloud code function that hasn't been published yet is called from the SDK, the SDK will throw a Null Reference Exception rather than a normal CloudCodeException

## [1.0.0-pre.5] - 2021-09-17
- No longer throws on null function parameter values
- No longer throws on null api return values
- Corrected exception types
- Removed tests from public package
- Fixed code examples in documentation

## [1.0.0-pre.4] - 2021-08-19
- Updated readme and changelog to be more descriptive.
- Updated package description to better highlight the usages of Cloud Code.

## [1.0.0-pre.1] - 2021-08-10

- Updated documentation in preperation for release.
- Updated dependencies (Core and Authentication) to latest versions.
- Updated internals for more stability.
- Added a new API that returns string, in order to support custom user serialization of return values.

## [1.0.0-pre.1] - 2021-08-10

- Updated documentation in preperation for release.
- Updated dependencies (Core and Authentication) to latest versions.
- Updated internals for more stability.
- Added a new API that returns string, in order to support custom user serialization of return values.

## [0.0.3-preview] - 2021-06-17

- Updated depedencies of Core and Authentication to latest versions.

## [0.0.2-preview] - 2021-05-27

- Update documentation and license

## [0.0.1-preview] - 2021-05-10

### Package Setup for Cloud Code.

- Creating the package skeleton.
