using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EasyRbac.Test
{
    public class SnowflakeTest
    {
        [Fact]
        public void Snowflake_Generate_test_success()
        {
            for (int index = 0; index < 10; index++)
            {
                var g = new Snowflake.IdWorker(1,10);

                var queue1 = new ConcurrentQueue<long>();
                Enumerable.Range(0, 5097159).AsParallel().WithDegreeOfParallelism(100).ForAll(x =>
                {
                    var idg = g.NextId();
                    //var newId = idg.GenerateId();
                    queue1.Enqueue(idg);
                });
                Assert.Equal(queue1.Count, queue1.ToDictionary(x => x, x => x).Count);
            }
        }
    }
}
