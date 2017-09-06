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
                //lock (this)
                //{
                //    var secons = (DateTime.Now - _startTime).TotalSeconds;
                //    long nowTimeStamp = (long)secons;
                //    this._sequence++;
                //    if (this._sequence  < 1048574)
                //    {
                //        var idresult = new IdResult()
                //        {
                //            Timestamp = nowTimeStamp,
                //            NodeId = _nodeId,
                //            Sequence = _sequence
                //        };
                //        return idresult;
                //    }
                //    Thread.Sleep(100);
                //}
                var secons = (DateTime.Now - _startTime).TotalSeconds;
                long nowTimeStamp = (long)secons;

                var seed = this.idDic.GetOrAdd(nowTimeStamp, new IdSeed());

                var sequence = Interlocked.Increment(ref seed.Seed);

                //清理过期key 
                if (sequence == 1 && this.idDic.Count > 1)
                {
                    var keys = this.idDic.Keys.ToList();
                    var orderdKeys = keys.OrderByDescending(x => x);
                    int i = 0;
                    foreach (var timestamp in orderdKeys)
                    {
                        // 存60s的seed，防止时钟回拨
                        if (i < 60)
                        {
                            continue;
                        }
                        this.idDic.TryRemove(timestamp, out IdSeed _);
                    }
                }

                if (sequence < 1048574)
                {
                    var idresult = new IdResult()
                    {
                        Timestamp = nowTimeStamp,
                        NodeId = _nodeId,
                        Sequence = sequence
                    };
                    return idresult;
                }
                throw new Exception("hahah ");
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
