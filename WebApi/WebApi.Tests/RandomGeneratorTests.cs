using System.Collections.Concurrent;
using System.Threading.Tasks;
using WebApi.Logic;
using Xunit;

namespace WebApi.Tests
{
    
    public class RandomGeneratorTests
    {
        /*
         * idea is to some how simulate, 
         * multiple threads creating a random number at the same time or nearly same time  
         * Which is similar to two users requesting the game at the same time
         * Should ideally run this on Multi core Box
         */
        [Fact(Skip = "Test is non-deterministic depending upon the processor and number of cores")]
        public void Next_CreatesUniqueRandomForDifferentThreads()
        {
            var singletonInstance = new RandomSeeder();

            ConcurrentDictionary<int, int> results = new ConcurrentDictionary<int, int>();
            
            Parallel.For(0, 5, i =>
            {
                var sut = new RandomGenerator(singletonInstance); //RandomGenerator instance per thread
                var r = sut.Next();
                Assert.False(results.ContainsKey(r), $"Found previously generated random number {r}");
                results.GetOrAdd(r, r);
            });
        }
    }
}
