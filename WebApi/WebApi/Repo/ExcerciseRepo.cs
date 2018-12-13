using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Repo
{
    public class ExerciseRepo : IExerciseRepo
    {
        private readonly Dictionary<Guid, Exercise> _inMemoryDb = new Dictionary<Guid, Exercise>();
        public void Add(Exercise exercise)
        {
            this._inMemoryDb.Add(exercise.Id, exercise);
        }

        public Exercise Get(Guid id)
        {
            return this._inMemoryDb[id];
        }

        public void Update(Exercise exercise)
        {
            this._inMemoryDb[exercise.Id] = exercise;
        }
    }
}
