# turtle-challenge c# code

- [TurtleChallenge.Console](/TurtleChallenge.Console): A console app that serves as entry point to run a game
- [TurtleChallenge.Core](/TurtleChallenge.Core): A class library project which has the game logic
- [TurtleChallenge.UnitTests](/TurtleChallenge.UnitTests): a unit tests project which cover game logic

Implementation uses observer design pattern is [suitable for distributed push-based notifications, 
because it supports a clean separation between two different components or application layers, 
such as a data source (business logic) layer and a user interface (display) layer. 
The pattern can be implemented whenever a provider uses callbacks to supply its clients with current information](https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern#applying-the-pattern)

In other words, you can add a graphical interface like a web app or desktop app without touch into core layer.