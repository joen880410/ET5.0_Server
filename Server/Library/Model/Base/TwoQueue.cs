using System;
using System.Collections.Generic;

namespace ETModel
{
    public class TwoQueue<T> : IDisposable where T : struct
    {
        public Queue<T> Queue1 = new Queue<T>();
        public Queue<T> Queue2 = new Queue<T>();

        public void ClearAll()
        {
            this.Queue1?.Clear();
            this.Queue2?.Clear();
        }

        public void Swap()
        {
            (this.Queue1, this.Queue2) = (this.Queue2, this.Queue1);
        }

        public void Dispose()
        {
            this.ClearAll();
        }
    }
}