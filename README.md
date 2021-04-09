# turtle-challenge
A c# implementation for the [Turtle Challenge](TurtleChallenge.pdf). **Implementation details are described [here](TurtleChallenge)**

## Getting Started

### Prerequisites

- .Net 5.x

### Run a game

1. Clone the repo


2. Build the solution / generate binaries files

`dotnet publish TurtleChallenge/TurtleChallenge.Console`

3. Run a game with example files
   
`cd TurtleChallenge/TurtleChallenge.Console/bin/Debug/net5.0/publish/`

`dotnet TurtleChallenge.Console.dll ExampleInputFiles/game-settings.json ExampleInputFiles/moves.json`

4. Edit input files and run your own game :)

### Run unit tests

`dotnet test TurtleChallenge.UnitTests/TurtleChallenge.UnitTests.csproj`
