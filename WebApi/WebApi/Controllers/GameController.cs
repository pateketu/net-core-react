using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Logic;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly IGameEngine _exerciseLogic;

        public GameController(IGameEngine exerciseLogic)
        {
            this._exerciseLogic = exerciseLogic;
        }
        // GET api/exercise
        [HttpGet]
        public Game GetExercise()
        {
            return _exerciseLogic.Start();
        }


        // POST api/exercise
        [HttpPost("{id}/{answer}")]
        public GameResult PostAnswer(Guid id, int answer)
        {
            return this._exerciseLogic.GetNext(id, answer);
        }
    }
}
