using System;
using WebApi.Model;

namespace WebApi.Logic
{
    public interface IExerciseLogic
    {
        Exercise Start();
        ExerciseResult GetNext(Guid id, int answer);
    }
}