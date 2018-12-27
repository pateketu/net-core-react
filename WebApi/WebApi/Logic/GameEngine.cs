using System;
using WebApi.Model;
using WebApi.Repo;

namespace WebApi.Logic
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameRepo _repo;
        private readonly IRandomGenerator _randomGenerator;
        
        public GameEngine(IGameRepo repo, IRandomGenerator randomGenerator)
        {
            this._repo = repo;
            this._randomGenerator = randomGenerator;
        }

        public Game Start()
        {
            var ex = Game.CreateGame(_randomGenerator.Next(),
                                             _randomGenerator.Next(),
                                             Constants.InitialTimeFrameSeconds);
            this._repo.Add(ex);
            return ex;
        }

        public GameResult GetNext(Guid id, int answer)
        {
            var ex = _repo.Get(id);

            if (HasAllLevelsFinished(ex))
            {
                return new GameResult() { AllLevelsFinished = true };
            }

            if (HasTimeFrameExpired(ex))
            {
                return new GameResult() { TimeFrameExpired = true };
            }

            if (!CheckAnswer(ex, answer))
            {
                return new GameResult() { InCorrectAnswer = true };
            }

            ex.NumOfConsecutiveCorrectAnswers += 1;
            ex.A = _randomGenerator.Next();
            ex.B = _randomGenerator.Next();
            ex.StartedAt = DateTime.Now;
            IncrementLevel(ex);
            this._repo.Update(ex);
            return new GameResult() { Game = ex};
        }

        private bool HasAllLevelsFinished(Game exercise)
        {
            return exercise.Level == Level.Expert;
        }

        private bool HasTimeFrameExpired(Game exercise)
        {
            return DateTime.Now.Subtract(exercise.StartedAt).Seconds > exercise.TimeFrameSeconds;
        }

        private bool CheckAnswer(Game exercise, int answer)
        {
            return ((exercise.A + exercise.B) == answer);
        }

        private void IncrementLevel(Game exercise)
        {
            if (exercise.NumOfConsecutiveCorrectAnswers == 3)
            {
                exercise.NumOfConsecutiveCorrectAnswers = 0;
                exercise.Level = exercise.Level + 1;
                exercise.TimeFrameSeconds = exercise.TimeFrameSeconds - 1;
            }
        }

        
    }
}
    