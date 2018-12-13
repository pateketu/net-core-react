using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Logic
{
    public class RandomGenerator:IRandomGenerator
    {
        private readonly Random _random;
        public RandomGenerator(IRandomSeeder randomSeeder)
        {
            _random = new Random(randomSeeder.GetSeed());
        }
        
        public int Next()
        {
            return _random.Next(1,100);
        }
    }
}
