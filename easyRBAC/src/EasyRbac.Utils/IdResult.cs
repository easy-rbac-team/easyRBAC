using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Utils
{
    public struct IdResult
    {
        public long Timestamp { get; set; }

        public long Sequence { get; set; }        

        public long NodeId { get; set; }

        public IdResult Resolve(long id)
        {
            this.Timestamp = id >> 31;
            var sequence = id << 33;
            sequence = sequence >> (64 - 21);
            this.Sequence = sequence;
            this.NodeId = id << 54;
            this.NodeId = this.NodeId >> 54;
            return this;
        }

        public long GenerateId()
        {
           
            long baseNum = 0L;
            //|--1位符号--|--32位时间戳--|--21位序列--|--10位机器码--|
            return baseNum | Timestamp << 31 | Sequence << 10 | this.NodeId;
            //|--1位符号--|--32位时间戳--|--24位序列--|--7位机器码--|
            //return baseNum | Timestamp << 31 | Sequence << 7 | this.NodeId;
        }

        public override string ToString()
        {
            return $"ID:{this.GenerateId()},{nameof(Timestamp)}:{Timestamp}|{nameof(Sequence)}:{Sequence}|{nameof(NodeId)}:{NodeId}";
        }
    }
}
