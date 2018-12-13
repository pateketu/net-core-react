using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Logic;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        private readonly IExerciseLogic _exerciseLogic;

        public ExerciseController(IExerciseLogic exerciseLogic)
        {
            this._exerciseLogic = exerciseLogic;
        }
        // GET api/exercise
        [HttpGet]
        public Exercise GetExercise()
        {
            return _exerciseLogic.Start();
        }


        // POST api/exercise
        [HttpPost("{id}/{answer}")]
        public ExerciseResult PostAnswer(Guid id, int answer)
        {
            return this._exerciseLogic.GetNext(id, answer);
        }
    }
}
