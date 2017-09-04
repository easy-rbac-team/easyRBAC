using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace EasyRbac.Utils
{
    public class EasyGenerator : IIdGenerator
    {
        private DateTime _startTime = new DateTime(2000, 1, 1);
        private long _nodeId;
        private long _sequence;
        private long _moment;

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

                long oldTimestamp = Interlocked.Exchange(ref this._moment, nowTimeStamp);

                //long sequence = nowTimeStamp == oldTimestamp ? Interlocked.Increment(ref _sequence) : Interlocked.Exchange(ref _sequence, 0);

                long sequence;

                
                if(nowTimeStamp == oldTimestamp)
                {
                    sequence = Interlocked.Increment(ref _sequence);
                }
                else
                {
                    //sequence = Interlocked.Increment(ref _sequence);
                    Interlocked.Exchange(ref _sequence, 0);
                    sequence = 0;
                }

                if (sequence < 1048575)
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
