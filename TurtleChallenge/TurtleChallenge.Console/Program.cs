using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurtleChallenge.Console.Json;
using TurtleChallenge.Console.Observers;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.Console
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            // Test if input arguments were supplied.
            if (args.Length != 2)
            {
                System.Console.WriteLine(
                    "Please enter the file path for game settings followed by file path to moves.");
                System.Console.WriteLine(
                    "Usage: dotnet TurtleChallenge.Console.dll ExampleInputFiles/game-settings.json ExampleInputFiles/moves.json");
                return 1;
            }

            GameSettings gameSettings;
            try
            {
                gameSettings = await LoadGameSettings(args[0]);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(
                    "Fail to read game settings. Please provide a game settings in the 'ExampleInputFiles/game-settings.json' format with consistent values. Detail exception: {0}",
                    ex);
                return 1;
            }

            IList<MoveType> moves;
            try
            {
                moves = await LoadMoves(args[1]);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(
                    "Fail to read game settings. Please provide a game settings in the 'ExampleInputFiles/moves.json' format. Detail exception: {0}",
                    ex);
                return 1;
            }
            
            // Time to play :)
            // Unlike JsonParser, TurtleGame has state but no dependencies.
            // By now, I'm using the most basic form of a factory to create an instance of a turtle game.
            // In the future, I could add an [Abstract Factory](](https://sourcemaking.com/design_patterns/creational_patterns))
            // so Program.cs do not need to know about implementation classes
            // or even use [DI](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage) specially if
            // dependencies start to be added like ILogger
            var turtleGame = TurtleGame.NewGame(gameSettings);
            
            var observer = new GameReporter();
            observer.Subscribe(turtleGame);
            turtleGame.Play(moves);

            // From https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/main-and-command-args/main-return-values#example:
            // "When a program is executed in Windows, any value returned from the Main function is stored in an environment variable.
            // This environment variable can be retrieved using ERRORLEVEL from a batch file, or $LastExitCode from PowerShell."
            // Although the code challenge do not require to return a result, there is no harm on doing so and make the program more reusable
            return 0;
        }

        private static async Task<GameSettings> LoadGameSettings(string filePath)
        {
            // JsonParser has no dependencies and implemented as a static class (stateless).
            // https://enterprisecraftsmanship.com/posts/static-methods-evil/?__s=bv3ob39ia4igyb41zap2
            
            // I could use a level of abstraction by adding a IParser for example and got parser created by
            // a [creational pattern like an Abstract Factory](https://sourcemaking.com/design_patterns/creational_patterns) or DI
            // but this is, IMHO, making the implementation more complex than it need to be by now or even in the future.
            // A more flexible solution for something that do not require such flexibility also has a cost
            // like more time to understand and goes against KISS principles.
            // For example, if in the future I want to support a different file format like csv, I can extend this method
            // with a switch statement based on file extension for example. I do not need to change JsonParser implementation.
            return await JsonParser.LoadGameSettings(filePath);
        }

        private static async Task<IList<MoveType>> LoadMoves(string filePath)
        {
            return await JsonParser.LoadMoves(filePath);
        }
    }
}