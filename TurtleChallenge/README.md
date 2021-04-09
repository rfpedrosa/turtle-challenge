# turtle-challenge c# code

- [TurtleChallenge.Console](TurtleChallenge.Console): A console app that serves as entry point to run a game
- [TurtleChallenge.Core](TurtleChallenge.Core): A class library project which has the game logic
- [TurtleChallenge.UnitTests](TurtleChallenge.UnitTests): a unit tests project that covers game logic

Implementation uses observer design pattern as it is [suitable for distributed push-based notifications, 
because it supports a clean separation between two different components or application layers, 
such as a data source (business logic) layer and a user interface (display) layer. 
The pattern can be implemented whenever a provider uses callbacks to supply its clients with current information](https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern#applying-the-pattern)

Observer pattern Implementation is based [on a simple code from Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern#applying-the-pattern)
but for commercial software, which may or not use CQRS, a more powerful library like [MediatR](https://github.com/jbogard/MediatR) may be more adequate.

That being said, you can add a graphical interface like a web app or desktop app without touch into core layer. All you need is to implement an observer like [GameReporter](TurtleChallenge.Console/Observers/GameReporter.cs).