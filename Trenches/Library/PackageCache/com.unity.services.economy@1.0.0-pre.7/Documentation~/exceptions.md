# Exceptions

## Economy Exception

An `EconomyException` will be thrown when there is a problem with one of the operations in the SDK. These exceptions should 
be handled by calling code. The methods that can throw these exceptions are clearly marked in the method documentation.

The `EconomyException` has the following field in addition to those normally provided by C# `Exception`:

- `Reason`: An `EconomyExceptionReason` is an enum value that describes what category of issue occurred. This is provided
to allow a code-friendly way of detecting and handling the different types of errors that can be thrown.

Inspect the `Message` field on an `EconomyException` for a human-readable description of the error that was thrown.

There are method specific exceptions that may also be thrown by the Economy SDK. These are specified in the method documentation.

## Economy Validation Exception

`EconomyValidationException` inherits from `EconomyException` and will be thrown when there is a validation error in the SDK, for example, if you try to set an ID with an invalid character.
`EconomyValidationException` gives more insight into the validation issues that came up after reaching the service.

It includes one extra field: 
- **Details**: This is a list of errors returned from the API's Validation Error Response. It is a list of `EconomyValidationErrorDetail` (see below).

### Economy Validation Error Detail

A `EconomyValidationErrorDetail` represents a single error returned from the API's Validation Error Response.

It contains two fields:

- **Field**: The field in the data that caused the error. This is a string.
- **Messages**: Messages that describe the errors. This is a list of strings.
