# MISSIVE [1.0.1]

## What is Missive?
Missive is a drag-and-drop event aggregator for single-threaded applications. Missive was built for use with Unity3D, although it should work for any C# application that needs an event publisher/subscriber architecture with no additional code required

## Why use Missive?
* You may use Missives for tracking achievements, statistics, sound, or even certain UI actions. (Missive is not a replacement for Unity's EventSystem; rather think of it as a robust replacement for SendMessage() that doesn't involve strings all over the place).
* Missives are quick, configurable, and can carry metadata about events.
* The aggregator does setup at run-time so that event handling can be as fast as possible. 

## Using Missive
### Basic Usage
1. Download and drag the contents of /src/ into your desired Unity folder. Alternatively you can use the provided Asset Package [link pending approval]. 
2. Missive provides a static instance of the Missive Aggregator at `MissiveAggregator.instance .` You need two things - a Missive to send, and a registered listener to handle the event.
3. Create a Missive type of your own inheriting from Missive. Your Missive needs to implement `ToString()`, and this should be a clear, robust description of the Missive's contents. Your missive should pass metadata in via constructor.
4. Your listener needs to explicitly handle the type of Missive you want by implementing the `IMissiveListener<T>(T: Missive)` interface. You'll have to implement the Handle method for each type of listener. (Your class can handle as many Missive types as it wants - it needs an explicit handler for each type). By the way, Missive supports inheritance of event types. This gives you a ton of flexibility to support inheritance in your game classes without creating an unstructured tree of events.
5. Finally, your listener needs to register itself when it's ready to listen to events. `MissiveAggregator.instance.Register<T>(IMissiveListener listener<T`>.

### Global Listeners
Missive allows for you register global listeners. A standard use for this might be a logger for every event, either to Unity's debug console or to a custom developer console. A global listener should inherit IMissiveListener<Missive> to catch every missive type and then register by calling `MissiveAggregator.instance.RegisterGlobalListener().`

Missive uses [NUnit](https://github.com/nunit) for testing. 

Missive is licensed [CCBY 2.0.](https://creativecommons.org/licenses/by/2.0/)
