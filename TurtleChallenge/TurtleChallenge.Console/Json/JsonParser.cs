using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.Console.Json
{
    public static class JsonParser
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            Converters =
            {
                // I like to read enum as string as it is more descriptive for someone that is configuring a new game
                // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-customize-properties#enums-as-strings
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
        
        public static async Task<GameSettings> LoadGameSettings(string filePath)
        {
            await using var openStream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<GameSettings>(openStream, Options);
        }

        public static async Task<IList<MoveType>> LoadMoves(string filePath)
        {
            await using var openStream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<IList<MoveType>>(openStream, Options);
        }
    }
}