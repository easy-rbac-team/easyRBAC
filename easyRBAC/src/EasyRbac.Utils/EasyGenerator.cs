using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using EasyRbac.Utils.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EasyRbac.Utils
{
    public class EasyGenerator : IIdGenerator
    {
        private DateTime _startTime = new DateTime(2000, 1, 1);
        private long _nodeId;
        private long _sequence;
        private long _moment;
        private int roll;
        private ConcurrentDictionary<long,IdSeed> idDic = new ConcurrentDictionary<long, IdSeed>();

        public EasyGenerator(IOptions<IdGenerateConfig> ops) : this(ops.Value.NodeId) { }

        public EasyGenerator(short nodeId)
        {
            this._nodeId = nodeId;
        }

        public IdResult GetIdResult()
        {
            do
            {
                var secons = (DateTime.Now - _startTime).TotalSeconds;
                long nowTimeStamp = (long)secons;
                

                //long oldTimestamp = Interlocked.Exchange(ref this._moment, nowTimeStamp);
                
                //long sequence;


                //if (nowTimeStamp == oldTimestamp)
                //{
                //    sequence = Interlocked.Increment(ref _sequence);
                //}
                //else
                //{
                //    //sequence = Interlocked.Increment(ref _sequence);
                //    Interlocked.Exchange(ref _sequence, 0);
                //    Interlocked.Increment(ref roll);
                //    sequence = 0;
                //}
                var seed = this.idDic.GetOrAdd(nowTimeStamp, new IdSeed());
                
                //清理过期key                
                var sequence = Interlocked.Increment(ref seed.Seed);
                if (sequence == 1 && this.idDic.Count > 1)
                {
                    var keys = this.idDic.Keys.ToList();
                    var orderdKeys = keys.OrderByDescending(x => x);
                    int i = 0;
                    foreach(var timestamp in orderdKeys)
                    {
                        if (i == 0)
                        {
                            continue;
                        }
                        this.idDic.TryRemove(timestamp, out IdSeed _);
                    }
                }

                if (sequence < 1048575)
                {
                    var idresult = new IdResult()
                    {
                        Timestamp = nowTimeStamp,
                        NodeId = _nodeId,
                        Sequence = sequence,
                        Roll = this.roll
                    };
                    return idresult;
                }
                Thread.Sleep(100);
            } while (true);
        }

        public long NewId()
        {
            var result = this.GetIdResult();

            return result.GenerateId();
        }
    }
}
