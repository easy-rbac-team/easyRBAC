using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Utils
{
    public class IdResult
    {       
        private DateTime createTime = DateTime.Now;
        public IdResult() { }

        public IdResult(long id)
        {
            this.Timestamp = id >> 31;
            var sequence = id << 33;
            sequence = sequence >> (64-21);
            this.Sequence = sequence;
            this.NodeId = id << 54;
            this.NodeId =this.NodeId >> 54;
        }

        public long Timestamp { get; set; }

        public long Sequence { get; set; }        

        public long NodeId { get; set; }

        public DateTime CreateTime => this.createTime;

        public long GenerateId()
        {
            //|--1位符号--|--32位时间戳--|--21位序列--|--10位机器码--|
            long baseNum = 0L;           
            return baseNum | Timestamp << 31 | Sequence << 10 | this.NodeId;
        }

        public override string ToString()
        {
            return $"ID:{this.GenerateId()},{nameof(Timestamp)}:{Timestamp}|{nameof(Sequence)}:{Sequence}|{nameof(NodeId)}:{NodeId}|{nameof(CreateTime)}:{this.CreateTime}";
        }
    }
}
