using System;
using WebApi.Model;
namespace WebApi.Repo
{
    public interface IExerciseRepo
    {
        Exercise Get(Guid userId);
        void Add(Exercise exercise);
        void Update(Exercise exercise);

    }
}