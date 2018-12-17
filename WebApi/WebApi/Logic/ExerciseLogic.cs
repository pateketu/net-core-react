using System;
using WebApi.Model;
using WebApi.Repo;

namespace WebApi.Logic
{
    public class ExerciseLogic : IExerciseLogic
    {
        private readonly IExerciseRepo _repo;
        private readonly IRandomGenerator _randomGenerator;
        
        public ExerciseLogic(IExerciseRepo repo, IRandomGenerator randomGenerator)
        {
            this._repo = repo;
            this._randomGenerator = randomGenerator;
        }

        public Exercise Start()
        {
            var ex = Exercise.CreateExercise(_randomGenerator.Next(),
                                             _randomGenerator.Next(),
                                             Constants.InitialTimeFrameSeconds);
            this._repo.Add(ex);
            return ex;
        }

        public ExerciseResult GetNext(Guid id, int answer)
        {
            var ex = _repo.Get(id);

            if (HasAllLevelsFinished(ex))
            {
                return new ExerciseResult() { AllLevelsFinished = true };
            }

            if (HasTimeFrameExpired(ex))
            {
                return new ExerciseResult() { TimeFrameExpired = true };
            }

            if (!CheckAnswer(ex, answer))
            {
                return new ExerciseResult() { InCorrectAnswer = true };
            }

            ex.NumOfConsecutiveCorrectAnswers += 1;
            ex.A = _randomGenerator.Next();
            ex.B = _randomGenerator.Next();
            ex.StartedAt = DateTime.Now;
            IncrementLevel(ex);
            this._repo.Update(ex);
            return new ExerciseResult() { Exercise = ex};
        }

        private bool HasAllLevelsFinished(Exercise exercise)
        {
            return exercise.Level == ExerciseLevel.Expert;
        }

        private bool HasTimeFrameExpired(Exercise exercise)
        {
            return DateTime.Now.Subtract(exercise.StartedAt).Seconds > exercise.TimeFrameSeconds;
        }

        private bool CheckAnswer(Exercise exercise, int answer)
        {
            return ((exercise.A + exercise.B) == answer);
        }

        private void IncrementLevel(Exercise exercise)
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
    