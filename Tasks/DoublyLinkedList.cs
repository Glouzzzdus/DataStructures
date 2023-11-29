using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private int _length;
        private Node _first;
        private Node _last;

        public int Length => _length;

        public void Add(T e)
        {
            var newNode = new Node(e, _last, null);
            if (_last != null)
                _last.Next = newNode;
            else
                _first = newNode;

            _last = newNode;
            _length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > _length)
                throw new ArgumentOutOfRangeException(nameof(index));

            Node newNode;

            if (index == _length)
            {
                Add(e);
                return;
            }

            if (index == 0)
            {
                newNode = new Node(e, null, _first);
                _first.Previous = newNode;
                _first = newNode;
            }
            else
            {
                Node current = _first;
                for (int i = 1; i < index; i++)
                    current = current.Next;

                newNode = new Node(e, current, current.Next);
                current.Next.Previous = newNode;
                current.Next = newNode;
            }

            _length++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= _length)
                throw new IndexOutOfRangeException(nameof(index));

            Node currentNode = _first;
            for (int i = 0; i < index; i++)
                currentNode = currentNode.Next;

            return currentNode.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node currentNode = _first;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        public void Remove(T item)
        {
            Node nodeToRemove = _first;
            while (nodeToRemove != null && !EqualityComparer<T>.Default.Equals(nodeToRemove.Value, item))
                nodeToRemove = nodeToRemove.Next;

            if (nodeToRemove == null)
                return;

            if (nodeToRemove.Previous != null)
                nodeToRemove.Previous.Next = nodeToRemove.Next;
            else
                _first = nodeToRemove.Next;

            if (nodeToRemove.Next != null)
                nodeToRemove.Next.Previous = nodeToRemove.Previous;
            else
                _last = nodeToRemove.Previous;

            _length--;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= _length)
                throw new IndexOutOfRangeException(nameof(index));

            Node currentNode;
            if (index < _length / 2)
            {
                currentNode = _first;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.Next;
            }
            else
            {
                currentNode = _last;
                for (int i = _length - 1; i > index; i--)
                    currentNode = currentNode.Previous;
            }

            if (currentNode.Previous != null)
                currentNode.Previous.Next = currentNode.Next;
            else
                _first = currentNode.Next;

            if (currentNode.Next != null)
                currentNode.Next.Previous = currentNode.Previous;
            else
                _last = currentNode.Previous;

            _length--;

            return currentNode.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node
        {
            public Node(T value, Node previous, Node next)
            {
                Value = value;
                Previous = previous;
                Next = next;
            }

            public T Value { get; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
        }
    }
}
