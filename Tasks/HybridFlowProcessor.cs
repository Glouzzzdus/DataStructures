using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> _list = new DoublyLinkedList<T>();
        public T Dequeue()
        {
            if (_list.Length == 0)
                throw new InvalidOperationException("The list is empty.");

            var item = _list.ElementAt(0);
            _list.RemoveAt(0);

            return item;
        }

        public void Enqueue(T item)
        {
            _list.Add(item);
        }

        public T Pop()
        {
            if (_list.Length == 0)
                throw new InvalidOperationException("The list is empty.");

            var item = _list.ElementAt(_list.Length - 1);
            _list.RemoveAt(_list.Length - 1);

            return item;
        }

        public void Push(T item)
        {
            _list.Add(item);
        }
    }
}
