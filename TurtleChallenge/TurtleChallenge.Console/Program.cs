using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurtleChallenge.Console.Json;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Models;

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

            // To keep things simpler, I'm not using
            // [DI](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage)
            // or a [Creational pattern like an Abstract Factory](https://sourcemaking.com/design_patterns/creational_patterns)
            // However, in a long term, if i need to support a second TurtleGame implementation or a different input format file
            // I would add DI (which is aligned with 'D' of SOLID principles)
            IParser parser = new JsonParser();

            GameSettings gameSettings;
            try
            {
                
                gameSettings = await parser.LoadGameSettings(args[0]);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(
                    "Fail to read game settings. Please provide a game settings in the 'ExampleInputFiles/game-settings.json' format. Detail exception: {0}",
                    ex);
                return 1;
            }

            IList<MoveType> moves;
            try
            {
                moves = await parser.LoadMoves(args[1]);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(
                    "Fail to read game settings. Please provide a game settings in the 'ExampleInputFiles/moves.json' format. Detail exception: {0}",
                    ex);
                return 1;
            }

            var turtleGame = new TurtleGame(gameSettings, moves);
            turtleGame.Play();

            // From https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/main-and-command-args/main-return-values#example:
            // "When a program is executed in Windows, any value returned from the Main function is stored in an environment variable.
            // This environment variable can be retrieved using ERRORLEVEL from a batch file, or $LastExitCode from PowerShell."
            // Although the code challenge do not require to return a result, there is no harm on doing so and make the program more reusable
            return 0;
        }
    }
}