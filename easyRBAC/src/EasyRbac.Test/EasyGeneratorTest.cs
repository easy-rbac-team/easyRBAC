using EasyRbac.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyRbac.Test
{
    public class EasyGeneratorTest
    {
        [Fact]
        public void Generate_test_success()
        {
            //for (int index = 0; index < 10; index++)
            //{
                var g = new EasyGenerator(10);
                var queue = new ConcurrentDictionary<long, IdResult>();
                var queue1 = new ConcurrentQueue<long>();
                Enumerable.Range(0, 5097159).AsParallel().WithDegreeOfParallelism(10).ForAll(x =>
                {
                    var idg = g.GetIdResult();
                    var newId = idg.GenerateId();
                    //Assert.False(queue.ContainsKey(newId),$"{newId}:{idg}||{queue.TryGetValue(newId,out IdResult value)}{value} ");
                    //var has = queue.TryGetValue(newId, out IdResult value);
                    queue1.Enqueue(newId);
                    //Assert.False(has, $"{newId}:{idg}||{value}");
                    //queue.TryAdd(newId, idg);


                });
                Assert.Equal(queue1.Count, queue1.ToDictionary(x=>x,x=>x).Count);
            //}
        }
            

        [Fact]
        public void Generate_grows_success()
        {
            var g = new EasyGenerator(10);
            IdResult oldIdResult = new IdResult();
            for(int i = 0; i < 5000000; i++)
            {                
                IdResult newIdResult = g.GetIdResult();
                Assert.True(newIdResult.GenerateId() > oldIdResult.GenerateId(), $"{newIdResult}>{oldIdResult}???");
                oldIdResult = newIdResult;
            };
        }

        [Fact]
        public void ResolveId_success()
        {
            var g = new EasyGenerator(10);

            Parallel.For(0, 500000, (i) =>
              {
                  var newId = g.GetIdResult();
                  long id = newId.GenerateId();
                  var idResult = new IdResult(id);

                //Assert.Equal(newId.Timestamp, idResult.Timestamp);
                //Assert.Equal(newId.NodeId, idResult.NodeId);
                ////Assert.True(newId.Sequence == idResult.Sequence, $"{id},{newId.Sequence},{idResult.Sequence}");
                Assert.Equal(newId.Sequence, idResult.Sequence);
                Assert.True(id.Equals(idResult.GenerateId()), $"{id}|{idResult.GenerateId()}||{newId.ToString()}---{idResult.ToString()}");
              });
        }
    }
}
