﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Exercise
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public ExerciseLevel Level { get; set; }
        public string ExerciseLevel => this.Level.ToString();
        public int A { get; set; }
        public int B { get; set; }
        public int TimeFrameSeconds { get; set; }
        [JsonIgnore]
        public DateTime StartedAt { get; set; }
        [JsonIgnore]
        public int NumOfConsecutiveCorrectAnswers { get; set; }

        // Method to create an exercise of type a + b = ?
        public static Exercise CreateExercise(int a, int b, int defaultTimeFrameSeconds)
        {
            return new Exercise() { Id = Guid.NewGuid(), A = a, B = b, TimeFrameSeconds = defaultTimeFrameSeconds, StartedAt = DateTime.Now };
        }
    }
}
