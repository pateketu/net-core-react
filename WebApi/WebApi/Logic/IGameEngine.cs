using System;
using WebApi.Model;

namespace WebApi.Logic
{
    public interface IGameEngine
    {
        Game Start();
        GameResult GetNext(Guid id, int answer);
    }
}