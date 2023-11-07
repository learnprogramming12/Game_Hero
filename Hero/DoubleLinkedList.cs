using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//created by Cui  20230425 
//add []operator 20230427
namespace Hero
{
    internal class Node<T>
    {
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
        public T Value { get; set; }
       public Node(T tValue)
        {
            Value = tValue;
            Next = null;
            Previous = null;
        }
    }
    internal class DoubleLinkedList<T> /*where T: IEquatable<T>*/
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _count;

        public DoubleLinkedList()
        {
            _count = 0;
        }
        public Node<T> First 
        { 
            get { return _head; }
        }
        public Node<T> Last
        {
            get { return _tail; }
        }
        public int Count
        {
            get {
                if (_count < 0)
                    _count = 0;
                return _count;
            }
        }
        public T this[int iIndex]
        {
            get
            {
                return GetElement(iIndex).Value;
            }
            set
            {
                GetElement(iIndex).Value = value;
            }
        }
        public bool AddAfter(Node<T> nodeExisting, Node<T> nodeNew)
        {
            if(nodeExisting == null || nodeNew == null) 
                return false;
            if (_tail == nodeExisting)
                return AddLast(nodeNew);

            return AddBefore(nodeExisting.Next, nodeNew);
        }
        public Node<T> AddAfter(Node<T> nodeExisting, T tNew)
        {
            if(nodeExisting == null || tNew == null)
                return null;
            if(_tail == nodeExisting)
                return AddLast(tNew);

            return AddBefore(nodeExisting.Next, tNew);
        }
        public bool AddBefore(Node<T> nodeExisting, Node<T> nodeNew)
        {
            if (nodeExisting == null || nodeNew == null)
                return false;
            if(Contains(nodeExisting) == false)
                return false;
            nodeNew.Next = nodeExisting;
            nodeNew.Previous = nodeExisting.Previous;
            if(nodeExisting.Previous != null)
            {
                nodeExisting.Previous.Next = nodeNew;
            }
            nodeExisting.Previous = nodeNew;
            if(_head == nodeExisting)
                _head = nodeNew;

            _count++;

            return true;
        }
        public Node<T> AddBefore(Node<T> nodeExisting, T tNew)
        {
            if(nodeExisting == null || tNew == null)
                return null;
            Node<T> nodeNew = new Node<T>(tNew);
            if (AddBefore(nodeExisting, nodeNew) == false)
                return null;

            return nodeNew;
        }
        public Node<T> AddLast(T tValue)
        {
            if(tValue == null)
                return null;
            Node<T> nodeNew = new Node<T> (tValue);
            if (AddLast(nodeNew) == false)
                return null;

            return nodeNew;
        }
        public bool AddLast(Node<T> nodeNew)
        {
            if(nodeNew == null)
                return false;
            nodeNew.Previous = _tail;
            nodeNew.Next = null;
            if(_tail != null)
            {
                _tail.Next = nodeNew;
            }
            else
            {
                _head = nodeNew;//_head and _tail should point to the same node if the list only has one node.
            }
            _tail = nodeNew;

            _count++;

            return true;
        }
        public Node<T> AddFirst(T tValue)
        {
            if(IsEmpty() == true)
                return AddLast(tValue);

            return AddBefore(_head, tValue);
        }
        public bool AddFirst(Node<T> nodeNew)
        {
            if (IsEmpty() == true)
                return AddLast(nodeNew);

            return AddBefore(_head, nodeNew);
        }
        public Node<T> Contains(T tValue)
        {
            if (tValue == null || IsEmpty() == true)
                return null;
            for(Node<T> nodeTemp = _head; nodeTemp != null; nodeTemp = nodeTemp.Next)
            {
                if (EqualityComparer<T>.Default.Equals(nodeTemp.Value, tValue))
                    return nodeTemp;
            }
            return null;
        }
        public bool Contains(Node<T> nodeExisting)
        {
            if (nodeExisting == null || IsEmpty() == true)
                return false;
            for(Node<T> nodeTemp = _head; nodeTemp != null; nodeTemp = nodeTemp.Next)
            {
                if(nodeTemp == nodeExisting)
                    return true;
            }
            return false;
        }
        //It will remove the first occurance of element whose value is equal to tVaue
        public bool Remove(T tValue)
        {
            Node<T> nodeTemp = Contains(tValue);
            if(nodeTemp == null)
                return false;
            DeleteNode(nodeTemp);

            return true;
        }
        public bool Remove(Node<T> nodeExisting)
        {
            if (Contains(nodeExisting) == false)
                return false;
            DeleteNode(nodeExisting);

            return true;
        }
        //added 20230613
        public bool RemoveAt(int index)
        {
            Node<T> node = GetElement(index);
            return Remove(node);
        }
        //work on the actual deleting operation and should be sure that the nodeExisting element exists in the double linked list.
        private void DeleteNode(Node<T> nodeExisting)
        {
            if(_tail == nodeExisting)
            {
                if(_tail.Previous != null)
                {
                    _tail.Previous.Next = null;
                }
                else
                {
                    //The list only has one node.
                    _head = null;
                }
                _tail = _tail.Previous;
            }
            else
            {
                nodeExisting.Next.Previous = nodeExisting.Previous;
                if(_head == nodeExisting)
                {
                    _head = nodeExisting.Next;
                }
                else
                {
                    nodeExisting.Previous.Next = nodeExisting.Next;
                }
            }
            _count--;
        }
        public void RemoveFirst()
        {
            Remove(_head);
        }
        public void RemoveLast()
        {
            Remove(_tail);
        }
        private Node<T> GetElement(int iIndex)
        {
            if (iIndex < 0 || iIndex >= _count)
                throw new IndexOutOfRangeException();
            int i = 0;
            for(Node<T> nodeTemp = _head; nodeTemp != null; nodeTemp = nodeTemp.Next)
            {
                if(i == iIndex)
                    return nodeTemp;
                i++;
            }

            throw new IndexOutOfRangeException();
        }
        public bool IsEmpty()
        {
            return _count == 0;
        }
        public void Clear()
        {           
            for(Node<T> nodeTemp = _head, nodeTemp2 = nodeTemp; nodeTemp2 != null;)
            {
                nodeTemp2 = nodeTemp.Next;
                nodeTemp.Previous = null;
                nodeTemp.Next = null;
                nodeTemp = nodeTemp2;
                _count--;//for test
            }
            _head = null;
            _tail = null;
            _count = 0;
        }
    }
}
