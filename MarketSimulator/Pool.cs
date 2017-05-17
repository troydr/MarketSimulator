using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    //private Pool<Order> orderPool = new Pool<Order>();
    //private Pool<Position> positionPool = new Pool<Position>();
    public class Pool<T> where T : new()
    {
        private Stack<T> items = new Stack<T>();
        private object sync = new object();

        public T Get()
        {
            lock (sync)
            {
                if (items.Count == 0)
                {
                    return new T();
                }
                else
                {
                    return items.Pop();
                }
            }
        }

        public void Free(T item)
        {
            lock (sync)
            {
                items.Push(item);
            }
        }
    }
}
