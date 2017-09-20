using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EasyRbac.Utils.Configs;
using Microsoft.Extensions.Options;

namespace EasyRbac.Utils
{
    public class EasyGenerator : IIdGenerator
    {
        private DateTime _startTime = new DateTime(2000, 1, 1);
        private long _nodeId;
        private long _sequence;
        private long _moment;
        private CircleArray _circleArray;

        public EasyGenerator(IOptions<IdGenerateConfig> ops) : this(ops.Value.NodeId,ops.Value.TimeBack) { }

        public EasyGenerator(short nodeId,int timeback = 60)
        {
            this._nodeId = nodeId;
            this._circleArray = new CircleArray(timeback);
        }

        public IdResult GetIdResult()
        {
            do
            {
                var secons = (DateTime.Now - _startTime).TotalSeconds;
                long nowTimeStamp = (long)secons;

                var sequence = this._circleArray.GenerateSequence(nowTimeStamp);

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
