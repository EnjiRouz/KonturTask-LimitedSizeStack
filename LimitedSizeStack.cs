using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        readonly LinkedList<T> list = new LinkedList<T>();
        private readonly int limit;
        
        public LimitedSizeStack(int limit)
        {
            this.limit = limit;
            Count = 0;
        }

        public void Push(T item)
        {
            if (list.Count == limit)
            {
                list.RemoveFirst();
                Count--;
            }

            list.AddLast(item);
            Count++;
        }

        public T Pop()
        {
            if (list.Count == 0) throw new InvalidOperationException();
            var result = list.Last.Value;
            list.RemoveLast();
            Count--;
            return result;
        }

        public int Count { get; private set; }
    }
}
