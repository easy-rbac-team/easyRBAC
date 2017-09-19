using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EasyRbac.Utils
{
    class CircleArray
    {
        private int _capacity;
        private IdSeed[] _datas;

        public CircleArray(int capacity)
        {
            this._capacity = capacity;
            this._datas = new IdSeed[capacity];
        }



        public long GenerateSequence(long timestamp)
        {
            var ix = timestamp % this._capacity;
            var seed = this._datas[ix];
            if (seed?.Timestamp != timestamp)
            {
                var newSeed = new IdSeed()
                {
                    Seed = 0,
                    Timestamp = timestamp
                };
                Interlocked.CompareExchange(ref this._datas[ix], newSeed, seed);
            }
            return Interlocked.Increment(ref this._datas[ix].Seed);
        }

    }
}
