using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Logic
{
    public class RandomSeeder:IRandomSeeder
    {
        /*Registered as a singleton idea is use a global seeder as a parent of the random generator*/
        //https://blogs.msdn.microsoft.com/pfxteam/2009/02/19/getting-random-numbers-in-a-thread-safe-way/
        private readonly Random _global;
        public RandomSeeder()
        {
            _global = new Random();
        }

        public int GetSeed()
        {
            int seed;
            lock (_global)
            {
                seed = _global.Next();
            };
            return seed;
        }
    }
}
