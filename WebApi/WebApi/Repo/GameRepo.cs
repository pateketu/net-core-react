using System;
using System.Collections.Generic;
using WebApi.Model;

namespace WebApi.Repo
{
    public class GameRepo : IGameRepo
    {
        private readonly Dictionary<Guid, Game> _inMemoryDb = new Dictionary<Guid, Game>();
        public void Add(Game exercise)
        {
            this._inMemoryDb.Add(exercise.Id, exercise);
        }

        public Game Get(Guid id)
        {
            return this._inMemoryDb[id];
        }

        public void Update(Game exercise)
        {
            this._inMemoryDb[exercise.Id] = exercise;
        }
    }
}
