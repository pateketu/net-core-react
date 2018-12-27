using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace WebApi.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Level Level { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int TimeFrameSeconds { get; set; }
        [JsonIgnore]
        public DateTime StartedAt { get; set; }
        [JsonIgnore]
        public int NumOfConsecutiveCorrectAnswers { get; set; }

        // Method to create an exercise of type a + b = ?
        public static Game CreateGame(int a, int b, int defaultTimeFrameSeconds)
        {
            return new Game() { Id = Guid.NewGuid(), A = a, B = b, TimeFrameSeconds = defaultTimeFrameSeconds, StartedAt = DateTime.Now };
        }
    }
}
