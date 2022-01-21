# Unity Gaming Services Analytics

**For full documentation, visit https://docs.unity.com/analytics/**

The Unity Gaming Services Analytics package lets you record events from your application. There are
two main types of events.

Type     | Description
---------|------------------------------------------------------------------------------
Standard | Standard events are predefined, some are automatic, others are manual.
Custom   | Events that are authored by the developer that contain custom data points.

## Analytics Lifetime

The Analytics SDK is started when you initialize all Unity Services:

```cs
await UnityServices.InitializeAsync();
```

At the end of the session it will also shut down automatically.

## Logging Standard Events

Many standard events are logged automatically by the SDK. These automatic events will give you a basic picture
of how users are using it without any further intervention.

- The `sdkStart` event will be recorded on Startup
- The `clientDevice` event will be recorded on Startup
- The `gameStarted` event will be recorded on Startup
- The `newPlayer` event will be recorded on Startup if the particular device has never started the app before
- The `gameRunning` event will be recorded every 1 minute
- The `gameEnded` event will be recorded on Shutdown

There are two standard events that you can record manually, at any point in the game lifecycle.

### Transaction Event

The Transaction event is to record that the player spent some resource to obtain some other resource.
This may be real or virtual currencies or game items on either side. Because of the number of optional
arguments depending on what is being exchanged and on what platform, the Transaction method takes
a `TransactionParameters` object which can be populated as required.

```cs
Events.Transaction(new Events.TransactionParameters
	{
		productsReceived = new Events.Product(),
		productsSpent = new Events.Product(),
		transactionName = "transactionName",
		transactionType = Events.TransactionType.SALE
	});
```

### AdImpression Event

The AdImpression event is to record that the player was shown an advert and how they interacted with it.
Because of the number of optional arguments, the AdImpression method takes an `AdImpressionArgs` object
which can be populated as required.

```cs
Events.AdImpression(new Events.AdImpressionArgs(
	Events.AdCompletionStatus.Completed,
	Events.AdProvider.UnityAds,
	"PLACEMENT_ID",
	"PLACEMENT_NAME"));
```

## Logging Custom Events

At any point you may send custom events. Custom events must have a schema defined on the dashboard
or else they will be rejected by the collector (they will still be sent by the SDK).

```cs
Events.CustomData("customEventName", new Dictionary<string, object> { { "parameter": "value" } });
```

## Sending Events

Event data will be flushed out of the system every 60 seconds, and on automatic shutdown at the end of the session.

You can also force a flush at a time of your choosing:

```cs
Events.Flush();
```

You shouldn't need to do this in normal usage.
