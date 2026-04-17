using System;
using System.Collections;
using System.Collections.Generic;

namespace Elin.Plugin.Main.Models
{
    public interface IReadOnlyFixedQueue<T> : IReadOnlyCollection<T>
    {
        #region property

        /// <summary>
        /// 上限。
        /// </summary>
        int Limit { get; }

        /// <summary>
        /// 空か。
        /// </summary>
        bool IsEmpty { get; }


        #endregion

        #region function

        /// <inheritdoc cref="Queue{T}.CopyTo(T[], int)"/>
        void CopyTo(T[] array, int index);

        /// <inheritdoc cref="Queue{T}.ToArray"/>
        T[] ToArray();

        /// <inheritdoc cref="Queue{T}.Peek()"/>
        T Peek();

        #endregion
    }

    public interface IFixedQueue<T> : IReadOnlyFixedQueue<T>
    {
        #region function

        /// <inheritdoc cref="Queue{T}.Clear()"/>
        void Clear();

        /// <inheritdoc cref="Queue{T}.Dequeue()"/>
        T Dequeue();

        /// <inheritdoc cref="Queue{T}.Enqueue(T)"/>
        void Enqueue(T item);

        #endregion
    }

    public class FixedQueue<T> : IFixedQueue<T>
    {
        public FixedQueue(int limit)
        {
            if (limit < 1)
            {
                throw new ArgumentException(null, nameof(limit));
            }
            Limit = limit;
            Queue = new Queue<T>(limit);
        }

        #region property

        private Queue<T> Queue { get; }

        #endregion

        #region IQueue
        public int Limit { get; }

        public int Count => Queue.Count;

        public bool IsEmpty => Queue.Count == 0;

        public void Clear() => Queue.Clear();

        public void CopyTo(T[] array, int index) => Queue.CopyTo(array, index);

        public T[] ToArray()
        {
            return Queue.ToArray();
        }

        public T Dequeue()
        {
            return Queue.Dequeue();
        }

        public void Enqueue(T item)
        {
            while (Queue.Count >= Limit)
            {
                Queue.Dequeue();
            }
            Queue.Enqueue(item);
        }

        public T Peek()
        {
            return Queue.Peek();
        }

        public IEnumerator<T> GetEnumerator() => Queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

    }
}
