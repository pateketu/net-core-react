using System;
using WebApi.Model;
namespace WebApi.Repo
{
    public interface IGameRepo
    {
        Game Get(Guid userId);
        void Add(Game exercise);
        void Update(Game exercise);

    }
}